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
            //生成订单和支付前需检查 
            //1.该商品状态（有没有被下架，删除等）
            //2.商品价格有没有变动（如果后台修改了商品价格则需重新下单）
            //3.商品库存（下单时检查是否还有库存，支付时扣除库存，同时检查是否还有库存）
            var entity = model.CopyTo();
            //用户信息
            entity.UserGuid = mmyUser.UserGuid;
            entity.UserName = mmyUser.UserName;
            entity.UserNickName = mmyUser.NickName;

            //商品信息
            var product=_product.FindProduct(entity.ProductGuid);
            var classification = product.ProductClassification.FirstOrDefault(q => q.Guid == entity.ClassificationGuid);
            entity.ProductName = product.SaleTitle;
            if (classification != null)
            {
                entity.ClassificationName = classification.Name;
            }
            //收货地址
            var address=_userAccount.FinDeliveryAddress(entity.DeliveryAddressGuid);
            string myAddress = string.Format("{0},{1},{2},{3}",address.ReceiverName,address.Phone,address.Address,address.ZipCode);
            entity.DeliveryAddress = myAddress;
            _order.CreateOrder(entity);
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