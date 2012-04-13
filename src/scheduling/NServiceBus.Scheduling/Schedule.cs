using System;
using NServiceBus.Scheduling;
using ScheduledTask = NServiceBus.Scheduling.ScheduledTask;

namespace NServiceBus
{
    public class Schedule
    {        
        private readonly IScheduler _scheduler;
        private readonly ScheduledTask _scheduledTask;

        private Schedule(TimeSpan timeSpan)
        {            
            _scheduler = Configure.Instance.Builder.Build<IScheduler>();
            _scheduledTask = new ScheduledTask { Every = timeSpan };
        }

        public static Schedule Every(TimeSpan timeSpan)
        {
            return new Schedule(timeSpan);
        }

        public void Action(Action task)
        {            
            Action(task.Method.DeclaringType.Name, task);
        }

        public void Action(string name, Action task)
        {
            _scheduledTask.Task = task;
            _scheduledTask.Name = name;
            _scheduler.Schedule(_scheduledTask);            
        }
    }
}
