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

        private int collectForSeconds;

        public int CollectForSeconds
        {
            get { return collectForSeconds; }
            set
            {
                collectForSeconds = value;
                paintValuesMax = collectForSeconds * paintValuesPerSecond;
            }
        }

        private int valuesPerSecond;

        public int ValuesPerSecond
        {
            get { return valuesPerSecond; }
            set
            {
                valuesPerSecond = value;
                paintScale = (double)valuesPerSecond / (double)paintValuesPerSecond;
            }
        }

        private int paintValuesPerSecond;
        private int paintValuesMax;
        private double paintScale;

        public GraphRenderer()
        {
            paintValuesPerSecond = 10;
            lastUpdate = DateTime.Now;
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        private double accelerometerScale = -2.5;

        private double gyroscopeScale = -0.08;

        private DateTime lastUpdate;

        public void PaintSurface(SKSurface surface, SKImageInfo info)
        {
            if(DateTime.Now - lastUpdate > TimeSpan.FromSeconds(1))
            {
                lastUpdate = DateTime.Now;
                PaintGrid(surface, info);

                if(Gesture != null)
                {
                    PaintAccelerometer(surface, info);
                    PaintGyroscope(surface, info);
                    PaintTimestamp(surface, info);
                }
            }
        }

        SKPaint gridPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 1,
            Color = SKColors.Black
        };

        SKPaint XPaintLine = new SKPaint
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

        SKPaint ZPaintLine = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 1,
            Color = SKColors.Green
        };

        public void PaintGrid(SKSurface surface, SKImageInfo info)
        {
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            canvas.DrawLine(0, info.Height / 4 * 1, info.Width, info.Height / 4 * 1, gridPaint); //x-axis accelerometer
            canvas.DrawText("Accelerometer", 5, 10, gridPaint);
            canvas.DrawLine(0, info.Height / 4 * 3, info.Width, info.Height / 4 * 3, gridPaint); //x-axis gyroscope
            canvas.DrawText("Gyroscope", 5, info.Height / 2 + 10, gridPaint);
        }

        public void PaintAccelerometer(SKSurface surface, SKImageInfo info)
        {
            SKCanvas canvas = surface.Canvas;

            SKPoint[] accelerometerX = new SKPoint[paintValuesMax];
            SKPoint[] accelerometerY = new SKPoint[paintValuesMax];
            SKPoint[] accelerometerZ = new SKPoint[paintValuesMax];

            for (int i = 0; i < paintValuesMax; i++)
            {
                int X = i * info.Width / (paintValuesMax - 1);
                accelerometerX[i].X = X;
                accelerometerY[i].X = X;
                accelerometerZ[i].X = X;

                int Y = Convert.ToInt32(Math.Floor(i * paintScale));
                if (Y < Gesture.GesturePointList.Count)
                {
                    accelerometerX[i].Y = Gesture.GesturePointList[Y].Accelerometer.X * (float)accelerometerScale + (info.Height / 4 * 1);
                    accelerometerY[i].Y = Gesture.GesturePointList[Y].Accelerometer.Y * (float)accelerometerScale + (info.Height / 4 * 1);
                    accelerometerZ[i].Y = Gesture.GesturePointList[Y].Accelerometer.Z * (float)accelerometerScale + (info.Height / 4 * 1);
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
        }

        public void PaintGyroscope(SKSurface surface, SKImageInfo info)
        {
            SKCanvas canvas = surface.Canvas;

            SKPoint[] gyroscopeX = new SKPoint[paintValuesMax];
            SKPoint[] gyroscopeY = new SKPoint[paintValuesMax];
            SKPoint[] gyroscopeZ = new SKPoint[paintValuesMax];

            for (int i = 0; i < paintValuesMax; i++)
            {
                int X = i * info.Width / (paintValuesMax - 1);
                gyroscopeX[i].X = X;
                gyroscopeY[i].X = X;
                gyroscopeZ[i].X = X;

                int Y = Convert.ToInt32(Math.Floor(i * paintScale));
                if (Y < Gesture.GesturePointList.Count)
                {
                    gyroscopeX[i].Y = Gesture.GesturePointList[Y].Gyroscope.X * (float)gyroscopeScale + (info.Height / 4 * 3);
                    gyroscopeY[i].Y = Gesture.GesturePointList[Y].Gyroscope.Y * (float)gyroscopeScale + (info.Height / 4 * 3);
                    gyroscopeZ[i].Y = Gesture.GesturePointList[Y].Gyroscope.Z * (float)gyroscopeScale + (info.Height / 4 * 3);
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
        }

        public void PaintTimestamp(SKSurface surface, SKImageInfo info)
        {
            if (Gesture == null || Gesture.GesturePointList.Count == 0)
            {
                return;
            }

            string TimeStamp0 = Gesture.GesturePointList[Gesture.GesturePointList.Count - 1].TimeStamp.Substring(8);
            string TimeStamp1 = Gesture.GesturePointList[0].TimeStamp.Substring(8);

            SKCanvas canvas = surface.Canvas;

            canvas.DrawText(TimeStamp0, info.Width - 40, info.Height / 2, gridPaint);
            canvas.DrawText(TimeStamp1, 0, info.Height / 2, gridPaint);
        }

        public event EventHandler RefreshRequested;
    }
}