using System;
using System.Collections.Generic;

namespace NServiceBus.Scheduling
{
    public class InMemoryScheduledTaskStorage : IScheduledTaskStorage
    {
        protected readonly IDictionary<Guid, ScheduledTask> _scheduledTasks = new Dictionary<Guid, ScheduledTask>();

        public void Add(ScheduledTask scheduledTask)
        {
            _scheduledTasks.Add(scheduledTask.Id, scheduledTask);
        }

        public ScheduledTask Get(Guid taskId)
        {
            if (_scheduledTasks.ContainsKey(taskId))
                return _scheduledTasks[taskId];

            return null;
        }
    }
}