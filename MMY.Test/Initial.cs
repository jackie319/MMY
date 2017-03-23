using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using JK.Framework.Data;
using MMY.Services.IServices;
using MMY.Services.ServicesImpl;

namespace MMY.Test
{
    public class Initial
    {
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

            builder.RegisterType<ProductSupplierImpl>().As<IProductSupplier>().InstancePerRequest(); //mvc
            builder.RegisterType<AuthorityImpl>().As<IAuthority>().InstancePerRequest();
            builder.RegisterType<UserAccountImpl>().As<IUserAccount>().InstancePerDependency();
            builder.RegisterType<ProductImpl>().As<IProduct>().InstancePerDependency();
            builder.RegisterType<OrderImpl>().As<IOrder>().InstancePerDependency();
            #endregion
            // then
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
