using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// JobRemindSupplierShip 的摘要说明
/// </summary>
public class JobRemindSupplierShip:IJob
{
    IOrderService orderService;
    public JobRemindSupplierShip(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    public void Execute(IJobExecutionContext context)
    {
       string orderId=(string) context.JobDetail.JobDataMap["orderId"];
        orderService.OrderCancel(orderId);
    }
}