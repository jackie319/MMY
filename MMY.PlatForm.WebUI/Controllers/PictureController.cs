using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Extensions;
using JK.Framework.Web.Model;
using MMY.PlatForm.WebUI.Models.Product;
using MMY.Services.ServicesImpl;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class PictureController : Controller
    {
        /// <summary>
        /// 封面上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
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

        public ActionResult CkEditorUpload()
        {
            var file = HttpContext.Request.Files[0];
            var name = UploadManager.SavePicture(file, "Product");
            var picurl = AppSetting.Instance().PictureUrl;
            var url = picurl+ name;
            string num = HttpContext.Request.QueryString["CKEditorFuncNum"];
            string content = string.Format("<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction({0},\"{1}\",\"\");</script>", num, url);
            return Content(content, "text/html");

        }

        /// <summary>
        /// 相册上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadList(IList<HttpPostedFileBase> files)
        {
            IList< UploadListViewModel > list=new List<UploadListViewModel>();
            try
            {
                foreach (var item in files)
                {
                    UploadListViewModel model=new UploadListViewModel();
                    var url = UploadManager.SavePicture(item, "Product");
                    model.AlbumGuid = Guid.NewGuid();
                    model.Url = url;
                    list.Add(model);
                }
                int total = list.Count;
                return this.ResultListModel(total,list);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }

        }
    }
}