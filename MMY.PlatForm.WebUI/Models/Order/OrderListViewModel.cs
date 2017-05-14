using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using MMY.Services.ServiceModel;

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
        public decimal OrderAmount { get; set; }

        public Guid ProductGuid { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 颜色分类
        /// </summary>
        public string ClassificationName { get; set; }
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

        public static OrderListViewModel CopyFrom(Data.Model.Order order)
        {
            OrderListViewModel model = new OrderListViewModel();
            model.Guid = order.Guid;
            model.OrderNo = order.OrderNo;
            model.OrderAmount = Convert.ToDecimal(order.OrderAmount) / 100;
            model.ProductGuid = order.ProductGuid;
            model.ClassificationName = order.ClassificationName;
            model.ProductName = order.ProductName;
            model.ProductNumber = order.ProductNumber;
            model.UserNickName = order.UserNickName;
            model.DeliveryAddress = order.DeliveryAddress;
            model.PaymentName = "";//TODO;
            model.DeliveryName = order.DeliveryName;
            model.OrderStatus = ConvertOrderStatus(order.OrderStatus);
            model.TimePaid = DateTime.Now;//TODO
            model.TimeCreated = order.TimeCreated;
            return model;
        }
        public static string ConvertOrderStatus(string orderStatus)
        {
            string result = string.Empty;
            OrderStatusEnum type;
            OrderStatusEnum.TryParse(orderStatus, out type);
            switch (type)
            {
                case OrderStatusEnum.Default:
                    result = "未支付";
                    break;
                case OrderStatusEnum.Cancel:
                    result = "已取消";
                    break;
                case OrderStatusEnum.Delivered:
                    result = "已发货";
                    break;
                case OrderStatusEnum.Finished:
                    result = "已完成";
                    break;
                case OrderStatusEnum.Paid:
                    result = "已支付";
                    break;
                case OrderStatusEnum.PayFailure:
                    result = "支付失败";
                    break;
                case OrderStatusEnum.Paying:
                    result = "支付中";
                    break;
                case OrderStatusEnum.Refund:
                    result = "已退货";
                    break;

            }
            return result;
        }
    }


}