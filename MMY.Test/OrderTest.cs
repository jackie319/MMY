using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Test
{
    [TestClass]
    public class OrderTest
    {
        private IOrder _order;
        [TestInitialize]
        public void Initialize()
        {
            Initial.RegisterAutofac();
            _order = Initial._container.Resolve<IOrder>();
        }
        [TestMethod]
        public void CreateOrderTest()
        {
            Order order=new Order();
            order.OrderAmount = 123;
            order.OrderNo = "13212313123123";
            order.ProductGuid = Guid.Parse("D1C9886C-8228-48BE-938C-38BD9F0CBF37");
            order.ProductName = "单元测试";
            order.ProductNumber = 1;
            order.ProductPrice = 123;
            order.UserGuid=Guid.Empty;
            order.UserName = string.Empty;
            order.UserNickName = string.Empty;
            _order.CreateOrder(order);
        }
        [TestMethod]
        public void GetListTest()
        {
            int total;
            var list=_order.GetList("", null, "", null, null, 0, 20, out total);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CreateOrderPaymentTest()
        {
            OrderPayment orderPayment=new OrderPayment();
            orderPayment.PaymentName = "支付宝";
           _order.CreateOrderPayment(orderPayment);
        }

        [TestMethod]
        public void CreatedOrderDeliveryTest()
        {
            OrderDelivery delivery=new OrderDelivery();
            delivery.DeliveryName = "顺丰";
            _order.CreatedOrderDelivery(delivery);
        }
    }
}
