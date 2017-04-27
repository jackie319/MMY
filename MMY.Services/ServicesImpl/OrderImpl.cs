using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.Services.ServicesImpl
{
    public class OrderImpl : IOrder
    {
        private IRepository<Order> _orderRepository;
        private IRepository<OrderPayment> _orderPaymentRepository;
        private IRepository<OrderDelivery> _orderDeliveryRepository;
        private IProduct _product;
        private IUserAccount _userAccount;
        public OrderImpl(IRepository<Order> ordeRepository, IRepository<OrderPayment> orderPaymentRepository, IRepository<OrderDelivery> orderDeliveryRepository, IProduct product,IUserAccount userAccount)
        {
            _orderRepository = ordeRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderPaymentRepository = orderPaymentRepository;
            _product = product;
            _userAccount = userAccount;
        }
        public IList<Order> GetList(string orderNo, OrderStatusEnum? status, string userNickName,
            DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total)
        {
            var query = _orderRepository.Table.Where(q => q.Guid != Guid.Empty);
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

        public IList<Order> GetUserOrders(Guid userGuid, int skip, int take, out int total)
        {
            var query = _orderRepository.Table.Where(q => q.UserGuid == userGuid);
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// 生成订单和支付前需检查 
        /// 1.该商品状态（有没有被下架，删除等）
        /// 2.商品价格有没有变动（如果后台修改了商品价格则需重新下单）
        /// 3.商品库存（下单时检查是否还有库存，支付时扣除库存，同时检查是否还有库存）
        /// <param name="order"></param>
        public void CreateOrder(Order order)
        {
            //商品信息
            var product = _product.FindProduct(order.ProductGuid);
            if (!product.Status.Equals(ProductStatusEnum.OnShelf.ToString()))
            {
                throw new CommonException("商品已下架");
            }
            if (product.PromotionPrice != order.ProductPrice)
            {
                throw new CommonException("商品已被更改，请重新下单");
            }

            if (product.ProductNumber < 1)
            {
                throw new CommonException("商品库存不足");
            }
            var classification = product.ProductClassification.FirstOrDefault(q => q.Guid == order.ClassificationGuid);
            order.ProductName = product.SaleTitle;//商品名称
            if (classification != null)
            {
                order.ClassificationName = classification.Name;
            }

            //收货地址
            var address = _userAccount.FinDeliveryAddress(order.DeliveryAddressGuid);
            string myAddress = string.Format("{0},{1},{2},{3}", address.ReceiverName, address.Phone, address.Address, address.ZipCode);
            order.DeliveryAddress = myAddress;

            order.Guid = Guid.NewGuid();
            order.OrderNo = CreateOrderNo();
            order.OrderAmount = order.ProductNumber * order.ProductPrice;
            order.TimeCreated = DateTime.Now;
            order.DeliveryGuid = Guid.Empty;//TODO:
            order.DeliveryName = string.Empty;
            order.OrderStatus = OrderStatusEnum.Default.ToString();
            _orderRepository.Insert(order);
        }

        private string CreateOrderNo()
        {
            string date = DateTime.Now.ToString("yyMMddHHmmss");
            //种子精确到百纳秒级别
            int ram = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0)).Next(100000, 999999);
            return string.Format(date + ram);
        }

        public void CreateOrderPayment(OrderPayment orderPayment)
        {
            orderPayment.Guid = Guid.NewGuid();
            orderPayment.TimeCreated = DateTime.Now;
            _orderPaymentRepository.Insert(orderPayment);
        }

        public void CreatedOrderDelivery(OrderDelivery orderDelivery)
        {
            orderDelivery.Guid = Guid.NewGuid();
            orderDelivery.TimeCreated = DateTime.Now;
            _orderDeliveryRepository.Insert(orderDelivery);
        }

        public void CancleOrder(Guid orderGuid)
        {
            var order = _orderRepository.Table.FirstOrDefault(q => q.Guid == orderGuid);
            if (order == null) return;
            order.OrderStatus = OrderStatusEnum.Cancel.ToString();
            _orderRepository.Update(order);
        }

        public void ChangeOrderPrice(Guid orderGuid, int price)
        {
            var order = _orderRepository.Table.FirstOrDefault(q => q.Guid == orderGuid);
            if (order.OrderStatus.Equals(OrderStatusEnum.Default.ToString())) {
                order.OrderAmount = price;
                _orderRepository.Update(order);
            }
        }
    }
}
