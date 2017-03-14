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
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserAccount _userAccount;

        public AccountController(IUserAccount userAccount)
        {
            _userAccount = userAccount;
        }
        public ActionResult Login()
        {
            string md5 = "12345678".ToMd5();
            string md5salt = md5.ToMd5WithSalt("_MMYPlatform");
            try
            {
                _userAccount.Login("Jackie", "12345678");
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