using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using JK.Framework.Data;
using log4net.Config;
using MMY.Services.ServicesImpl;

namespace MMY.FrontSite.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutofac();
            InitLog4Net();
            GetAppSetting();
        }

        public static void RegisterAutofac()
        {
            string connectionStr = System.Web.Configuration.WebConfigurationManager.
                ConnectionStrings["MMYEntities"].ConnectionString;

            RegisterAutofacForJK.Register(connectionStr, AutoFacRegister.RegisterAutofacDelegate);
        }

        public void GetAppSetting()
        {
            string pictureUrl = WebConfigurationManager.AppSettings["PictureUrl"];
            AppSetting.Instance().PictureUrl = pictureUrl;
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
    }
}
