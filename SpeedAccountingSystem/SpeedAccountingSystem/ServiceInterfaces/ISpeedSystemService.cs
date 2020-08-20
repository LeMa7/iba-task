using SpeedAccountingSystem.Models;
using System;
using System.Collections.Generic;

namespace SpeedAccountingSystem.ServiceInterfaces
{
    public interface ISpeedSystemService
    {
        IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed);

        IEnumerable<SpeedSystemRecordModel> GetMinAndMaxSpeedForDay(DateTime day);

        bool IsAccessDenied();
    }
}