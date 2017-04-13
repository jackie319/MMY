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
        private IShoppingCart _shoppingCart;
        public ConfirmOrderController(IProduct product,IShoppingCart shoppingCart)
        {
            _product = product;
            _shoppingCart = shoppingCart;
        }
        [JKAuthorize]
        public ActionResult FromCart(IList<Guid> shoppingCartGuids )
        {
            if (shoppingCartGuids.Count > 10) return this.ResultError("购物车最多可以结算10个商品");
            IList<ConfirmOrderViewModel> list=new List<ConfirmOrderViewModel>();

            var carts=_shoppingCart.FindList(shoppingCartGuids);
            foreach (var item in carts)
            {
                ConfirmOrderViewModel model= ConfirmOrderViewModel.CopyFrom(item.Product);
                var classification = item.Product.ProductClassification.FirstOrDefault(q => q.Guid == item.ClassificationGuid);
                if (classification != null)
                {
                    model.ClassificationName = classification.Name;
                    model.Grams = classification.Grams;
                }
                list.Add(model);
            }
            return this.ResultListModel(list.Count,list);
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
                confirm.Grams = classification.Grams;
            }
            return this.ResultModel(confirm);
        }
    }
}