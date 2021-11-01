﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DeviceTester.Helper
{
    public class CameraSettings
    {

        //Focus
        public double FocusValue { get; set; }

        //Exposure
        public double Offset { get; set; }
        public double Duration { get; set; }
        public double ISO { get; set; }
        public double Bias { get; set; }

        //White balance
        public double Temp { get; set; }
        public double Tint { get; set; }

        //Bracket Capture
        public Boolean CaptureBracket { get; set; }


        public CameraSettings(){}

    }
    public enum FocusTypes { Auto,Locked}
    public enum Exposureypes { Auto,Locked,Custom}
    public enum WhiteTypes { Auto,Locked }
}
