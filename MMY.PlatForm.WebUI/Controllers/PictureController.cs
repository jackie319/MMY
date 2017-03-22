using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Extensions;
using JK.Framework.Web.Model;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class PictureController : Controller
    {
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var url = UploadManager.SavePicture(file, "Product");
                return this.ResultModel(url);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }
        
        }
    }
}