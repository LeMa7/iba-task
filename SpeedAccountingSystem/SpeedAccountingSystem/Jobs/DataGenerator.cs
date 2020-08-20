using Newtonsoft.Json;
using Quartz;
using SpeedAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SpeedAccountingSystem.Jobs
{
    public class DataGenerator : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Random rnd = new Random();
            var model = new SpeedSystemRecordModel()
            {
                Time = DateTime.Now,
                Speed = rnd.Next(0, 150),
                CarNumber = GenerateRandomCarNumber()
            };
            var list = JsonConvert.DeserializeObject<List<SpeedSystemRecordModel>>(File.ReadAllText(@"Data/SpeedSystemData.json"));
            list.Add(model);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            await File.WriteAllTextAsync(@"Data/SpeedSystemData.json", convertedJson);
        }

        private string GenerateRandomCarNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999) + " " + GetRandomCharacter() + GetRandomCharacter() + "-" + rnd.Next(1, 6);
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