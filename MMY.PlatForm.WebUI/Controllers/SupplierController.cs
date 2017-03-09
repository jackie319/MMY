using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMY.PlatForm.WebUI.Models;
using MMY.PlatForm.WebUI.Models.Product;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class SupplierController : Controller
    {
        private IProductSupplier Supplier;
        public SupplierController(IProductSupplier supplier)
        {
            Supplier = supplier;
        }
        // GET: Supplier
        public ActionResult List(SupplierQueryModel query)
        {
            int total;
            var supplier=Supplier.Query(query.SupplierName, query.SupplierAddress, query.Skip, query.Take,out total);
            ResultModel< ProductListViewModel > sModel=new ResultModel<ProductListViewModel>();
            return Json(new ProductListViewModel());
        }
    }
}