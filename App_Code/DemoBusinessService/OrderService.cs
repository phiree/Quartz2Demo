using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
/// <summary>
/// IOrderService 的摘要说明
/// </summary>
public class OrderService:IOrderService
{
    ILog log = LogManager.GetLogger("QuartNet2Demo.DemoOrderService");

    public void OrderCancel(string orderId)
    {
        log.Info("order has been canceld:" + orderId);
    }

 
}