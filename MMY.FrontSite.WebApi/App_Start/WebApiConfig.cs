﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using JK.Framework.API.Filter;
using System.Web.Configuration;

namespace MMY.FrontSite.WebApi
{
    public static partial class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            // config.SuppressDefaultHostAuthentication();
            // config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new ApiGlobalExceptioinFilter(GlobalExceptionHandler));

            string privateToken = WebConfigurationManager.AppSettings["PrivateToken"];
            config.Filters.Add(new JKApiTokenAuthorizeAttribute(privateToken));
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
