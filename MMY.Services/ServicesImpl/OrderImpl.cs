using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.Services.ServicesImpl
{
    public class OrderImpl:IOrder
    {
        private IRepository<Order> _orderRepository;
        private IRepository<OrderPayment> _orderPaymentRepository;
        private IRepository<OrderDelivery> _orderDeliveryRepository;
        public OrderImpl(IRepository<Order> ordeRepository, IRepository<OrderPayment> orderPaymentRepository, IRepository<OrderDelivery> orderDeliveryRepository)
        {
            _orderRepository = ordeRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderPaymentRepository = orderPaymentRepository;
        }
        public IList<Order> GetList(string orderNo, OrderStatusEnum? status, string userNickName,
            DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total)
        {
            var guid = Guid.Empty;
            var query = _orderRepository.Table.Where(q=>q.Guid!= guid);
            if (!string.IsNullOrEmpty(orderNo))
            {
                query = query.Where(q => q.OrderNo.Contains(orderNo));
            }
            if (status != null)
            {
                var statusStr = status.ToString();
                query = query.Where(q => q.Equals(statusStr));
            }
            if (!string.IsNullOrEmpty(userNickName))
            {
                query = query.Where(q => q.UserNickName.Contains(userNickName));
            }
            if (timeCreatedBegin != null)
            {
                query = query.Where(q => q.TimeCreated >= timeCreatedBegin);
            }
            if (timeCreatedEnd != null)
            {
                query = query.Where(q => q.TimeCreated < timeCreatedEnd);
            }
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public IList<Order> GetUserOrders(Guid userGuid,int skip,int take,out int total)
        {
            var query = _orderRepository.Table.Where(q => q.UserGuid==userGuid);
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public void CreateOrder(Order order)
        {
            order.Guid=Guid.NewGuid();
            order.TimeCreated=DateTime.Now;
            order.PayBatch = string.Empty;
            order.PaymentGuid = Guid.Empty;
            order.PaymentName = string.Empty;
            order.DeliveryGuid = Guid.Empty;
            order.DeliveryName = string.Empty;
            order.DeliveryAddressGuid = Guid.Empty;
            order.DeliveryAddress = string.Empty;
            order.OrderStatus = OrderStatusEnum.Default.ToString();
            _orderRepository.Insert(order);
        }

        private string CreateOrderNo()
        {
            return string.Empty;
        }

        public void CreateOrderPayment(OrderPayment orderPayment)
        {
            orderPayment.Guid=Guid.NewGuid();
            orderPayment.TimeCreated=DateTime.Now;
            _orderPaymentRepository.Insert(orderPayment);
        }

        public void CreatedOrderDelivery(OrderDelivery orderDelivery)
        {
            orderDelivery.Guid = Guid.NewGuid();
            orderDelivery.TimeCreated=DateTime.Now;
           _orderDeliveryRepository.Insert(orderDelivery);
        }
    }
}
