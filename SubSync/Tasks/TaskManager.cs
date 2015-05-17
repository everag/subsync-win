using Quartz;
using Quartz.Impl;
using SubSync.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Tasks
{
    public sealed class TaskManager
    {
        private static ISchedulerFactory SchedulerFactory;
        private static IScheduler Scheduler;

        private static IDictionary<Type, ISet<IJobDetail>> ScheduledJobs;

        static TaskManager()
        {
            SchedulerFactory = new StdSchedulerFactory();
            
            Scheduler = SchedulerFactory.GetScheduler();
            Scheduler.StartDelayed(new TimeSpan(0, 0, 5));
            Scheduler.Start();
            
            ScheduledJobs = new Dictionary<Type, ISet<IJobDetail>>();
        }

        public static void StartTask<Task>() where Task : IJob
        {
            Type jobType = typeof(Task);

            if (ScheduledJobs.ContainsKey(jobType) && ScheduledJobs[jobType].Any())
                throw new InvalidOperationException(string.Format("Task {0} has already been started", jobType.Name));

            if (jobType == typeof(CheckForVideosJob))
            {
                foreach (var dir in SyncManager.Instance.MediaDirectories)
                {
                    var trigger = TriggerBuilder.Create()
                        .WithIdentity(string.Format("Trigger.CheckDir[{0}]", dir.FullName), "SyncTasks")
                        .StartNow()
                        .WithSimpleSchedule(t => t
                            .WithIntervalInMinutes(Configuration.ScheduledFoldersCheckingDelay)
                            .RepeatForever())
                        .Build();

                    var job = JobBuilder.Create<CheckForVideosJob>()
                        .WithIdentity(string.Format("Job.CheckDir[{0}]", dir.FullName), "SyncTasks")
                        .UsingJobData(CheckForVideosJob.KEY_DIR_PATH, dir.FullName)
                        .Build();
                    
                    if (!ScheduledJobs.ContainsKey(jobType))
                        ScheduledJobs[jobType] = new HashSet<IJobDetail>();

                    ScheduledJobs[jobType].Add(job);

                    Scheduler.ScheduleJob(job, trigger);
                }
            }
            else if (jobType == typeof(CheckForUpdatesJob))
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("Trigger.CheckForUpdates", "SystemTasks")
                    .StartNow()
                    .WithSimpleSchedule(t => t
                        .WithIntervalInHours(Configuration.ScheduledUpdatesCheckingDelay)
                        .RepeatForever())
                    .Build();

                var job = JobBuilder.Create<CheckForUpdatesJob>()
                    .WithIdentity("Job.CheckForUpdates", "SystemTasks")
                    .Build();

                if (!ScheduledJobs.ContainsKey(jobType))
                    ScheduledJobs[jobType] = new HashSet<IJobDetail>();

                ScheduledJobs[jobType].Add(job);

                Scheduler.ScheduleJob(job, trigger);
            }
            else if (jobType == typeof(CheckInternetAvailabilityJob))
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("Trigger.CheckInternetAvailability", "SystemTasks")
                    .StartNow()
                    .WithSimpleSchedule(t => t
                        .WithIntervalInSeconds(Configuration.ScheduledInternetAvailabilityCheckingDelay)
                        .RepeatForever())
                    .Build();

                var job = JobBuilder.Create<CheckInternetAvailabilityJob>()
                    .WithIdentity("Job.CheckInternetAvailability", "SystemTasks")
                    .Build();

                if (!ScheduledJobs.ContainsKey(jobType))
                    ScheduledJobs[jobType] = new HashSet<IJobDetail>();

                ScheduledJobs[jobType].Add(job);

                Scheduler.ScheduleJob(job, trigger);
            }
        }

        public static void AntecipateTask<Task>(bool adjustSchedule) where Task : IJob
        {
            Type jobType = typeof(Task);

            if (adjustSchedule)
            {
                RestartTask<Task>();
            }
            else
            {
                foreach (var job in ScheduledJobs[jobType])
                {
                    Scheduler.TriggerJob(job.Key);
                }
            }
        }

        public static void RestartTask<Task>() where Task : IJob
        {
            Type jobType = typeof(Task);

            foreach (var job in ScheduledJobs[jobType])
            {
                var triggers = Scheduler.GetTriggersOfJob(job.Key);

                foreach (var trigger in triggers)
                {
                    var newTrigger = trigger.GetTriggerBuilder().Build();
                    Scheduler.RescheduleJob(trigger.Key, newTrigger);
                }
            }
        }

        public static void StopTask<Task>() where Task : IJob
        {
            Type jobType = typeof(Task);

            foreach (var job in ScheduledJobs[jobType].ToArray())
            {
                var triggers = Scheduler.GetTriggersOfJob(job.Key);

                foreach (var trigger in triggers)
                {
                    Scheduler.UnscheduleJob(trigger.Key);
                    ScheduledJobs[jobType].Remove(job);
                }
            }
        }
    }
}
