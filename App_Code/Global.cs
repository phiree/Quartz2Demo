using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

/// <summary>
/// Global 的摘要说明
/// </summary>
public class Global:HttpApplication
{
   
    void Application_Start(object sender, EventArgs e)
    {
        Bootstrap.Boot();
        log4net.Config.XmlConfigurator.Configure();
        IScheduler s = Bootstrap.Container.Resolve<IScheduler>();
        s.Start();
    }

    // keep site live. 
    public static void _SetupRefreshJob()
    {
        string refreshUrl = "http://localhost:19213//?refreshid="+Guid.NewGuid();
        Action remove = null;
        if (HttpContext.Current != null)
        {
            remove = HttpContext.Current.Cache["Refresh"] as Action;
        }
        if (remove is Action)
        {
            HttpContext.Current.Cache.Remove("Refresh");
            remove.EndInvoke(null);
        }

        //get the worker
        Action work = () =>
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000 * 60 * 1);
                System.Net.WebClient refresh = new System.Net.WebClient();
                try
                {
                    
                    refresh.UploadString(refreshUrl, string.Empty);
                }
                catch (Exception ex)
                {

                    
                }
                finally
                {
                    refresh.Dispose();
                }
            }
        };

       
        work.BeginInvoke(null, null);

        //add this job to the cache
        if (HttpContext.Current != null)
        {
            HttpContext.Current.Cache.Add(
            "Refresh",
            work,
            null,
            Cache.NoAbsoluteExpiration,
            Cache.NoSlidingExpiration,
            CacheItemPriority.Normal,
            (s, o, r) => { _SetupRefreshJob(); }
            );
        }
    }
}