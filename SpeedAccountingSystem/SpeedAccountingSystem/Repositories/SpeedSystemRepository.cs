using Newtonsoft.Json;
using SpeedAccountingSystem.Models;
using SpeedAccountingSystem.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeedAccountingSystem.Repositories
{
    public class SpeedSystemRepository : ISpeedSystemRepository
    {
        private const string DataPath = @"Data/SpeedSystemData.json";

        public IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed)
        {
            string jsonString = File.ReadAllText(DataPath);
            return JsonConvert.DeserializeObject<List<SpeedSystemRecordModel>>(jsonString)
                .Where(x => x.Time.Date == day.Date && x.Speed > speed) ?? Enumerable.Empty<SpeedSystemRecordModel>();
        }

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxForDay(DateTime day)
        {
            string jsonString = File.ReadAllText(DataPath);
            var models = JsonConvert.DeserializeObject<List<SpeedSystemRecordModel>>(jsonString).Where(x => x.Time.Date == day.Date);
            if (!models.Any())
            {
                return Enumerable.Empty<SpeedSystemRecordModel>();
            }

            double min = models.Min(x => x.Speed);
            double max = models.Max(x => x.Speed);
            return new List<SpeedSystemRecordModel>()
            {
                models.Where(x => x.Speed == min).First(),
                models.Where(x => x.Speed == max).First()
            };
        }
    }
}