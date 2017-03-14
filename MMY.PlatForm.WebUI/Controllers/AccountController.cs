using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Extensions;
using JK.Framework.Web.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MMY.PlatForm.Domain;
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
        public ActionResult SubmitLogin(string userName,string nickName)
        {
            string md5 = nickName.ToMd5();
            try
            {
                var account=_userAccount.Login(userName, md5);
                var menu = _authority.GetUserMenu(new Guid(), Guid.Empty);
                UserModel userModel = new UserModel(account.Guid, account.UserName, account.NickName, menu, true) {};
                HttpContext.User = userModel;
                Session["UserInfoModel"]=userModel;
            }
            catch (CommonException)
            {
                return this.ResultError("用户名或密码错误");
            }
            return this.ResultSuccess();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return this.ResultSuccess();
        }
    }
}