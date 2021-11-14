using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Plugin.Segmented.Event;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class HapticPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        public enum WorkMode { Haptic,Vibration }
        private bool _isReleased;
        public HapticPage()
        {
            InitializeComponent();
            
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Haptics", Constants.LoremTemp, this);

            var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(BarometerPageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;

            this.MainGrid.Children.Add(tmpComp, 0, 0);
            Haptic_Image.Source = ImageSource.FromResource("DeviceTester.Resources.Images.Haptic.png");
            foreach (var Mode in Enum.GetValues(typeof(WorkMode)))
                ChangeMode.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = Mode.ToString() });
        }

        public void ChangeDescriptionState(bool State)
        {
            GridLength HeightValue = new GridLength(50, GridUnitType.Absolute);
            switch (State)
            {
                case true:
                    HeightValue = new GridLength(50, GridUnitType.Absolute);
                    break;
                case false:
                    HeightValue = new GridLength(230, GridUnitType.Absolute);
                    break;
            }
            _animation = new Animation(
                        (d) => MainGrid.RowDefinitions[0] = new RowDefinition() { Height = HeightValue });
            _animation.Commit(this, "GPS Animation", 16, 1000000, Easing.BounceIn, null, null);
        }

        void ButtonClicked_Behavior(System.Object sender, System.EventArgs e)
        {
            try
            {
                HapticFeedback.Perform(HapticFeedbackType.Click);
            }
            catch (FeatureNotSupportedException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        void LongPressBehavior_LongPressed(Object sender, EventArgs e)
        {
            try
            {
                HapticFeedback.Perform(HapticFeedbackType.LongPress);
            }
            catch (FeatureNotSupportedException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        async void Haptic_Image_Pressed(System.Object sender, System.EventArgs e)
        {
            try
            {
                _isReleased = false;
                if (((WorkMode)ChangeMode.SelectedSegment) == WorkMode.Vibration)
                {
                    var task = new Task(async () => {
                        while (!_isReleased)
                        {
                            Vibration.Vibrate();
                            Thread.Sleep(500);
                            if (_isReleased)
                                break;
                        }
                    },new CancellationToken(_isReleased));
                    var Th = new Thread(() =>
                    {
                        task.RunSynchronously();
                    });
                    Th.Start();

                }       
            }
            catch (FeatureNotSupportedException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        void Haptic_Image_Released(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (((WorkMode)ChangeMode.SelectedSegment) == WorkMode.Vibration)
                {
                    _isReleased = true;
                    Vibration.Cancel();
                }
            }
            catch (FeatureNotSupportedException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }
    }
    public class HapticPageFactory : PageFactory
    {
        public override string getPageName() => "Haptics";
        public override Page getPageObject()
        {
            return new HapticPage();
        }
    }
}

namespace DeviceTester.Content.Pages.Behaviors
{
    public class LongPressBehavior : Behavior<ImageButton>
    {
        private readonly object _syncObject = new object();
        private const int Duration = 1000;
        private Timer _timer;
        private readonly int _duration;
        private volatile bool _isReleased;


        private bool TimerElapsed;
        public event EventHandler LongPressed;
        public event EventHandler Clicked;

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LongPressBehavior), default(ICommand));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LongPressBehavior));

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public LongPressBehavior()
        {
            _isReleased = true;
            _duration = Duration;
        }
        public LongPressBehavior(int duration) : this()
        {
            _duration = duration;
        }


        protected override void OnAttachedTo(ImageButton button)
        {
            base.OnAttachedTo(button);
            this.BindingContext = button.BindingContext;
            button.Pressed += Button_Pressed;
            button.Released += Button_Released;
        }

        protected override void OnDetachingFrom(ImageButton button)
        {
            base.OnDetachingFrom(button);
            this.BindingContext = null;
            button.Pressed -= Button_Pressed;
            button.Released -= Button_Released;
        }

        /// <summary>
        /// DeInitializes and disposes the timer.
        /// </summary>
        private void DeInitializeTimer()
        {
            lock (_syncObject)
            {
                if (_timer == null)
                {
                    return;
                }
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _timer.Dispose();
                _timer = null;
            }
        }

        /// <summary>
        /// Initializes the timer.
        /// </summary>
        private void InitializeTimer()
        {
            lock (_syncObject)
            {
                _timer = new Timer(Timer_Elapsed, null, _duration, Timeout.Infinite);
            }
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            _isReleased = false;
            TimerElapsed = false;
            InitializeTimer();
        }

        private void Button_Released(object sender, EventArgs e)
        {
            _isReleased = true;
            if (!TimerElapsed)
                Device.BeginInvokeOnMainThread(OnClicked);
            DeInitializeTimer();
        }

        protected virtual void OnLongPressed()
        {
            var handler = LongPressed;
            handler?.Invoke(this, EventArgs.Empty);
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }

        protected virtual void OnClicked()
        {
            var handler = Clicked;
            handler?.Invoke(this, EventArgs.Empty);
            if(Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
        
        private void Timer_Elapsed(object state)
        {
            TimerElapsed = true;
            DeInitializeTimer();
            if (_isReleased)
            {
                return;
            }
            Device.BeginInvokeOnMainThread(OnLongPressed);
        }
    }
}