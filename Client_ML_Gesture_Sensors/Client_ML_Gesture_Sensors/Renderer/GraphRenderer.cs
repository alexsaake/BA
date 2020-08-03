using Client_ML_Gesture_Sensors.Models;
using SkiaSharp;
using System;

namespace Client_ML_Gesture_Sensors.Renderers
{
    public class GraphRenderer : IRenderer
    {
        Gesture Gesture;
        int Sampling;

        double accelerometerScale = -2.5;

        double gyroscopeScale = -0.08;

        public void PaintSurface(SKSurface surface, SKImageInfo info)
        {
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            SKPaint gridPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Black
            };

            canvas.DrawLine(0, info.Height / 4 * 1, info.Width, info.Height / 4 * 1, gridPaint); //x-axis accelerometer
            canvas.DrawText("Accelerometer", 5, 10, gridPaint);
            canvas.DrawLine(0, info.Height / 4 * 3, info.Width, info.Height / 4 * 3, gridPaint); //x-axis gyroscope
            canvas.DrawText("Gyroscope", 5, info.Height / 2 + 10, gridPaint);



            SKPoint[] accelerometerX = new SKPoint[Sampling];
            SKPoint[] accelerometerY = new SKPoint[Sampling];
            SKPoint[] accelerometerZ = new SKPoint[Sampling];
            SKPoint[] gyroscopeX = new SKPoint[Sampling];
            SKPoint[] gyroscopeY = new SKPoint[Sampling];
            SKPoint[] gyroscopeZ = new SKPoint[Sampling];

            SKPaint XPaintLine = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Blue
            };

            SKPaint XPaintPoint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Blue
            };

            SKPaint YPaintLine = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Red
            };

            SKPaint YPaintPoint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Red
            };

            SKPaint ZPaintLine = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Green
            };

            SKPaint ZPaintPoint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Green
            };

            for (int i = 0; i < Sampling; i++)
            {
                accelerometerX[i].X = i * (info.Width / Sampling);
                accelerometerY[i].X = i * (info.Width / Sampling);
                accelerometerZ[i].X = i * (info.Width / Sampling);
                gyroscopeX[i].X = i * (info.Width / Sampling);
                gyroscopeY[i].X = i * (info.Width / Sampling);
                gyroscopeZ[i].X = i * (info.Width / Sampling);

                if (Gesture != null && i < Gesture.GesturePointList.Count)
                {
                    accelerometerX[i].Y = Gesture.GesturePointList[i].Accelerometer.X * (float)accelerometerScale + (info.Height / 4 * 1);
                    accelerometerY[i].Y = Gesture.GesturePointList[i].Accelerometer.Y * (float)accelerometerScale + (info.Height / 4 * 1);
                    accelerometerZ[i].Y = Gesture.GesturePointList[i].Accelerometer.Z * (float)accelerometerScale + (info.Height / 4 * 1);
                    gyroscopeX[i].Y = Gesture.GesturePointList[i].Gyroscope.X * (float)gyroscopeScale + (info.Height / 4 * 3);
                    gyroscopeY[i].Y = Gesture.GesturePointList[i].Gyroscope.Y * (float)gyroscopeScale + (info.Height / 4 * 3);
                    gyroscopeZ[i].Y = Gesture.GesturePointList[i].Gyroscope.Z * (float)gyroscopeScale + (info.Height / 4 * 3);
                }
                else
                {
                    accelerometerX[i].Y = (info.Height / 4 * 1);
                    accelerometerY[i].Y = (info.Height / 4 * 1);
                    accelerometerZ[i].Y = (info.Height / 4 * 1);
                    gyroscopeX[i].Y = (info.Height / 4 * 3);
                    gyroscopeY[i].Y = (info.Height / 4 * 3);
                    gyroscopeZ[i].Y = (info.Height / 4 * 3);
                }

                if(i > 0)
                {
                    canvas.DrawLine(accelerometerX[i - 1], accelerometerX[i], XPaintLine);
                    canvas.DrawLine(accelerometerY[i - 1], accelerometerY[i], YPaintLine);
                    canvas.DrawLine(accelerometerZ[i - 1], accelerometerZ[i], ZPaintLine);
                    canvas.DrawLine(gyroscopeX[i - 1], gyroscopeX[i], XPaintLine);
                    canvas.DrawLine(gyroscopeY[i - 1], gyroscopeY[i], YPaintLine);
                    canvas.DrawLine(gyroscopeZ[i - 1], gyroscopeZ[i], ZPaintLine);
                }
            }

            canvas.DrawPoints(SKPointMode.Points, accelerometerX, XPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, accelerometerY, YPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, accelerometerZ, ZPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, gyroscopeX, XPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, gyroscopeY, YPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, gyroscopeZ, ZPaintPoint);



            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        public void DrawGraph(Gesture gesture, int sampling)
        {
            Gesture = gesture;
            Sampling = sampling;

            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RefreshRequested;
    }
}