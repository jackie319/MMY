using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MMY.FrontSite.WebApi.App_Start
{
    public class ApiSessionAuthorizeAttribute: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}