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
            var query = _orderRepository.Table.Where(q=>q.Guid!=Guid.Empty);
            if (!string.IsNullOrEmpty(orderNo))
            {
                query = query.Where(q => q.OrderNo.Contains(orderNo));
            }
            if (status != null)
            {
                var statusStr = status.ToString();
                query = query.Where(q => q.OrderStatus.Equals(statusStr));
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
            order.OrderNo = CreateOrderNo();
            order.OrderAmount = order.ProductNumber * order.ProductPrice;
            order.TimeCreated=DateTime.Now;
            order.DeliveryGuid = Guid.Empty;//TODO:
            order.DeliveryName = string.Empty;
            order.OrderStatus = OrderStatusEnum.Default.ToString();
            _orderRepository.Insert(order);
        }

        private string CreateOrderNo()
        {
            string date = DateTime.Now.ToString("yyMMddHHmmss");
            //种子精确到百纳秒级别
            int ram =new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(),0)).Next(100000, 999999);
            return string.Format(date+ram);
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

        public void CancleOrder(Guid orderGuid)
        {
            var order=_orderRepository.Table.FirstOrDefault(q => q.Guid == orderGuid);
            if (order == null) return;
            order.OrderStatus = OrderStatusEnum.Cancel.ToString();
            _orderRepository.Update(order);
        }
    }
}
