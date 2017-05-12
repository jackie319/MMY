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
        private ISms _sms;
        public AccountController(IUserAccount userAccount, ISms sms)
        {
            _userAccount = userAccount;
            _sms = sms;
        }

        public ActionResult UserLogin()
        {
            return View();
        }

        public ActionResult UserRegister()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordMd5"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitLogin(string userName, string passwordMd5)
        {
            try
            {
                var account = _userAccount.Login(userName, passwordMd5);
                UserModel userModel = new UserModel(account, account.NickName, true) { };
                HttpContext.User = userModel;
                Session["UserInfoModel"] = userModel;
            }
            catch (CommonException)
            {
                return this.ResultError("用户名或密码错误");
            }
            return this.ResultSuccess();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidationFilter]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                var entity = _sms.FindRegisteRecord(model.MobilePhone);
                if (!model.SmsCode.Equals(entity.RadomCode))
                {
                    return this.ResultError("验证码错误");
                }
                _userAccount.Register(model.MobilePhone, model.PasswordMd5, model.SmsCode);
                _sms.Validate(entity.Guid);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }
            return this.ResultSuccess();
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ActionResult SendCode(string phone)
        {
            _sms.SendRegisteCode(phone);
            return this.ResultSuccess();
        }

        [JKAuthorize]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return this.ResultSuccess();
        }


    }
}