using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JKResultModel=JK.Framework.Web.Model.ResultModel;
using log4net;

namespace MMY.PlatForm.WebUI
{
    public partial class FilterConfig
    {
        private static JKResultModel GlobalExceptionHandler(ExceptionContext exceptionfiltercontext)
        {
            var url = exceptionfiltercontext.RequestContext.HttpContext.Request.RawUrl;
            var exception = exceptionfiltercontext.Exception;
            //if(exception is AuthorizeException)
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            var logger = LogManager.GetLogger(typeof(FilterConfig));
            logger.Error("出错了！错误信息："+exception.Message+"访问路径："+url+"堆栈："+exception.StackTrace);
            var result = new JKResultModel(false,exception.Message,0,url,null) {};
            return result;
        }
    }
}