using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using MMY.Services.IServices;
using MMY.Services.ServicesImpl;

namespace MMY.FrontSite.WebApi
{
    public class AutoFacRegister
    {
        public static void RegisterAutofacDelegate(ContainerBuilder builder)
        {
           builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ProductSupplierImpl>().As<IProductSupplier>().InstancePerDependency();
            builder.RegisterType<AuthorityImpl>().As<IAuthority>().InstancePerRequest();
            builder.RegisterType<UserAccountImpl>().As<IUserAccount>().InstancePerDependency();
            builder.RegisterType<ProductImpl>().As<IProduct>().InstancePerDependency();
            builder.RegisterType<OrderImpl>().As<IOrder>().InstancePerDependency();
            builder.RegisterType<UserFavoriteImpl>().As<IUserFavorite>().InstancePerDependency();
            builder.RegisterType<ShoppingCartImpl>().As<IShoppingCart>().InstancePerDependency();
            builder.RegisterType<PayImpl>().As<IPay>().InstancePerDependency();
            builder.RegisterType<SmsImpl>().As<ISms>().InstancePerDependency();
        }
    }
}