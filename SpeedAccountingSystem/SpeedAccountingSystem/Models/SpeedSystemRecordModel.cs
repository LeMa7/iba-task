using System;

namespace SpeedAccountingSystem.Models
{
    public class SpeedSystemRecordModel
    {
        public DateTime Time { get; set; }

        public string CarNumber { get; set; }

        public double Speed { get; set; }
    }
}