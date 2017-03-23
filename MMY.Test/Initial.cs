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
        public static IContainer _container;
        public static void RegisterAutofac()
        {
            string connectionStr = System.Web.Configuration.WebConfigurationManager.
                ConnectionStrings["MMYEntities"].ConnectionString;

            ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
            RegisterAutofacForJK.RegisterAutofacForJKFramework(builder, connectionStr);

            builder.RegisterType<ProductSupplierImpl>().As<IProductSupplier>().InstancePerRequest(); 
            builder.RegisterType<AuthorityImpl>().As<IAuthority>().InstancePerRequest();
            builder.RegisterType<UserAccountImpl>().As<IUserAccount>().InstancePerDependency();
            builder.RegisterType<ProductImpl>().As<IProduct>().InstancePerDependency();
            builder.RegisterType<OrderImpl>().As<IOrder>().InstancePerDependency();

            // then
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));

        }
    }
}
