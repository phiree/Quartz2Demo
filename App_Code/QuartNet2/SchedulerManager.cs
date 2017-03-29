using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SchedulerManager  
/// </summary>
public class SchedulerManager
{
    IScheduler scheduler; //Bootstrap.Container.Resolve<IScheduler>();
    IOrderService orderService;


    public SchedulerManager(IOrderService orderService, IScheduler scheduler, IJobFactory jobFactory)
    {
        this.orderService = orderService;
        this.scheduler = scheduler;
        scheduler.JobFactory = jobFactory;

    }
    public void CreateJob(string orderid, string type)
    {
        IJobDetail job = JobBuilder.Create(Type.GetType(type))
                     .SetJobData(new JobDataMap { { "orderId", orderid } })
                     .Build();
        scheduler.ScheduleJob(job, CreateTrigger());
        
    }
    public void DeleteJob(string jobKey)
    {
        scheduler.DeleteJob(new JobKey(jobKey));
    }
    public IList<JobDto> GetAllJobs()
    {
        var jobGroups = scheduler.GetJobGroupNames();
        IList<JobDto> allJobs = new List<JobDto>();
        foreach (string group in jobGroups)
        {
            var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
            var jobKeys = scheduler.GetJobKeys(groupMatcher);
            foreach (var jobKey in jobKeys)
            {
                JobDto dto = new JobDto();
                var detail = scheduler.GetJobDetail(jobKey);
                var triggers = scheduler.GetTriggersOfJob(jobKey);
                dto.OrderId= detail.JobDataMap["orderId"].ToString();
                dto.JobKeyName = detail.Key.Name;
                //in this demo,only one trigger for each job
                foreach (ITrigger trigger in triggers)
                {
                    dto.StartTime = trigger.StartTimeUtc.LocalDateTime;
                    dto.TriggerState = scheduler.GetTriggerState(trigger.Key).ToString();
                    DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                    if (nextFireTime.HasValue)
                    {
                        dto.NextFireTime = TimeZone.CurrentTimeZone.ToLocalTime(nextFireTime.Value.DateTime);
                    }

                    DateTimeOffset? previousFireTime = trigger.GetPreviousFireTimeUtc();
                    if (previousFireTime.HasValue)
                    {
                        dto.PreviousFireTime = TimeZone.CurrentTimeZone.ToLocalTime(previousFireTime.Value.DateTime);
                    }

                    allJobs.Add(dto);
                }
            }
        }
        return allJobs;
    }
    private ITrigger CreateTrigger()
    {
        var  trigger = TriggerBuilder.Create()
            .WithSimpleSchedule(
            x=>x.WithIntervalInSeconds(5)
            .WithRepeatCount(2)) 
        .Build();
        return trigger;

    }
}