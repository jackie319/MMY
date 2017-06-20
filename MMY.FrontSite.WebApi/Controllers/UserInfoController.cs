using JK.Framework.Core;
using JK.Framework.Core.Caching;
using MMY.FrontSite.Domain;
using MMY.FrontSite.WebApi.App_Start;
using MMY.FrontSite.WebApi.Models.UserAccount;
using MMY.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MMY.FrontSite.WebApi.Controllers
{
    public class UserInfoController : ApiController
    {
        private IUserAccount _UserAccount;
        public ICacheManager _cache;
        public UserInfoController(IUserAccount userAccount,ICacheManager cache)
        {
            _UserAccount = userAccount;
            _cache = cache;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Route("~/api/userinfo")]
        [HttpGet]
        public UserInfoViewModel UserInfo()
        {
            var sessionkey = string.Empty;
            if (Request.Headers.Contains("sessionkey"))
            {
                try
                {
                    sessionkey = Request.Headers.GetValues("sessionkey").FirstOrDefault();
                }
                catch (ArgumentException)
                {
                }
                if (string.IsNullOrEmpty(sessionkey))
                {
                    throw new MMYAuthorizeException("无效的sessionkey");
                }

                var flag = _cache.IsSet(sessionkey);
                if (!flag) throw new MMYAuthorizeException("无效的sessionkey");
                var userModel = _cache.Get<UserModel>(sessionkey);
                var userInfo = _UserAccount.FindUserAccount(userModel.UserGuid);
                var result = UserInfoViewModel.CopyFrom(userInfo);
                return result;
            }
            else
            {
                throw new CommonException("缺少参数sessionkey");
            }
            
        }
    }
}
