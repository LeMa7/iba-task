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

        public IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed)
        {
            if (IsAccessDenied())
            {
                throw new UnauthorizedAccessException();
            }

            return speedSystemRepository.GetOverspeedForDay(day, speed);
        }

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxForDay(DateTime day)
        {
            if (IsAccessDenied())
            {
                throw new UnauthorizedAccessException();
            }

            return speedSystemRepository.GetMinAndMaxForDay(day);
        }

        private TimeSpan GetTimeFromConfigurationString(string time)
        {
            return DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
        }

        private bool IsAccessDenied()
        {
            var startAccessTime = GetTimeFromConfigurationString(configuration["RequestAccess:StartTime"]);
            var endAccessTime = GetTimeFromConfigurationString(configuration["RequestAccess:EndTime"]);
            return DateTime.Now.TimeOfDay < startAccessTime || DateTime.Now.TimeOfDay > endAccessTime;
        }
    }
}