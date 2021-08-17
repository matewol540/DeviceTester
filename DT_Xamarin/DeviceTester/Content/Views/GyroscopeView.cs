
using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace DeviceTester.Content.Views
{
    public class GyroscopeView : ContentView
    {
        //Main polygon
        Ellipse roadMap;

        //To strenght effect of moving pointer
        const int Maximum = 30;
        const double Range = 150;
        //Trinagle vertex
        Point left_Top = new Point();
        Point right_Top = new Point();
        Point top = new Point();
        Point left_Bottom = new Point();
        Point right_Bottom = new Point();
        Point Bottom = new Point();

        //Pointer
        Ellipse ell = new Ellipse()
        {
            Stroke = Brush.WhiteSmoke,
            WidthRequest = 6,
            HeightRequest = 6,
            Fill = Brush.DarkRed
        };

        public GyroscopeView()
        {
            Content = new AbsoluteLayout();
            this.Content.SizeChanged += SizedChanged;
            Initialize();
        }

        internal void Initialize()
        {
            (this.Content as AbsoluteLayout).Children.Clear();
            roadMap = new Ellipse
            {
                StrokeLineJoin = PenLineJoin.Round,
                StrokeThickness = 10,
                Stroke = Brush.White,
                Fill = new RadialGradientBrush(
                    new GradientStopCollection()
                        {
                        new GradientStop(Color.Orange,0.1F),
                        new GradientStop(Color.White,1.0F)
                        },
                    new Point(0.5, 0.5),
                    0.1
                    )
            };
            (this.Content as AbsoluteLayout).Children.Add(roadMap, new Xamarin.Forms.Rectangle(0.5, 0.5, Range*2, Range*2), AbsoluteLayoutFlags.PositionProportional);
            (this.Content as AbsoluteLayout).Children.Add(ell, new Xamarin.Forms.Rectangle(0.5, 0.5, 15, 15), AbsoluteLayoutFlags.PositionProportional);
        }

        public void SizedChanged(object sender, EventArgs ea)
        {
            var Content = sender as AbsoluteLayout;
            var centerPoint = new Point(Content.Width / 2, Content.Height / 2);

            top.X = Bottom.X = centerPoint.X;
            left_Bottom.X = left_Top.X = centerPoint.X - (Range * Math.Sqrt(3)) / 2;
            right_Bottom.X = right_Top.X = centerPoint.X + (Range * Math.Sqrt(3)) / 2;

            top.Y = centerPoint.Y - Range;
            Bottom.Y = centerPoint.Y + Range;
            left_Top.Y = right_Top.Y = centerPoint.Y - Range / 2;
            left_Bottom.Y = right_Bottom.Y = centerPoint.Y + Range / 2;
        }

        public async void MovePointer(double x, double y, double z)
        {
            ConvertToPoint(x, y, z, out Point StopPoint);
            await this.ell.TranslateTo(StopPoint.X, StopPoint.Y, 100, Easing.Linear);
            UpdateFill(StopPoint, x, y, z);
        }

        private void ConvertToPoint(double x, double y, double z, out Point StopPoint)
        {
            StopPoint = new Point();
            var Xcoord = 0.0;
            var Ycoord = 0.0;
            x = x / Maximum * Range;
            y = y / Maximum * Range;
            z = z / Maximum * Range;

            Xcoord += (-x + y) / Math.Sqrt(2);
            Ycoord -= z + ((-y + -x) / Math.Sqrt(2));
            StopPoint.X = Xcoord;
            StopPoint.Y = Ycoord;
        }

        private void UpdateFill(Point StopPoint, double x, double y, double z)
        {
            if (StopPoint.X == 0 && StopPoint.Y == 0)
            {
                roadMap.Fill = new RadialGradientBrush(
                    new GradientStopCollection()
                        {
                        new GradientStop(Color.Orange,0.1F),
                        new GradientStop(Color.White,1.0F)
                        },
                        new Point(0.5, 0.5),
                        0.1
                    );
                return;
            }
            else
            {
                var intersectionPoint = CheckIntersectionWithBounds(StopPoint, x, y, z);
                if (StopPoint.X == 21 && StopPoint.Y == 37)
                    return;
                roadMap.Fill = new RadialGradientBrush(
                    new GradientStopCollection()
                        {
                        new GradientStop(Color.Orange,0.1F),
                        new GradientStop(Color.White,1.0F)
                        },
                        intersectionPoint,
                        0.1
                    );
            }
        }

        private Point CheckIntersectionWithBounds(Point StopPoint, double x, double y, double z)
        {
            var GradientStopPoint = new Point();

            var x_Center = Content.Width / 2;
            var y_Center = Content.Height / 2;
            if (Content.Width == -1 || Content.Height == -1)
                return new Point(21,37);
            var Marker_Point_X = StopPoint.X + x_Center;
            var Marker_Point_Y = StopPoint.Y + y_Center;
            var xModifier = 1;
            if (StopPoint.X < 0) xModifier = -1;
            if (x_Center != Marker_Point_X)
            {
                var upper = (decimal)(y_Center - Marker_Point_Y);
                var down = (decimal)(x_Center - Marker_Point_X);
                var PointerDirection = (double)(upper / down);
                var function_Angle = Math.Atan(PointerDirection); //In radians
                GradientStopPoint.X = x_Center + Math.Cos(function_Angle) * Range * xModifier;
                GradientStopPoint.Y = y_Center + Math.Sin(function_Angle) * Range * xModifier;
            }
            else
            {
                GradientStopPoint.X = x_Center;
                if (StopPoint.Y > 0)
                    GradientStopPoint.Y = y_Center + Range;
                else
                    GradientStopPoint.Y = y_Center - Range;
            }

            //for proportional
            GradientStopPoint.X /= Content.Width;
            GradientStopPoint.Y /= Content.Height;

            return GradientStopPoint;
        }
    }
}

