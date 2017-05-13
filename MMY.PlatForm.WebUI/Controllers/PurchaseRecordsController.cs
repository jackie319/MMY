using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.PlatForm.Domain;
using MMY.PlatForm.WebUI.Models.Product;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class PurchaseRecordsController : Controller
    {
        private IProduct _product;
        // GET: PurchaseRecords
        public  PurchaseRecordsController(IProduct product)
        {
            _product = product;
        }


        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult List(PurchaseRecordsQueryModel model)
        {
            int total;
            var result=_product.GetProductPurchaseRecords(model.ProductGuid, model.SupplierGuid, model.Skip, model.Take, out total);
            var resultList = result.Select(item => ProductPurchaseRecordsListViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 进货记录不需要填写克数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidationFilter]
        [HttpPost]
        public ActionResult Save(ProductPurchaseRecordsViewModel model)
        {
            var record = model.CopyTo();
            var user = (UserModel) HttpContext.User;
            record.OperatorGuid = user.UserGuid;
            record.OperatorName = user.NickName;
            _product.AddPurchaseRecords(record);
            return this.ResultSuccess();
        }
    }
}