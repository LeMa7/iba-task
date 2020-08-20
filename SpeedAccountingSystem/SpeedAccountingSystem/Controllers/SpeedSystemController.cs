using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpeedAccountingSystem.Models;
using SpeedAccountingSystem.Schedulers;
using SpeedAccountingSystem.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

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
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }

            return speedSystemService.GetOverspeedForDay(day, speed);
        }

        public IEnumerable<SpeedSystemRecordModel> GetMinAndMaxSpeedForDay(DateTime day)
        {
            DataGeneratorScheduler.Start();
            if (speedSystemService.IsAccessDenied())
            {
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }

            return speedSystemService.GetMinAndMaxSpeedForDay(day);
        }
    }
}