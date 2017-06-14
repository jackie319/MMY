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
using JK.Framework.API.Filter;
using log4net;

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

            RegisterAutofacForJK.RegisterApi(connectionStr, AutoFacRegister.RegisterAutofacDelegate);
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
            var logger = LogManager.GetLogger(typeof(WebApiConfig));
            logger.Error($"Application_error: {error.GetHttpCode()};{error.Message}");
        }
    }
}
