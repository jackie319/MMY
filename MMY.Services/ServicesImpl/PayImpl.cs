using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using JK.Framework.Pay.Tencent;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;
using Senparc.Weixin.MP.TenPayLibV3;

namespace MMY.Services.ServicesImpl
{
    public class PayImpl:IPay
    {
        private IRepository<Order> _orderRepository;
        private IRepository<OrderPayRecords> _payRecordsRepository;
        private IRepository<OrderPayment> _paymentRepository;
        private IRepository<Product> _productRepository;
        public PayImpl(IRepository<Order> ordeRepository,IRepository<OrderPayRecords> payRecordsRepository,IRepository<OrderPayment> paymentRepository,IRepository<Product> productRepository  )
        {
            _orderRepository = ordeRepository;
            _payRecordsRepository = payRecordsRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
        }
        /// <summary>
        /// 支付前检查商品库存，支付成功后扣减库存
        /// </summary>
        /// <param name="orderGuid"></param>
        public void WechatPay(Guid orderGuid)
        {
            var order=_orderRepository.Table.FirstOrDefault(q => q.Guid == orderGuid);
            var product = _productRepository.Table.FirstOrDefault(q => q.Guid == order.ProductGuid);
            if(product.ProductNumber<1)throw new CommonException("商品库存不足");
            OrderPayRecords record=new OrderPayRecords();
            record.Guid = Guid.NewGuid();
            record.OrderGuid = orderGuid;
            record.PayBatch = Guid.Empty;//TODO:
            record.PayResult = "";
            record.Remark = "";//TODO:
            record.TimeUpdate=DateTime.MinValue;
            record.TimeCreated=DateTime.Now;
            record.PaymentGuid = GetWechatPay().Guid;
            _payRecordsRepository.Insert(record);
            if (order != null)
            {
                order.OrderStatus = OrderStatusEnum.Paying.ToString();
                _orderRepository.Update(order);
            }
            //TenPayV3UnifiedorderRequestData data=new TenPayV3UnifiedorderRequestData();
            //var result=UnifiedOrder.Pay(data);
        }

        public IList<OrderPayment> GetPayments()
        {
            return _paymentRepository.Table.Where(q=> !q.IsDeleted).ToList();
        }

        public OrderPayment GetWechatPay()
        {
            string wechat = PaymentEnum.WechatPay.ToString();
            return _paymentRepository.Table.FirstOrDefault(q => q.PaymentType.Equals(wechat) && !q.IsDeleted);
        }
    }
}
