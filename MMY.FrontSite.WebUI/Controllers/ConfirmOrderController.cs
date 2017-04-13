using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.FrontSite.WebUI.Models.ConfirmOrder;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class ConfirmOrderController : Controller
    {
        private IProduct _product; 
        public ConfirmOrderController(IProduct product)
        {
            _product = product;
        }
        [JKAuthorize]
        public ActionResult FromCart(IList<Guid> shoppingCartGuids )
        {
            return null;
        }

        [JKAuthorize]
        [ValidationFilter]
        public ActionResult FromBuyNow(AddConfirmOrderViewModel model)
        {
            var product=_product.FindProduct(model.ProductGuid);
            var confirm = ConfirmOrderViewModel.CopyFrom(product);
            confirm.ProductNum = model.ProductNum;
            confirm.OrderAmount = model.ProductNum * confirm.PromotionPrice;
            var classification = product.ProductClassification.FirstOrDefault(q => q.Guid == model.ClassificationGuid);
            if (classification != null)
            {
                confirm.ClassificationName = classification.Name;
            }
            return this.ResultModel(confirm);
        }
    }
}