﻿using JK.Framework.Core;
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
        public ICacheManager _cache { get; set; }
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
                    throw new AuthorizeException("缺少参数sessionkey");
                }

                var flag=SessionKeyIsExist(sessionkey);
                if(!flag) throw new AuthorizeException("无效的sessionkey");
                HttpContext.Current.User = GetUser(sessionkey);

                base.OnAuthorization(filterContext);
            }
            else
            {
                throw new AuthorizeException("缺少参数sessionkey");
            }
       
        }

        private bool SessionKeyIsExist(string sessionKey)
        {
            return _cache.IsSet(sessionKey);
        }
        private UserModel GetUser(string sessionKey)
        {
            return _cache.Get<UserModel>(sessionKey);
        }
    }
}