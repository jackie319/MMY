using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Extensions;
using JK.Framework.Web.Model;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class PictureController : Controller
    {
        /// <summary>
        /// 头像上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var url = UploadManager.SavePicture(file, "User");
                return this.ResultModel(url);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }

        }
    }
}