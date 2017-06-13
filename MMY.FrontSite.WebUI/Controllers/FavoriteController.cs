using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.FrontSite.Domain;
using MMY.FrontSite.WebUI.Models.UserAccount;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        private IUserFavorite _userFavorite;
        public FavoriteController(IUserFavorite userFavorite)
        {
            _userFavorite = userFavorite;
        }
        /// <summary>
        /// 新增收藏
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [JKAuthorize]
        public ActionResult Add(Guid productGuid)
        {
            var user = (UserModel)HttpContext.User;
            _userFavorite.Add(user.UserGuid,productGuid);
            return this.ResultSuccess();
        }
        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult List()
        {
            var user = (UserModel)HttpContext.User;
            var query=new JK.Framework.Core.QueryBase();
            int total;
            var list=_userFavorite.GetUserFavorites(user.UserGuid,query.Skip, query.Take, out total);
            var resultList = list.Select(item => UserFavoriteViewModel.CopyFrom(item));
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [JKAuthorize]
        public ActionResult Delete(Guid uid)
        {
            _userFavorite.Delete(uid);
            return this.ResultSuccess();
        }
    }
}