using System;
using System.Collections.Specialized;

using SkiaSharp;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Renderers
{
    public class GraphRenderer : IRenderer
    {
        private Gesture gesture;

        public Gesture Gesture
        {
            get
            {
                return gesture;
            }
            set
            {
                gesture = value;
                Gesture.GesturePointList.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChangedMethod);
            }
        }

        public int ValuesMax { get; set; }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        private double accelerometerScale = -2.5;

        private double gyroscopeScale = -0.08;

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
        }

        public void PaintAccelerometer(SKSurface surface, SKImageInfo info)
        {
            SKCanvas canvas = surface.Canvas;

            SKPoint[] accelerometerX = new SKPoint[ValuesMax];
            SKPoint[] accelerometerY = new SKPoint[ValuesMax];
            SKPoint[] accelerometerZ = new SKPoint[ValuesMax];

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

            for (int i = 0; i < ValuesMax; i++)
            {
                if (ValuesMax > 1)
                {
                    int X = i * info.Width / (ValuesMax - 1);
                    accelerometerX[i].X = X;
                    accelerometerY[i].X = X;
                    accelerometerZ[i].X = X;
                }
                else
                {
                    accelerometerX[i].X = 0;
                    accelerometerY[i].X = 0;
                    accelerometerZ[i].X = 0;
                }

                if (Gesture != null && i < Gesture.GesturePointList.Count)
                {
                    accelerometerX[i].Y = Gesture.GesturePointList[i].Accelerometer.X * (float)accelerometerScale + (info.Height / 4 * 1);
                    accelerometerY[i].Y = Gesture.GesturePointList[i].Accelerometer.Y * (float)accelerometerScale + (info.Height / 4 * 1);
                    accelerometerZ[i].Y = Gesture.GesturePointList[i].Accelerometer.Z * (float)accelerometerScale + (info.Height / 4 * 1);
                }
                else
                {
                    accelerometerX[i].Y = (info.Height / 4 * 1);
                    accelerometerY[i].Y = (info.Height / 4 * 1);
                    accelerometerZ[i].Y = (info.Height / 4 * 1);
                }

                if (i > 0)
                {
                    canvas.DrawLine(accelerometerX[i - 1], accelerometerX[i], XPaintLine);
                    canvas.DrawLine(accelerometerY[i - 1], accelerometerY[i], YPaintLine);
                    canvas.DrawLine(accelerometerZ[i - 1], accelerometerZ[i], ZPaintLine);
                }
            }

            canvas.DrawPoints(SKPointMode.Points, accelerometerX, XPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, accelerometerY, YPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, accelerometerZ, ZPaintPoint);
        }

        public void PaintGyroscope(SKSurface surface, SKImageInfo info)
        {
            SKCanvas canvas = surface.Canvas;

            SKPoint[] gyroscopeX = new SKPoint[ValuesMax];
            SKPoint[] gyroscopeY = new SKPoint[ValuesMax];
            SKPoint[] gyroscopeZ = new SKPoint[ValuesMax];

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

            for (int i = 0; i < ValuesMax; i++)
            {
                if(ValuesMax > 1)
                {
                    int X = i * info.Width / (ValuesMax - 1);
                    gyroscopeX[i].X = X;
                    gyroscopeY[i].X = X;
                    gyroscopeZ[i].X = X;
                }
                else
                {
                    gyroscopeX[i].X = 0;
                    gyroscopeY[i].X = 0;
                    gyroscopeZ[i].X = 0;
                }

                if (Gesture != null && i < Gesture.GesturePointList.Count)
                {
                    gyroscopeX[i].Y = Gesture.GesturePointList[i].Gyroscope.X * (float)gyroscopeScale + (info.Height / 4 * 3);
                    gyroscopeY[i].Y = Gesture.GesturePointList[i].Gyroscope.Y * (float)gyroscopeScale + (info.Height / 4 * 3);
                    gyroscopeZ[i].Y = Gesture.GesturePointList[i].Gyroscope.Z * (float)gyroscopeScale + (info.Height / 4 * 3);
                }
                else
                {
                    gyroscopeX[i].Y = (info.Height / 4 * 3);
                    gyroscopeY[i].Y = (info.Height / 4 * 3);
                    gyroscopeZ[i].Y = (info.Height / 4 * 3);
                }

                if (i > 0)
                {
                    canvas.DrawLine(gyroscopeX[i - 1], gyroscopeX[i], XPaintLine);
                    canvas.DrawLine(gyroscopeY[i - 1], gyroscopeY[i], YPaintLine);
                    canvas.DrawLine(gyroscopeZ[i - 1], gyroscopeZ[i], ZPaintLine);
                }
            }

            canvas.DrawPoints(SKPointMode.Points, gyroscopeX, XPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, gyroscopeY, YPaintPoint);
            canvas.DrawPoints(SKPointMode.Points, gyroscopeZ, ZPaintPoint);
        }

        public void PaintTimestamp(SKSurface surface, SKImageInfo info)
        {
            if (Gesture == null || Gesture.GesturePointList.Count == 0)
            {
                return;
            }

            int GesturePointsCount = Gesture.GesturePointList.Count;
            TimeSpan TimeSpan0 = TimeSpan.FromSeconds(Gesture.GesturePointList[GesturePointsCount - 1].TimeStamp);
            TimeSpan TimeSpan1 = TimeSpan.FromSeconds(Gesture.GesturePointList[0].TimeStamp);

            DateTime TimeStamp0 = new DateTime(1970, 1, 1) + TimeSpan0;
            DateTime TimeStamp1 = new DateTime(1970, 1, 1) + TimeSpan1;

            SKCanvas canvas = surface.Canvas;

            SKPaint gridPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Black
            };

            canvas.DrawText(TimeStamp0.ToString("mm:ss.fff"), info.Width - 60, info.Height / 2, gridPaint);
            canvas.DrawText(TimeStamp1.ToString("mm:ss.fff"), 0, info.Height / 2, gridPaint);
        }

        public event EventHandler RefreshRequested;
    }
}