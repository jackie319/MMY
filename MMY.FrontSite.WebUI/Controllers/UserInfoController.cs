using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.FrontSite.Domain;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class UserInfoController : Controller
    {
        [JKAuthorize]
        public ActionResult UserInfo()
        {
            var mmyUser = (UserModel)HttpContext.User;
            return this.ResultModel(mmyUser);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            return null;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return null;
        }
    }
}