using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Extensions;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MMY.PlatForm.Domain;
using MMY.PlatForm.WebUI.Models.Account;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserAccount _userAccount;
        private IAuthority _authority;

        public AccountController(IUserAccount userAccount, IAuthority authority)
        {
            _userAccount = userAccount;
            _authority = authority;
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SubmitLogin(string userName, string passwordMd5)
        {
            try
            {
                var account = _userAccount.Login(userName, passwordMd5);
                var menu = _authority.GetUserMenu(new Guid(), Guid.Empty);
                UserModel userModel = new UserModel(account.Guid, account.UserName, account.NickName, menu, true) { };
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
        public ActionResult UserInfo()
        {
            var mmyUser = (UserModel)HttpContext.User;
            return this.ResultModel(mmyUser);
        }
        [HttpPost]
        [ValidationFilter]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!model.NewPasswordMd5Confirm.Equals(model.NewPasswordMd5))
                return this.ResultError("俩次输入的密码不匹配");
            try
            {
                var mmyUser = (UserModel) HttpContext.User;
                _userAccount.ChangePwd(mmyUser.UserName, model.OldPasswordMd5, model.NewPasswordMd5);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }
            return this.ResultSuccess();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return this.ResultSuccess();
        }
    }
}