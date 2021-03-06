﻿using Quartz;
using Quartz.Impl;
using SpeedAccountingSystem.Jobs;
using System;

namespace SpeedAccountingSystem.Schedulers
{
    public class DataGeneratorScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<DataGenerator>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMilliseconds(1))
                    .WithRepeatCount(500000))
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }

        public static async void PauseAll()
        {
            ISchedulerFactory schfack = new StdSchedulerFactory();
            IScheduler scheduler = await schfack.GetScheduler();
            await scheduler.PauseAll();
        }
    }
}