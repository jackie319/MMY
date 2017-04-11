using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using MMY.Services.IServices;
using MMY.Services.ServicesImpl;

namespace MMY.FrontSite.WebUI.App_Start
{
    public class AutoFacRegister
    {
        public static void RegisterAutofacDelegate(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ProductSupplierImpl>().As<IProductSupplier>().InstancePerDependency();
            builder.RegisterType<AuthorityImpl>().As<IAuthority>().InstancePerRequest();
            builder.RegisterType<UserAccountImpl>().As<IUserAccount>().InstancePerDependency();
            builder.RegisterType<ProductImpl>().As<IProduct>().InstancePerDependency();
            builder.RegisterType<OrderImpl>().As<IOrder>().InstancePerDependency();
            builder.RegisterType<UserFavoriteImpl>().As<IUserFavorite>().InstancePerDependency();
            builder.RegisterType<ShoppingCartImpl>().As<IShoppingCart>().InstancePerDependency();
        }
    }
}