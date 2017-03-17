using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}