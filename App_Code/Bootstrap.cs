using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Bootstrap 的摘要说明
/// </summary>
public class Bootstrap
{
    static IWindsorContainer container;
    public static IWindsorContainer Container
    {
        get { return container; }
        private set { container = value; }
    }
    public static void Boot()
    {
        container = new WindsorContainer();
        container.Register(Component.For<IOrderService>().ImplementedBy<OrderService>());
        container.Register(Component.For<IJobFactory>() .Instance(new WindsorJobFactory(container)));
        container.Register(Component.For<JobRemindSupplierShip>());
        container.Register(Component.For<IScheduler>() .Instance(StdSchedulerFactory.GetDefaultScheduler()));
        container.Register(Component.For<SchedulerManager>()
            );
 
    }

    
}