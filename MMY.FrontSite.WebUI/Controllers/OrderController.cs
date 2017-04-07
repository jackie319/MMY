using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.FrontSite.Domain;
using MMY.FrontSite.WebUI.Models.Order;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
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
            return this.ResultSuccess();
        }

        /// <summary>
        /// 退货
        /// </summary>
        /// <param name="orderGuid"></param>
        /// <returns></returns>
        public ActionResult Refund(Guid orderGuid)
        {
            return this.ResultSuccess();
        }
    }
}