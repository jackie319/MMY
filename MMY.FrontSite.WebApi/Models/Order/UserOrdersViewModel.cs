﻿using System;

namespace MMY.FrontSite.WebApi.Models.Order
{
    public class UserOrdersViewModel
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

        public static UserOrdersViewModel CopyFrom(Data.Model.Order order)
        {
            UserOrdersViewModel model = new UserOrdersViewModel();
            model.Guid = order.Guid;
            model.OrderNo = order.OrderNo;
            model.OrderAmount = order.OrderAmount;
            model.ProductName = order.ProductName;
            model.ProductNumber = order.ProductNumber;
            model.UserNickName = order.UserNickName;
            model.DeliveryAddress = order.DeliveryAddress;
            model.PaymentName = "";//TODO;
            model.DeliveryName = order.DeliveryName;
            model.OrderStatus = order.OrderStatus;
            model.TimePaid = DateTime.Now;//TODO
            model.TimeCreated = order.TimeCreated;
            return model;
        }

    }
}