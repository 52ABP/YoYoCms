using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Timing;
using Abp.Web;
using Castle.Facilities.Logging;

namespace YoYo.Cms.Web
{
    public class MvcApplication : AbpWebApplication<CmsWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {


            Clock.Provider = ClockProviders.Local;

            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            base.Application_Start(sender, e);
        }
    }
}
