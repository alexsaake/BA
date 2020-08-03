using SkiaSharp;
using System;

namespace Client_ML_Gesture_Sensors.Renderers
{
    interface IRenderer
    {
        void PaintSurface(SKSurface surface, SKImageInfo info);
        event EventHandler RefreshRequested;
    }
}