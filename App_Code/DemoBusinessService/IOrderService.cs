﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// IOrderService 的摘要说明
/// </summary>
public interface IOrderService
{
 
    void OrderCancel(string orderId);
    
}