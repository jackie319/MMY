using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using log4net;
using JKResultModel = JK.Framework.Web.Model.ResultModel;

namespace MMY.FrontSite.WebUI
{
    public partial class FilterConfig
    {
        private static JKResultModel GlobalExceptionHandler(ExceptionContext exceptionfiltercontext)
        {
            string errorMsg = string.Empty;
            JKExceptionType type = JKExceptionType.Common;
            string redirectUrl = string.Empty;
            var url = exceptionfiltercontext.RequestContext.HttpContext.Request.RawUrl;
            var exception = exceptionfiltercontext.Exception;
            if (exception is AuthorizeException)
            {
                type = JKExceptionType.NoAuthorized;
                redirectUrl = "/Account/Login";
                errorMsg = "用户认证未通过";
            }
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                errorMsg = exception.Message;
            }

            var logger = LogManager.GetLogger(typeof(FilterConfig));
            logger.Error("出错了！错误信息：" + errorMsg + "访问路径：" + url + "堆栈：" + exception.StackTrace);
            var result = new JKResultModel(false, errorMsg, 0, url, type, redirectUrl, null) { };
            return result;
        }
    }
}