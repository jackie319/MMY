using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.FrontSite.Domain;
using MMY.FrontSite.WebUI.Models.Order;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrder _order;
        private IProduct _product;
        private IUserAccount _userAccount;
        public OrderController(IOrder order,IProduct product,IUserAccount userAccount)
        {
            _order = order;
            _product = product;
            _userAccount = userAccount;
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult Add(AddOrderViewModel model)
        {
            var mmyUser=(UserModel)HttpContext.User;
            var entity = model.CopyTo();
            //用户信息
            entity.UserGuid = mmyUser.UserGuid;
            entity.UserName = mmyUser.UserName;
            entity.UserNickName = mmyUser.NickName;
            try
            {
                _order.CreateOrder(entity);
            }
            catch (CommonException ex)
            {
                return this.ResultError(ex.Message);
            }
      
            return this.ResultSuccess();
        }
        /// <summary>
        /// 我的订单
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult List()
        {
            var query = new QueryBase();
            var user = (UserModel)HttpContext.User;
            int total;
            var list = _order.GetUserOrders(user.UserGuid, query.Skip, query.Take, out total);
            var resultList = list.Select(item => UserOrdersViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderGuid"></param>
        /// <returns></returns>
        public ActionResult Cancel(Guid orderGuid)
        {
            _order.CancleOrder(orderGuid);
            return this.ResultSuccess();
        }

        /// <summary>
        /// 退货
        /// </summary>
        /// <param name="orderGuid"></param>
        /// <returns></returns>
        public ActionResult Refund(Guid orderGuid)
        {
            //TODO:
            return this.ResultSuccess();
        }
    }
}