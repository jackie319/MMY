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
        private IRepository<Order> _ordeRepository;
        public OrderImpl(IRepository<Order> ordeRepository)
        {
            _ordeRepository = ordeRepository;
        }
        public IList<Order> GetList(string orderNo, OrderStatusEnum? status, string userNickName,
            DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total)
        {
            var query = _ordeRepository.Table.Where(q=>q.Guid!=Guid.Empty);
            if (!string.IsNullOrEmpty(orderNo))
            {
                query = query.Where(q => q.OrderNo.Contains(orderNo));
            }
            if (status != null)
            {
                query = query.Where(q => q.Equals(status.ToString()));
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
    }
}
