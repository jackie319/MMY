using JK.Framework.API.Filter;
using System.Web;
using System.Web.Mvc;

namespace MMY.FrontSite.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }
    }
}
