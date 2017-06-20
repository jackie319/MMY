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


    }
}
