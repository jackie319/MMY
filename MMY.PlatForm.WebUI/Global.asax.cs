using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using JK.Framework.Core.Caching;
using JK.Framework.Core.Data;
using JK.Framework.Data;
using log4net.Config;
using MMY.Data.Model;
using MMY.PlatForm.Domain;
using MMY.Services.IServices;
using MMY.Services.ServicesImpl;
using ICacheManager = WebGrease.ICacheManager;

namespace MMY.PlatForm.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            PostAcquireRequestState += MvcApplicationPostAcquireRequestState;
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutofac();
            InitLog4Net();
        }

        public static void RegisterAutofac()
        {
            string connectionStr = System.Web.Configuration.WebConfigurationManager.
                ConnectionStrings["MMYEntities"].ConnectionString;

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #region IOC注册区域
            //倘若需要默认注册所有的，请这样写（主要参数需要修改）
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //   .AsImplementedInterfaces();

            RegisterAutofacForJK.RegisterAutofacForJKFramework(builder, connectionStr);

            // builder.RegisterType<DbContextGetter>().As<IDbContextGetter>().SingleInstance();

            builder.RegisterType<ProductSupplierImpl>().As<IProductSupplier>().InstancePerHttpRequest(); //mvc
            builder.RegisterType<AuthorityImpl>().As<IAuthority>().InstancePerRequest();
            builder.RegisterType<UserAccountImpl>().As<IUserAccount>().InstancePerDependency();
            #endregion
            // then
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        protected void Application_error(object sender, EventArgs e)
        {
            HttpException error = (HttpException)Server.GetLastError();
            if (error.GetHttpCode() == 404)
            {
                Response.Redirect("/Error/NotFound");

                //请求不存在的页面之前会话已过期
                //（触发新的错误，左侧菜单不能正常显示,解决：跳转到登录页面，让用户登录恢复会话）
                // Response.Redirect("/Account/Login");
            }
        }

        protected void MvcApplicationPostAcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;
            var user = Session["UserInfoModel"];
            if (user is UserModel)
            {
                HttpContext.Current.User = user as UserModel;
            }
        }
    }
}
