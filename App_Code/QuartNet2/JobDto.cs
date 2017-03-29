using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobDto
{
    
    public string JobKeyName { get; set; }
    public string TriggerState { get; set; }
    public DateTime PreviousFireTime { get; set; }
    public DateTime NextFireTime { get; internal set; }
    public DateTime StartTime { get; set; }
    public int RepeatedTimes { get; set; }
    public string OrderId { get; set; }
}

