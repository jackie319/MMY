using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.FrontSite.Domain;
using MMY.FrontSite.WebUI.Models.UserAccount;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class UserInfoController : Controller
    {
        public IUserAccount _UserAccount;
        public UserInfoController(IUserAccount userAccount)
        {
            _UserAccount = userAccount;
        }
        /// <summary>
        /// 获取用户信息(实时)
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult UserInfo()
        {
            var mmyUser = (UserModel)HttpContext.User;
            var userInfo = _UserAccount.FindUserAccount(mmyUser.UserGuid);
            var result = UserInfoViewModel.CopyFrom(userInfo);
            return this.ResultModel(result);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [JKAuthorize]
        [ValidationFilter]
        public ActionResult Update(UserInfoViewModel model)
        {
            var mmyUser = (UserModel)HttpContext.User;
            var entity = model.CopyTo();
            entity.Guid = mmyUser.UserGuid;
            entity.UserName = mmyUser.UserName;
            _UserAccount.UpdateUserInfo(entity);
            return this.ResultSuccess();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [JKAuthorize]
        [ValidationFilter]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!model.NewPasswordMd5Confirm.Equals(model.NewPasswordMd5))
                return this.ResultError("俩次输入的密码不匹配");
            try
            {
                var mmyUser = (UserModel)HttpContext.User;
                _UserAccount.ChangePwd(mmyUser.UserName, model.OldPasswordMd5, model.NewPasswordMd5);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }
            return this.ResultSuccess();
        }
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [JKAuthorize]
        [ValidationFilter]
        public ActionResult GetBackPassword(GetBackPasswrodViewModel model)
        {
            var mmyUser = (UserModel)HttpContext.User;
            try
            {
                _UserAccount.GetBackPassword(mmyUser.UserName, model.PasswordMd5, model.SmsCode);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }
            return this.ResultSuccess();
        }
    }
}