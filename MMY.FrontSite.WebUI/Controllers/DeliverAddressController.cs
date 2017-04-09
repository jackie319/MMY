using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class DeliverAddressController : Controller
    {
        /// <summary>
        /// 我的收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 新增或编辑收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return null;
        }

        public ActionResult Detail()
        {
            return null;
        }

        public ActionResult Delete()
        {
            return null;
        }

       
    }
}