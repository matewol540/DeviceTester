﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class AccelerometerPage : ContentPage
    {
        public ObservableCollection<SensorSpeed> coll = new ObservableCollection<SensorSpeed>(Enum.GetValues(typeof(SensorSpeed)) as IEnumerable<SensorSpeed>);
        RotationModel urhoApp;
        Task IsRotationCompleted;

        public AccelerometerPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new MyLabelView("Accelerometer");
            SpeedPicker.ItemsSource = coll;
            SpeedPicker.SelectedIndex = 0;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            _ = EnableAccelerometerAsync(false);
            this.MainGrid.Children.Add(tmpComp, 0, 0);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            urhoApp = await RotationModelSurface.Show<RotationModel>(new ApplicationOptions(assetsFolder: null) { Orientation = ApplicationOptions.OrientationType.LandscapeAndPortrait });

            _ = EnableAccelerometerAsync(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _ = EnableAccelerometerAsync(false);
        }

        private async Task EnableAccelerometerAsync(Boolean state)
        {
            try
            {
                if (!state)
                {
                    Accelerometer.Stop();
                    return;
                }
                SensorSpeed sensor = (SensorSpeed)(SpeedPicker as Picker).SelectedItem;

                Accelerometer.Start(sensor);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Feature not supported", "Sorry, but your device does not support selected feature", "Return");
                await Navigation.PopModalAsync();
            }
        }

        void SpeedPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            Accelerometer.Stop();
            SensorSpeed sensor = (SensorSpeed)(sender as Picker).SelectedItem;
            Accelerometer.Start(sensor);
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            System.Numerics.Vector3 data = e.Reading.Acceleration;
            if (Track_X.IsChecked)
                data.X = 0.0F;
            if (Track_Y.IsChecked)
                data.Y = 0.0F;
            if (Track_Z.IsChecked)
                data.Z = 0.0F;

            coordX.Text = String.Format("x: {0:0.00}", data.X);
            coordY.Text = String.Format("y: {0:0.00}", data.Y);
            coordZ.Text = String.Format("z: {0:0.00}", data.Z);

            if (coordX_max.Text == String.Empty || Double.Parse(coordX_max.Text.Split(':')[1]) < data.X)
                coordX_max.Text = String.Format("Max X: {0:0.00}", data.X);
            if (coordY_max.Text == String.Empty || Double.Parse(coordY_max.Text.Split(':')[1]) < data.Y)
                coordY_max.Text = String.Format("Max y: {0:0.00}", data.Y);
            if (coordZ_max.Text == String.Empty || Double.Parse(coordZ_max.Text.Split(':')[1]) < data.Z)
                coordZ_max.Text = String.Format("Max z: {0:0.00}", data.Z);

            if (coordX_min.Text == String.Empty || Double.Parse(coordX_min.Text.Split(':')[1]) > data.X)
                coordX_min.Text = String.Format("Min X: {0:0.00}", data.X);
            if (coordY_min.Text == String.Empty || Double.Parse(coordY_min.Text.Split(':')[1]) > data.Y)
                coordY_min.Text = String.Format("Min y: {0:0.00}", data.Y);
            if (coordZ_min.Text == String.Empty || Double.Parse(coordZ_min.Text.Split(':')[1]) > data.Z)
                coordZ_min.Text = String.Format("Min z: {0:0.00}", data.Z);

             RotateObject(data);
        }

        private void RotateObject(System.Numerics.Vector3 data)
        {
            //if (IsRotationCompleted == null || IsRotationCompleted.IsCompleted)
                IsRotationCompleted = urhoApp?.RotateAsync(data);
        }
    }

    public class AccelerometerFactory : PageFactory
    {
        public override Page getPageObject()
        {
            return new AccelerometerPage();
        }
    }

    public class RotationModel : Urho.Application
    {
        bool movementsEnabled;
        Scene scene;
        Node plotNode;
        Camera camera;
        Octree octree;

        [Preserve]
        public RotationModel(ApplicationOptions options = null) : base(options) { }

        static RotationModel()
        {
            UnhandledException += (s, e) =>
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                e.Handled = true;
            };
        }

        protected override void Start()
        {
            base.Start();
            CreateScene();
            SetupViewport();
        }

        async void CreateScene()
        {
            scene = new Scene();
            octree = scene.CreateComponent<Octree>();

            plotNode = scene.CreateChild();
            plotNode.Rotation = new Quaternion(0, 0, 0);
            var baseNode = plotNode.CreateChild().CreateChild();

            CreateCamera();

            baseNode.Scale = new Vector3(5, 5, 5);

            //For oX
            var m_BoxNodeX = plotNode.CreateChild();
            var BoxNodeX = m_BoxNodeX.CreateChild();
            BoxNodeX.Position = new Vector3(0, 0, 0);
            BoxNodeX.Scale = new Vector3(5, 1, 1);

            var BoxX = BoxNodeX.CreateComponent<Box>();
            BoxX.Color = new Urho.Color(Urho.Color.Blue, 0.8F);

            var TextNodeX = m_BoxNodeX.CreateChild();
            TextNodeX.Position = new Vector3(2.5F, 0.25F, -0.25F);
            TextNodeX.Rotate(new Quaternion(0, 270, 0), TransformSpace.World);

            var TextX = TextNodeX.CreateComponent<Text3D>();
            TextX.SetFont(CoreAssets.Fonts.AnonymousPro, 30);
            TextX.TextEffect = TextEffect.Stroke;
            TextX.Text = "oX";

            //For oY
            var m_BoxNodeY = plotNode.CreateChild();
            var BoxNodeY = m_BoxNodeY.CreateChild();
            BoxNodeY.Position = new Vector3(0, 0, 0);
            BoxNodeY.Scale = new Vector3(1, 5, 1);

            var BoxY = BoxNodeY.CreateComponent<Box>();
            BoxY.Color = new Urho.Color(Urho.Color.Red, 0.8F);

            var TextNodeY = m_BoxNodeY.CreateChild();
            TextNodeY.Position = new Vector3(0.25F, 2.5F, -0.25F);
            TextNodeY.Rotate(new Quaternion(90, 180, 0), TransformSpace.World);

            var TextY = TextNodeY.CreateComponent<Text3D>();
            TextY.SetFont(CoreAssets.Fonts.AnonymousPro, 30);
            TextY.TextEffect = TextEffect.Stroke;
            TextY.Text = "oY";

            //For oZ
            var m_BoxNodeZ = plotNode.CreateChild();
            var BoxNodeZ = m_BoxNodeZ.CreateChild();
            BoxNodeZ.Position = new Vector3(0, 0, 0);
            BoxNodeZ.Scale = new Vector3(1, 1, 5);

            var BoxZ = BoxNodeZ.CreateComponent<Box>();
            BoxZ.Color = new Urho.Color(Urho.Color.Green, 0.8F);

            var TextNodeZ = m_BoxNodeZ.CreateChild();
            TextNodeZ.Position = new Vector3(0.25F, 0.25F, 2.5F);
            TextNodeZ.Rotate(new Quaternion(0, 180, 0), TransformSpace.World);

            var TextZ = TextNodeZ.CreateComponent<Text3D>();
            TextZ.SetFont(CoreAssets.Fonts.AnonymousPro, 30);
            TextZ.TextEffect = TextEffect.Stroke;
            TextZ.Text = "oZ";

            try
            {
                await plotNode.RunActionsAsync(new EaseOut(new RotateBy(2f, 0, 360, 0), 0));
            }
            catch (OperationCanceledException) { }
            movementsEnabled = true;
        }

        private void CreateCamera()
        {
            var cameraNode = scene.CreateChild();
            camera = cameraNode.CreateComponent<Camera>();
            cameraNode.Position = new Vector3(10, 15, 10) / 1.75f;
            cameraNode.Rotation = new Quaternion(-0.121f, 0.878f, -0.305f, -0.35f);

            Node lightNode = cameraNode.CreateChild();
            var light = lightNode.CreateComponent<Light>();
            light.LightType = LightType.Point;
            light.Range = 100;
            light.Brightness = 1.3f;
        }

        protected override void OnUpdate(float timeStep)
        {
            if (Input.NumTouches >= 1 && movementsEnabled)
            {
                var touch = Input.GetTouch(0);
                plotNode.Rotate(new Quaternion(0, -touch.Delta.X, 0), TransformSpace.Local);
            }
            base.OnUpdate(timeStep);
        }

        internal async Task RotateAsync(System.Numerics.Vector3 v)
        {
            if (v.X > 1 || v.X < -1)
                v.X = (float)Math.Round(v.X);
            v.X = (v.X) * 180;
            if (v.Y > 1 || v.Y < -1)
                v.Y = (float)Math.Round(v.Y);
            v.Y = (v.Y) * 180;
            if (v.Z > 1 || v.Z < -1)
                v.Z = (float)Math.Round(v.Z);
            v.Z = (v.Z) * 180;

            var delta_X = plotNode.Rotation.X - v.X;
            var delta_Y = plotNode.Rotation.Y - v.Y;
            var delta_Z = plotNode.Rotation.Z - v.Z;

            System.Console.WriteLine($"[X] CV:{plotNode.Rotation.X} |Delta:{delta_X} |NV:{v.X}");
            System.Console.WriteLine($"[Y] CV:{plotNode.Rotation.Y} |Delta:{delta_Y} |NV:{v.Y}");
            System.Console.WriteLine($"[Z] CV:{plotNode.Rotation.Z} |Delta:{delta_Z} |NV:{v.Z}");

            System.Console.WriteLine("----------------------");
            await plotNode.RunActionsAsync(new RotateBy(1F, v.X, v.Y, v.Z));
            //plotNode.RunActions(new RotateTo(1F, (v.X+1) * 180, (v.Y+1) * 180, (v.Z+1) * 180));


            //plotNode.Rotation = new Quaternion(v.X*180, v.Y*180, v.Z*180);
        }

        void SetupViewport()
        {
            var renderer = Renderer;
            var vp = new Viewport(Context, scene, camera, null);
            renderer.SetViewport(0, vp);
        }
    }
}