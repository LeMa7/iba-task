using SpeedAccountingSystem.Models;
using System;
using System.Collections.Generic;

namespace SpeedAccountingSystem.RepositoryInterfaces
{
    public interface ISpeedSystemRepository
    {
        IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed);

        IEnumerable<SpeedSystemRecordModel> GetMinAndMaxForDay(DateTime day);
    }
}