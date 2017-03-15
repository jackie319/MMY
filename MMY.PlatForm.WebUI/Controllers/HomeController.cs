using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.PlatForm.Domain;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var model = (UserModel)HttpContext.User;
            return this.ResultModel(model.UserMenuModels);
        }

    }
}