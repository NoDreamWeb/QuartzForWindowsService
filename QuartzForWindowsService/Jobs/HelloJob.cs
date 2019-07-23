using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;



namespace QuartzForWindowsService.Jobs
{
    public class HelloJob : IJob
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => Logger.Debug("Greetings from HelloJob!"));
            
            //await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
