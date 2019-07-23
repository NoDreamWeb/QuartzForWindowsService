using Quartz;
using Quartz.Impl;
using QuartzForWindowsService.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace QuartzForWindowsService
{
    partial class Service1 : ServiceBase
    {
        private IScheduler _scheduler;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Grab the Scheduler instance from the Factory
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            _scheduler = factory.GetScheduler().GetAwaiter().GetResult();

            // and start it off
            _scheduler.Start();

            RunProgramRunExample().GetAwaiter().GetResult();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            _scheduler.Shutdown();
        }


        private async Task RunProgramRunExample()
        {
            try
            {
#region job1
                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 5 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithCronSchedule("0/5 * * * * ?")
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await _scheduler.ScheduleJob(job, trigger);
#endregion

                // TODO Defined other jobs here
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }
    }
}
