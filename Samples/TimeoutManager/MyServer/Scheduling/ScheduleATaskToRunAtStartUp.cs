using System;
using NServiceBus;

namespace MyServer.Scheduling
{
    public class ScheduleATaskToRunAtStartUp : IWantToRunAtStartup
    {
        public void Run()
        {
            Schedule.Every(TimeSpan.FromMinutes(5)).Action(() => Console.WriteLine("This task was schduled when the host started"));
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}