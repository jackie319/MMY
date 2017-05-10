using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.PlatForm.WebUI.Models;
using MMY.PlatForm.WebUI.Models.Product;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class SupplierController : Controller
    {
        private IProductSupplier _Supplier;
        public SupplierController(IProductSupplier supplier)
        {
            _Supplier = supplier;
        }
        // GET: Supplier
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult List(SupplierQueryModel query)
        {
            int total;
            var supplier = _Supplier.Query(query.SupplierName, query.SupplierAddress, query.Skip, query.Take, out total);
            IList<SupplierListViewModel> models = supplier.Select(item => SupplierListViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(total, models);
        }

        public ActionResult Detail(Guid supplierGuid)
        {

            var supplier = _Supplier.Find(supplierGuid);
            if (supplier == null)
            {
                return this.ResultModel(new SupplierViewModel());
            }
            var result = SupplierViewModel.CopyFrom(supplier);
            return this.ResultModel(result);
        }
        //[ValidationFilter]
        //[HttpPost]
        //public ActionResult Add(SupplierViewModel model)
        //{
        //    _Supplier.Add(model.CopyTo());
        //    return this.ResultSuccess();
        //}
        //[ValidationFilter]
        //[HttpPost]
        //public ActionResult Update(SupplierViewModel model)
        //{
        //    if (model.Uid == null ||model.Uid == Guid.Empty) return this.ResultError("供货商Guid不能为空");
        //    _Supplier.Update(model.CopyTo());
        //    return this.ResultSuccess();
        //}

        [ValidationFilter]
        [HttpPost]
        public ActionResult Save(SupplierViewModel model)
        {
            if (model.Guid == Guid.Empty)
            {
                _Supplier.Add(model.CopyTo());
            }
            else
            {
                _Supplier.Update(model.CopyTo());
            }
            return this.ResultSuccess();
        }

        [HttpPost]
        public ActionResult Delete(Guid guid)
        {
            _Supplier.Delete(guid);
            //JsonRequestBehavior.DenyGet 不允许get
            //但其实已经删除了，只是return 时报错，加httpPost不让进方法
            return this.ResultSuccess();
        }
    }
}