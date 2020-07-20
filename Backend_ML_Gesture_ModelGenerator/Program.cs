using System;
using Microsoft.ML;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Backend_ML_Gesture_Common.Models;

namespace Backend_ML_Gesture_ModelGenerator
{
    class Program
    {
        private static readonly string FileName = "gesture.zip";
        static void Main(string[] args)
        {
            List<DeviceMovement> DeviceMovementData = (List<DeviceMovement>)File.ReadAllLines("./gestureData.csv")
                .Skip(1)
                .Select(line => line.Split(","))
                .Select(i => new DeviceMovement(
                    new Accelerometer()
                        {
                            X = ParseFloat(i[1]),
                            Y = ParseFloat(i[2]),
                            Z = ParseFloat(i[3])
                        },
                    new Barometer()
                    {
                        Pressure = ParseDouble(i[4])
                    },
                    new Geolocation()
                    {
                        Latitude = ParseDouble(i[5]),
                        Longitude = ParseDouble(i[6]),
                        Altitude = ParseDouble(i[7])
                    },
                    new Gyroscope()
                    {
                        X = ParseFloat(i[8]),
                        Y = ParseFloat(i[9]),
                        Z = ParseFloat(i[10])
                    },
                    new HeartRateMonitor()
                    {
                        HeartRate = ParseInt(i[11])
                    },
                    new LightSensor()
                    {
                        Level = ParseFloat(i[12])
                    })
                {
                    TimeStamp = ParseDateTime(i[0])
                });

            var MLContext = new MLContext();

            var MLData = MLContext.Data//Data.LoadFromEnumerable(DeviceMovementData);

            var testTrainSplit = MLContext.Data.TrainTestSplit(MLData);

            var dataPreview = testTrainSplit.TestSet.Preview();

            var pipeline = MLContext.Transforms.Categorical.OneHotEncoding("TypeOneHot", "Type")
                .Append(MLContext.Transforms.Concatenate("Features", "FixedAcidity", "VolatileAcidity", "CitricAcid", "ResidualSugar", "Chlorides", "FreeSulfurDioxide", "TotalSulfurDioxide", "Density", "Ph", "Sulphates", "Alcohol"))
                .Append(MLContext.Regression.Trainers.FastTree(labelColumnName: "Quality"));

            var model = pipeline.Fit(testTrainSplit.TrainSet);

            using (var stream = File.Create(FileName))
            {
                MLContext.Model.Save(model, null, stream);
            }
        }

        private static float ParseFloat(string value)
        {
            return float.TryParse(value, out float parsedValue) ? parsedValue : default(float);
        }

        private static double ParseDouble(string value)
        {
            return double.TryParse(value, out double parsedValue) ? parsedValue : default(double);
        }

        private static int ParseInt(string value)
        {
            return int.TryParse(value, out int parsedValue) ? parsedValue : default(int);
        }

        private static DateTime ParseDateTime(string value)
        {
            return DateTime.TryParse(value, out DateTime parsedValue) ? parsedValue : default(DateTime);
        }
    }
}
