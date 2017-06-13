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
    public class DeliverAddressController : Controller
    {
        public IUserAccount _UserAccount;
        public DeliverAddressController(IUserAccount userAccount)
        {
            _UserAccount = userAccount;
        }
        /// <summary>
        /// 我的收货地址
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult List()
        {
            var mmyUser = (UserModel)HttpContext.User;
            JK.Framework.Core.QueryBase query = new JK.Framework.Core.QueryBase();
            int total;
            var list = _UserAccount.GetList(mmyUser.UserGuid,query.Skip, query.Take, out total);
            var resultList = list.Select(item => UserDeliveryAddressViewModel.CopyTo(item)).ToList();
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 新增或编辑收货地址
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        [ValidationFilter]
        public ActionResult Save(UserDeliveryAddressViewModel model)
        {
            var mmyUser = (UserModel)HttpContext.User;
            var entity = model.CopyFrom();
            if (model.Guid == Guid.Empty)
            {
                entity.UserGuid = mmyUser.UserGuid;
                _UserAccount.AddDeliveryAddress(entity);
            }
            else
            {
                _UserAccount.UpdateDeliveryAddress(entity);
            }
            return this.ResultSuccess();
        }
        /// <summary>
        /// 收货地址详情
        /// </summary>
        /// <param name="addressGuid"></param>
        /// <returns></returns>
        public ActionResult Detail(Guid addressGuid)
        {
            var model=_UserAccount.FinDeliveryAddress(addressGuid);
            return this.ResultModel(model);
        }
        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="addressGuid"></param>
        /// <returns></returns>
        public ActionResult Delete(Guid addressGuid)
        {
            _UserAccount.DeleteDeliveryAddress(addressGuid);
            return this.ResultSuccess();
        }
        /// <summary>
        /// 设置默认收货地址
        /// </summary>
        /// <param name="addressGuid"></param>
        /// <returns></returns>
        public ActionResult SetDefault(Guid addressGuid)
        {
            _UserAccount.SetDefaultDeliveryAddress(addressGuid);
            return this.ResultSuccess();
        }


    }
}