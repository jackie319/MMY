using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index()
        {
            var supplier=Supplier.Query();
            return View(supplier);
        }
    }
}