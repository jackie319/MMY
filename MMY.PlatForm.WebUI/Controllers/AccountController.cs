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
        public ActionResult Login()
        {
            string md5 = "12345678".ToMd5();
            try
            {
                var account=_userAccount.Login("Jackie", md5);
                var menu = _authority.GetUserMenu(new Guid(), Guid.Empty);
                UserModel userModel=new UserModel();
                userModel.UserGuid = account.Guid;
                userModel.UserName = account.UserName;
                userModel.NickName = account.NickName;
                userModel.UserMenuModels = menu;
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
            return this.ResultSuccess();
        }
    }
}