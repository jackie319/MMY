using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace MMY.PlatForm.WebUI.App_Start
{
    public class GlobalExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var url = filterContext.RequestContext.HttpContext.Request.RawUrl;
            var exception = filterContext.Exception;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            var logger = LogManager.GetLogger(typeof(GlobalExceptionFilter));
            logger.Error("出错了！请求地址："+url+"错误信息："+exception.Message);

        }
    }
}