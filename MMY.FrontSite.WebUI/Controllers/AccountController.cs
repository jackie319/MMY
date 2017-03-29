using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MMY.FrontSite.Domain;
using MMY.FrontSite.WebUI.Models;
using MMY.FrontSite.WebUI.Models.UserAccount;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserAccount _userAccount;
        public AccountController(IUserAccount userAccount)
        {
            _userAccount = userAccount;
        }

        [HttpPost]
        public ActionResult SubmitLogin(string userName, string passwordMd5)
        {
            try
            {
                var account = _userAccount.Login(userName, passwordMd5);
                UserModel userModel = new UserModel(account.Guid, account.UserName, account.NickName, true) { };
                HttpContext.User = userModel;
                Session["UserInfoModel"] = userModel;
            }
            catch (CommonException)
            {
                return this.ResultError("用户名或密码错误");
            }
            return this.ResultSuccess();
        }

        [HttpPost]
        [ValidationFilter]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                _userAccount.Register(model.MobilePhone, model.PasswordMd5, model.SmsCode);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }
            return this.ResultSuccess();
        }
    }
}