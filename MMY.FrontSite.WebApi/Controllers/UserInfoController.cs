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
using System.Web;
using System.Web.Http;

namespace MMY.FrontSite.WebApi.Controllers
{
    public class UserInfoController :ApiController
    {
        private IUserAccount _UserAccount;
        private ICacheManager _cache;
        public UserInfoController(IUserAccount userAccount,ICacheManager cache)
        {
            _UserAccount = userAccount;
            _cache = cache;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [ApiSessionAuthorize]
        [Route("~/api/userinfo")]
        [HttpGet]
        public UserInfoViewModel UserInfo()
        {
            var userModel =(UserModel) HttpContext.Current.User;
            var userInfo = _UserAccount.FindUserAccount(userModel.UserGuid);
            var result = UserInfoViewModel.CopyFrom(userInfo);
            return result;

        }
    }
}
