using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Services.ServicesImpl
{
    public class PayImpl:IPay
    {
        private IRepository<Order> _ordeRepository;
        private IRepository<OrderPayment> _paymentRepository;
        public PayImpl(IRepository<Order> ordeRepository,IRepository<OrderPayment> paymentRepository )
        {
            _ordeRepository = ordeRepository;
            _paymentRepository = paymentRepository;
        }
        public void Pay(Guid orderGuid, Guid paymentGuid)
        {
            var order=_ordeRepository.Table.FirstOrDefault(q => q.Guid == orderGuid);
        }
    }
}
