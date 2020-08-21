using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpeedAccountingSystem.Models;
using SpeedAccountingSystem.Schedulers;
using SpeedAccountingSystem.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
            if (speedSystemService.IsAccessDenied())
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Enumerable.Empty<SpeedSystemRecordModel>();
            }

            return speedSystemService.GetOverspeedForDay(day, speed);
        }

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxSpeedForDay(DateTime day)
        {
            if (speedSystemService.IsAccessDenied())
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return Enumerable.Empty<SpeedSystemRecordModel>();
            }

            return speedSystemService.GetMinAndMaxSpeedForDay(day);
        }

        public void RunGenerator()
        {
            DataGeneratorScheduler.Start();
        }

        public void StopGenerator()
        {
            DataGeneratorScheduler.PauseAll();
        }
    }
}