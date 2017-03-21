using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Model;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class PictureController : Controller
    {
        public ActionResult Upload()
        {
            return this.ResultModel(new {uid=Guid.NewGuid()});
        }
    }
}