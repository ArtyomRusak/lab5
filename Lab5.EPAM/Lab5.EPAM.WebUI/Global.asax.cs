using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Lab5.EPAM.Core;
using Lab5.EPAM.EFData;
using Lab5.EPAM.EFData.EFContext;
using Lab5.EPAM.Services.Services;
using Lab5.EPAM.WebUI.App_Start;

namespace Lab5.EPAM.WebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteTable.Routes.MapRoute("Default", "{controller}/{action}", new { controller = "Account", action = "Register" });
            //AttributeRoutingConfig.Start();

            //var builder = new ContainerBuilder();

            //SiteContext context = new SiteContext(Resources.ConnectionString);
            //builder.Register(x => context).As<DbContext>();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            //builder.RegisterType<UnitOfWork>().As<IRepositoryFactory>();
            //builder.RegisterAssemblyTypes()
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        var i = HttpContext.Current.User.Identity;
                        var context = new SiteContext(Resources.ConnectionString);
                        var unitOfWork = new UnitOfWork(context);
                        var membershipService = new MembershipService(unitOfWork, unitOfWork);
                        var user = membershipService.GetUserByEmail(i.Name);
                        var roles = user.Roles.Select(w => w.Name).ToArray();
                        HttpContext.Current.User = new GenericPrincipal(i, roles);
                        unitOfWork.Dispose();
                    }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}