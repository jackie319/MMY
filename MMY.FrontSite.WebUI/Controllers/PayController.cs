using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Model;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class PayController : Controller
    {
        public ActionResult WechatPay(Guid orderGuid)
        {
            return this.ResultSuccess();
        }
    }
}