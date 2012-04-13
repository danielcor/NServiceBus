using System;
using NServiceBus;

namespace MyServer.Scheduling
{
    public class ScheduleATaskHandler : IHandleMessages<ScheduleATask>
    {
        public void Handle(ScheduleATask message)
        {
            // this will schedule a task to write to the console every minute.
            Schedule.Every(TimeSpan.FromMinutes(1)).Action(() => Console.WriteLine("This is a scheduled task"));
        }
    }
}