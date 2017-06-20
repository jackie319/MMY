using JK.Framework.Core;
using JK.Framework.Core.Caching;
using MMY.FrontSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MMY.FrontSite.WebApi.App_Start
{
    public class ApiSessionAuthorizeAttribute : AuthorizeAttribute
    {
        public ICacheManager _cache;
        public ApiSessionAuthorizeAttribute(ICacheManager cache)
        {
            _cache = cache;
        }
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            string sessionkey = string.Empty;
            if (filterContext.Request.Headers.Contains("sessionkey"))
            {
                try
                {
                    sessionkey = HttpUtility.UrlDecode(filterContext.Request.Headers.GetValues("sessionkey").FirstOrDefault());
                }
                catch (ArgumentException)
                {
                }
                if (string.IsNullOrEmpty(sessionkey))
                {
                    throw new CommonException("缺少参数sessionkey");
                }

                var flag=SessionKeyIsExist(sessionkey);
                if(!flag) throw new CommonException("无效的sessionkey");
                base.OnAuthorization(filterContext);
            }
        }

        public bool SessionKeyIsExist(string sessionKey)
        {
            return _cache.IsSet(sessionKey);
        }
    }
}