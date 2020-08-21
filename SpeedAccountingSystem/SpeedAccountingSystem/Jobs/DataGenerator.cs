using System.IO;
using Quartz;
using CsvHelper;
using SpeedAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

namespace SpeedAccountingSystem.Jobs
{
    public class DataGenerator : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Random rnd = new Random();
            var model = new List<SpeedSystemRecordModel>()
            {
                new SpeedSystemRecordModel{
                Time = DateTime.Now,
                Speed = rnd.Next(0, 150),
                CarNumber = GenerateRandomCarNumber()
                }
            };

            using (var stream = File.Open(@"Data/SpeedSystemData.csv", FileMode.Append, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                await csv.WriteRecordsAsync(model);
            }
        }

        private string GenerateRandomCarNumber()
        {
            Random rnd = new Random();
            return rnd.Next(0, 9).ToString()
                + rnd.Next(0, 9).ToString()
                + rnd.Next(0, 9).ToString()
                + rnd.Next(0, 9).ToString()
                + " "
                + GetRandomCharacter()
                + GetRandomCharacter()
                + "-"
                + rnd.Next(1, 6);
        }

        private char GetRandomCharacter()
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rnd = new Random();
            int index = rnd.Next(text.Length);
            return text[index];
        }
    }
}