using System;

namespace NServiceBus.Scheduling
{
    public interface IScheduledTaskStorage
    {
        void Add(ScheduledTask scheduledTask);
        ScheduledTask Get(Guid taskId);
    }
}