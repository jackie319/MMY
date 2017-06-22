using JK.Framework.Core.Caching;
using MMY.FrontSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MMY.FrontSite.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        private const string ResultTotal = "X-Total-Count";
        private const string SessionKey = "X-SessionKey";
        public static void AppendHeaderTotal(int total)
        {
            System.Web.HttpContext.Current.Response.AppendHeader(ResultTotal, Convert.ToString(total));
        }

        public static void AppendHeaderSessionKey(string sessionKey)
        {
            System.Web.HttpContext.Current.Response.AppendHeader(SessionKey, sessionKey);
        }

        //private ICacheManager _cache;
        //public BaseApiController(ICacheManager cache)
        //{
        //    _cache = cache;
        //}

        //[NonAction]
        //public UserModel GerUserModel()
        //{
        //    var sessionkey = string.Empty;
        //    if (Request.Headers.Contains("sessionkey"))
        //    {
        //        try
        //        {
        //            sessionkey = Request.Headers.GetValues("sessionkey").FirstOrDefault();
        //        }
        //        catch (ArgumentException)
        //        {
        //        }
        //        if (string.IsNullOrEmpty(sessionkey))
        //        {
        //            throw new MMYAuthorizeException("无效的sessionkey");
        //        }

        //        var flag = _cache.IsSet(sessionkey);
        //        if (!flag) throw new MMYAuthorizeException("无效的sessionkey");
        //        var userModel = _cache.Get<UserModel>(sessionkey);
        //        return userModel;
        //    }
        //    else
        //    {
        //        throw new MMYAuthorizeException("缺少参数sessionkey");
        //    }
        //}

    }
}
