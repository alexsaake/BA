using Backend_ML_Gesture_Common;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Backend_ML_Gesture_ModelGenerator
{
    class Program
    {
        private static string _sqlConnectionString;
        private static readonly string fileName = "gesture.zip";
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            var configuration = builder.Build();

            _sqlConnectionString = configuration["connectionString"];

            var items = File.ReadAllLines("./gestureData.csv")
                .Skip(1)
                .Select(line => line.Split(","))
                .Select(i => new GestureData
                {
                    Type = i[0],
                    FixedAcidity = Parse(i[1]),
                    VolatileAcidity = Parse(i[2]),
                    CitricAcid = Parse(i[3]),
                    ResidualSugar = Parse(i[4]),
                    Chlorides = Parse(i[5]),
                    FreeSulfurDioxide = Parse(i[6]),
                    TotalSulfurDioxide = Parse(i[7]),
                    Density = Parse(i[8]),
                    Ph = Parse(i[9]),
                    Sulphates = Parse(i[10]),
                    Alcohol = Parse(i[11]),
                    Quality = Parse(i[12])
                });

            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                var insertComand = @"INSERT INTO gesturedata.dbo.GestureData VALUES
                    (@type, @fixedAcidity, @volatileAcidity, @citricAcid, @residualSugar, @chlorides, @freeSulfurDioxide, @totalSulfurDioxide, @density, @ph, @sulphates, @alcohol, @quality);";

                var selectCommand = "SELECT COUNT(*) FROM gesturedata.dbo.GestureData";

                var selectSqlCommand = new SqlCommand(selectCommand, connection);

                var results = (int)selectSqlCommand.ExecuteScalar();

                if(results > 0)
                {
                    var deleteCommand = "DELETE FROM gesturedata.dbo.GestureData";

                    var deleteSqlCommand = new SqlCommand(deleteCommand, connection);

                    deleteSqlCommand.ExecuteNonQuery();
                }

                foreach (var item in items)
                {
                    var command = new SqlCommand(insertComand, connection);

                    command.Parameters.AddWithValue("@type", item.Type);
                    command.Parameters.AddWithValue("@fixedAcidity", item.FixedAcidity);
                    command.Parameters.AddWithValue("@volatileAcidity", item.VolatileAcidity);
                    command.Parameters.AddWithValue("@citricAcid", item.CitricAcid);
                    command.Parameters.AddWithValue("@residualSugar", item.ResidualSugar);
                    command.Parameters.AddWithValue("@chlorides", item.Chlorides);
                    command.Parameters.AddWithValue("@freeSulfurDioxide", item.FreeSulfurDioxide);
                    command.Parameters.AddWithValue("@totalSulfurDioxide", item.TotalSulfurDioxide);
                    command.Parameters.AddWithValue("@density", item.Density);
                    command.Parameters.AddWithValue("@ph", item.Ph);
                    command.Parameters.AddWithValue("@sulphates", item.Sulphates);
                    command.Parameters.AddWithValue("@alcohol", item.Alcohol);
                    command.Parameters.AddWithValue("@quality", item.Quality);

                    command.ExecuteNonQuery();
                }

                var data = new List<GestureData>();

                using (var conn = new SqlConnection(_sqlConnectionString))
                {
                    conn.Open();

                    var selectCmd = "SELECT * FROM gesturedata.dbo.GestureData";

                    var sqlCommand = new SqlCommand(selectCmd, conn);

                    var reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new GestureData
                        {
                            Type = reader.GetValue(0).ToString(),
                            FixedAcidity = Parse(reader.GetValue(1).ToString()),
                            VolatileAcidity = Parse(reader.GetValue(2).ToString()),
                            CitricAcid = Parse(reader.GetValue(3).ToString()),
                            ResidualSugar = Parse(reader.GetValue(4).ToString()),
                            Chlorides = Parse(reader.GetValue(5).ToString()),
                            FreeSulfurDioxide = Parse(reader.GetValue(6).ToString()),
                            TotalSulfurDioxide = Parse(reader.GetValue(7).ToString()),
                            Density = Parse(reader.GetValue(8).ToString()),
                            Ph = Parse(reader.GetValue(9).ToString()),
                            Sulphates = Parse(reader.GetValue(10).ToString()),
                            Alcohol = Parse(reader.GetValue(11).ToString()),
                            Quality = Parse(reader.GetValue(12).ToString())
                        });
                    }
                }

                var context = new MLContext();

                var mlData = context.Data.LoadFromEnumerable(data);

                var testTrainSplit = context.Data.TrainTestSplit(mlData);

                var dataPreview = testTrainSplit.TestSet.Preview();

                var pipeline = context.Transforms.Categorical.OneHotEncoding("TypeOneHot", "Type")
                    .Append(context.Transforms.Concatenate("Features", "FixedAcidity", "VolatileAcidity", "CitricAcid", "ResidualSugar", "Chlorides", "FreeSulfurDioxide", "TotalSulfurDioxide", "Density", "Ph", "Sulphates", "Alcohol"))
                    .Append(context.Regression.Trainers.FastTree(labelColumnName: "Quality"));

                var model = pipeline.Fit(testTrainSplit.TrainSet);

                using (var stream = File.Create(fileName))
                {
                    context.Model.Save(model, null, stream);
                }
            }
        }

        private static float Parse(string value)
        {
            return float.TryParse(value, out float parsedValue) ? parsedValue : default(float);
        }
    }
}
