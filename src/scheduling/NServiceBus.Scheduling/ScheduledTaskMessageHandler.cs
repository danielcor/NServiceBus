namespace NServiceBus.Scheduling
{
    public class ScheduledTaskMessageHandler : IHandleMessages<Messages.ScheduledTask>
    {
        private readonly IScheduler _scheduler;

        public ScheduledTaskMessageHandler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Handle(Messages.ScheduledTask message)
        {
            _scheduler.Start(message.TaskId);
        }
    }
}