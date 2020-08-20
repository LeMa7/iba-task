using Microsoft.Extensions.Configuration;
using SpeedAccountingSystem.Models;
using SpeedAccountingSystem.RepositoryInterfaces;
using SpeedAccountingSystem.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SpeedAccountingSystem.Services
{
    public class SpeedSystemService : ISpeedSystemService
    {
        private readonly ISpeedSystemRepository speedSystemRepository;
        private readonly IConfiguration configuration;

        public SpeedSystemService(ISpeedSystemRepository speedSystemRepository, IConfiguration configuration)
        {
            this.speedSystemRepository = speedSystemRepository;
            this.configuration = configuration;
        }

        public IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed) =>
            speedSystemRepository.GetOverspeedForDay(day, speed);

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxSpeedForDay(DateTime day) =>
            speedSystemRepository.GetMinAndMaxSpeedForDay(day);

        public bool IsAccessDenied()
        {
            var startAccessTime = GetTimeFromConfigurationString(configuration["RequestAccess:StartTime"]);
            var endAccessTime = GetTimeFromConfigurationString(configuration["RequestAccess:EndTime"]);
            return DateTime.Now.TimeOfDay <= startAccessTime || DateTime.Now.TimeOfDay >= endAccessTime;
        }

        private TimeSpan GetTimeFromConfigurationString(string time) =>
            DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
    }
}