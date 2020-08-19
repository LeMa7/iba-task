using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpeedAccountingSystem.Models;
using SpeedAccountingSystem.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace SpeedAccountingSystem.Controllers
{
    public class SpeedSystemController : Controller
    {
        private readonly ILogger<SpeedSystemController> _logger;
        private readonly ISpeedSystemService speedSystemService;

        public SpeedSystemController(ILogger<SpeedSystemController> logger, ISpeedSystemService speedSystemService)
        {
            _logger = logger;
            this.speedSystemService = speedSystemService;
        }

        public IEnumerable<SpeedSystemRecordModel> GetOverspeedForDay(DateTime day, double speed)
        {
            return speedSystemService.GetOverspeedForDay(day, speed);
        }

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxForDay(DateTime day)
        {
            return speedSystemService.GetMinAndMaxForDay(day);
        }

        public string Index()
        {
            return "Hi";
        }
    }
}