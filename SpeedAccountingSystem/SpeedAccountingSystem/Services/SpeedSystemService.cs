using SpeedAccountingSystem.Models;
using SpeedAccountingSystem.RepositoryInterfaces;
using SpeedAccountingSystem.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace SpeedAccountingSystem.Services
{
    public class SpeedSystemService : ISpeedSystemService
    {
        private readonly ISpeedSystemRepository speedSystemRepository;

        public SpeedSystemService(ISpeedSystemRepository speedSystemRepository)
        {
            this.speedSystemRepository = speedSystemRepository;
        }

        public IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed)
        {
            return speedSystemRepository.GetOverspeedForDay(day, speed);
        }

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxForDay(DateTime day)
        {
            return speedSystemRepository.GetMinAndMaxForDay(day);
        }
    }
}