using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models.Order
{
    public class OrderListViewModel
    {
        public System.Guid Guid { get; set; }
        public string OrderNo { get; set; }
        public int OrderAmount { get; set; }
        public string ProductName { get; set; }
        public int ProductNumber { get; set; }
        public string UserNickName { get; set; }
        public string DeliveryAddress { get; set; }
        public string PaymentName { get; set; }
        public string PayBatch { get; set; }
        public string DeliveryName { get; set; }
        public string OrderStatus { get; set; }
        public System.DateTime TimePaid { get; set; }
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