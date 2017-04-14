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
    public class PayImpl:IPay
    {
        private IRepository<Order> _orderRepository;
        private IRepository<OrderPayRecords> _payRecordsRepository;
        private IRepository<OrderPayment> _paymentRepository;
        public PayImpl(IRepository<Order> ordeRepository,IRepository<OrderPayRecords> payRecordsRepository,IRepository<OrderPayment> paymentRepository )
        {
            _orderRepository = ordeRepository;
            _payRecordsRepository = payRecordsRepository;
            _paymentRepository = paymentRepository;
        }
        public void WechatPay(Guid orderGuid)
        {
            var order=_orderRepository.Table.FirstOrDefault(q => q.Guid == orderGuid);
            OrderPayRecords record=new OrderPayRecords();
            record.Guid = Guid.NewGuid();
            record.OrderGuid = orderGuid;
            record.PayBatch = "";//TODO:
            record.Remark = "";//TODO:
            record.TimeUpdate=DateTime.MinValue;
            record.TimeCreated=DateTime.Now;
            record.PaymentGuid = Guid.Empty;
            _payRecordsRepository.Insert(record);
        }

        public IList<OrderPayment> GetPayments()
        {
            return _paymentRepository.Table.ToList();
        }
    }
}
