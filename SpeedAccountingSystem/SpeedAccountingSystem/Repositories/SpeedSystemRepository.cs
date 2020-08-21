using CsvHelper;
using SpeedAccountingSystem.Models;
using SpeedAccountingSystem.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SpeedAccountingSystem.Repositories
{
    public class SpeedSystemRepository : ISpeedSystemRepository
    {
        private const string DataPath = @"Data/SpeedSystemData.csv";

        public IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed)
        {
            var models = new List<SpeedSystemRecordModel>();
            using (var stream = File.Open(DataPath, FileMode.Open, FileAccess.Read, FileShare.Write))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                models = csv.GetRecords<SpeedSystemRecordModel>().Where(x => x.Time.Date == day.Date && x.Speed > speed).ToList();
            }

            return models;
        }

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxSpeedForDay(DateTime day)
        {
            var models = new List<SpeedSystemRecordModel>();
            using (var reader = new StreamReader(DataPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                models = csv.GetRecords<SpeedSystemRecordModel>().ToList();
            }
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