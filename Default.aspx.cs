using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger("quartnet2");
    SchedulerManager sm = Bootstrap.Container.Resolve<SchedulerManager>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindJobs();
        }
    }

    private void BindJobs()
    {
        rptRuningJobs.DataSource = sm.GetAllJobs().OrderByDescending(x=>x.StartTime);
        rptRuningJobs.DataBind();
    }

    
    protected void rptRuningJobs_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "deletejob":
                string jobKey =(string) e.CommandArgument;
                sm.DeleteJob(jobKey);
                Response.Redirect("/");
                break;
        }
    }

    protected void btnPayNewOrder_Click(object sender, EventArgs e)
    {
        Guid orderId = Guid.NewGuid();
        sm.CreateJob(orderId.ToString(), "JobRemindSupplierShip");// sanme as job class name
        log.Info("an order need to shipping:" + orderId);
        Response.Redirect("/",true);
    }
}