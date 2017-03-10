using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult List(SupplierQueryModel query)
        {
            int total;
            var supplier = _Supplier.Query(query.SupplierName, query.SupplierAddress, query.Skip, query.Take, out total);
            IList<SupplierListViewModel> models = supplier.Select(item=> SupplierListViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(total, models);
        }

        public ActionResult Detail(Guid supplierGuid)
        {
            var supplier= _Supplier.Find(supplierGuid);
            var result = SupplierViewModel.CopyFrom(supplier);
            return this.ResultModel(result);
        }

        public ActionResult Add(SupplierViewModel model)
        {
            _Supplier.Add(model.CopyTo());
            return this.ResultSuccess();
        }

        public ActionResult Update(SupplierViewModel model)
        {
            _Supplier.Update(model.CopyTo());
            return this.ResultSuccess();
        }

        public ActionResult Delete(Guid guid)
        {
            _Supplier.Delete(guid);
            return this.ResultSuccess();
        }
    }
}