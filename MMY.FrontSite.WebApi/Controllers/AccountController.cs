﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MMY.FrontSite.WebApi.Models;
//using MMY.FrontSite.WebApi.Providers;
using MMY.FrontSite.WebApi.Results;
using MMY.Services.IServices;
using MMY.FrontSite.WebApi.Models.UserAccount;
using JK.Framework.Core;
using JK.Framework.API.Model;
using MMY.FrontSite.Domain;
using JK.Framework.Core.Caching;
using MMY.FrontSite.WebApi.App_Start;
using System.Linq;
using log4net;

namespace MMY.FrontSite.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        private IUserAccount _userAccount;
        private ISms _sms;
        private ICacheManager _cache;
        private ILog _log;
        public AccountController(IUserAccount userAccount, ISms sms, ICacheManager cache)
        {
            _userAccount = userAccount;
            _sms = sms;
            _cache = cache;
            _log = LogManager.GetLogger(typeof(AccountController));
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/account/login")]
        public ApiResultModel SubmitLogin([FromBody]LogInViewModel model)
        {
            try
            {
                var account = _userAccount.Login(model.UserName, model.PasswordMd5);
                UserModel userModel = new UserModel(account, account.NickName, true) { };
                string sessionKey = SessionManager.GetSessionKey(userModel.UserGuid);
                _cache.Remove(sessionKey);
                _cache.SetSliding(sessionKey, userModel, 100);
                var total = _cache.Total();
                _log.Info("已登录数：" + total.ToString());
                BaseApiController.AppendHeaderSessionKey(sessionKey);
            }
            catch (CommonException)
            {
                return this.ResultApiError("用户名或密码错误");
            }
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("~/api/account/sendcode")]
        [HttpGet]
        public ApiResultModel SendCode(string phone)
        {
            _sms.SendRegisteCode(phone);
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("~/api/account/register")]
        [HttpPost]
        public ApiResultModel Register([FromBody]RegisterViewModel model)
        {
            try
            {
                var entity = _sms.FindRegisteRecord(model.MobilePhone);
                if (!model.SmsCode.Equals(entity.RadomCode))
                {
                    return this.ResultApiError("验证码错误"); ;
                }
                _userAccount.Register(model.MobilePhone, model.PasswordMd5, model.SmsCode);
                _sms.Validate(entity.Guid);
            }
            catch (CommonException ex)
            {
                return this.ResultApiError(ex.Message);
            }
            return this.ResultApiSuccess();
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [Route("~/api/account/logout")]
        [ApiSessionAuthorize]
        [HttpPost]
        public ApiResultModel Logout()
        {
            //已经经过滤器进来，sessionkey一定有值
            var sessionkey = Request.Headers.GetValues("sessionkey").FirstOrDefault();
            _cache.Remove(sessionkey);
            return this.ResultApiSuccess();
        }
    }
}
