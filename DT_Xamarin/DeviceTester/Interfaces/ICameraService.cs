using System;
namespace DeviceTester.Interfaces
{
    public interface ICameraService
    {
         double DurationMinValue { get; } 
         double DurationMaxValue { get; } 
         double ISOMinValue { get; } 
         double ISOMaxValue { get; } 
         double BiasMinValue { get; } 
         double BiasMaxValue { get; }


        void FocusMode_ValueChanged();
        void FocusValue_ValueChanged();
        void ExposureMode_ValueChanged();
        void DurationValue_ValueChanged();
        void ISOValue_ValueChanged();
        void BiasValue_ValueChanged();
        void WhiteBalanceMode_ValueChanged();
        void TempValue_ValueChanged();
        void TintValue_ValueChanged();
    }
}
