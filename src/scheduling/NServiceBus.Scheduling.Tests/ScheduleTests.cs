using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace NServiceBus.Scheduling.Tests
{
    [TestFixture]
    public class ScheduleTests
    {
        private const string ACTION_NAME = "my action";
        private FuncBuilder _builder = new FuncBuilder();
        private FakeBus _bus = new FakeBus();

        private InMemoryScheduledTaskStorageForTest _taskStorage = new InMemoryScheduledTaskStorageForTest();        

        [SetUp]
        public void SetUp()
        {
            Configure.With(new Assembly[0]);
            Configure.Instance.Builder = _builder;

            _builder.Register<IBus>(() => _bus);
            _builder.Register<IScheduledTaskStorage>(() => _taskStorage);
            _builder.Register<IScheduler>(() => new DefaultScheduler(_bus, _taskStorage));
        }

        [Test]
        public void When_scheduling_an_action_with_a_name_the_task_should_get_that_name()
        {
            Schedule.Every(TimeSpan.FromMinutes(5)).Action(ACTION_NAME, () => { });
            Assert.That(EnsureThatNameExists(ACTION_NAME));
        }

        [Test]
        public void When_scheduling_an_action_without_a_name_the_task_should_get_the_DeclaringType_as_name()
        {
            Schedule.Every(TimeSpan.FromMinutes(5)).Action(() => {  });
            Assert.That(EnsureThatNameExists("ScheduleTests"));
        }

        private bool EnsureThatNameExists(string name)
        {
            bool actionNameFound = false;
            foreach (var task in _taskStorage.Tasks)
            {
                if (task.Value.Name.Equals(name))
                {
                    actionNameFound = true;
                }
            }
            return actionNameFound;
        }
    }

    public class InMemoryScheduledTaskStorageForTest : InMemoryScheduledTaskStorage
    {
        public IDictionary<Guid, ScheduledTask> Tasks
        {
            get { return _scheduledTasks; }
        }
    }
}