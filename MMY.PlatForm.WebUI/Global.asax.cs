﻿using System;
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
using MMY.Services.IServices;
using MMY.Services.ServicesImpl;
using ICacheManager = WebGrease.ICacheManager;

namespace MMY.PlatForm.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
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
           
            RegisterAutofacForJK.RegisterAutofacForJKFramework(builder,connectionStr);

           // builder.RegisterType<DbContextGetter>().As<IDbContextGetter>().SingleInstance();

            builder.RegisterType<ProductSupplierImpl>().As<IProductSupplier>().InstancePerHttpRequest(); //mvc

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
    }
}
