using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models.Order
{
    public class OrderListViewModel
    {
        public System.Guid Guid { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public int OrderAmount { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int ProductNumber { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserNickName { get; set; }
        /// <summary>
        ///收货地址
        /// </summary>
        public string DeliveryAddress { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentName { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string PayBatch { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public string DeliveryName { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public System.DateTime TimePaid { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public System.DateTime TimeCreated { get; set; }

        public static OrderListViewModel CopyFrom(Data.Model.Order order)
        {
            OrderListViewModel model=new OrderListViewModel();
            model.Guid = order.Guid;
            model.OrderNo = order.OrderNo;
            model.OrderAmount = order.OrderAmount;
            model.ProductName = order.ProductName;
            model.ProductNumber = order.ProductNumber;
            model.UserNickName = order.UserNickName;
            model.DeliveryAddress = order.DeliveryAddress;
            model.PaymentName = order.PaymentName;
            model.PayBatch = order.PayBatch;
            model.DeliveryName = order.DeliveryName;
            model.OrderStatus = order.OrderStatus;
            model.TimePaid = order.TimePaid;
            model.TimeCreated = order.TimeCreated;
            return model;
        }
    }
}