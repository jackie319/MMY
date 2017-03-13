using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;

namespace MMY.PlatForm.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
