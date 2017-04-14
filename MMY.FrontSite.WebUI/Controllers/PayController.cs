using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class PayController : Controller
    {
        private IOrder _order;
        private IPay _pay;
        public PayController(IOrder order,IPay pay)
        {
            _order = order;
            _pay = pay;
        }
        [JKAuthorize]
        public ActionResult WechatPay(IList<Guid> orderGuids)
        {
            foreach (var orderGuid in orderGuids)
            {
                _pay.WechatPay(orderGuid);
            }
        
            return this.ResultSuccess();
        }
    }
}