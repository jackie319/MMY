using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using MMY.FrontSite.WebUI.Models.ConfirmOrder;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class ConfirmOrderController : Controller
    {
        [JKAuthorize]
        public ActionResult FromCart(IList<Guid> shoppingCartGuids )
        {
            return null;
        }

        [JKAuthorize]
        public ActionResult FromGoods(AddConfirmOrderViewModel model)
        {
            return null;
        }
    }
}