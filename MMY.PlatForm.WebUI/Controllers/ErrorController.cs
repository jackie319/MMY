﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}