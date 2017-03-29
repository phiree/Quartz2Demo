using Castle.Windsor;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WindsorJobFactory 
/// </summary>
public class WindsorJobFactory:IJobFactory
{
    IWindsorContainer container;
    public WindsorJobFactory(IWindsorContainer container)
    {
        this.container = container;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        IJobDetail jobDetail = bundle.JobDetail;
        Type jobType = jobDetail.JobType;
        // Return job that is registrated in container
        return (IJob)container.Resolve(jobType);
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        if (disposable != null)
        {
            disposable.Dispose();
        }
    }
}