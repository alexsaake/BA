using ClientSerializerTester.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ClientSerializerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<DeviceMovement> a = new List<DeviceMovement>();
            a.Add(new DeviceMovement(new Accelerometer(), new Barometer(), new Geolocation(), new Gyroscope()));


            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonString = JsonSerializer.Serialize(a, options);

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");


        }
    }
}
