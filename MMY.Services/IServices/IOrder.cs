using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;
using MMY.Services.ServiceModel;

namespace MMY.Services.IServices
{
    public interface IOrder
    {
        IList<Order> GetList(string orderNo, OrderStatusEnum? status, string userNickName,
            DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total);

        void CreateOrder(Order order);
        void CancleOrder(Guid orderGuid);

        void CreateOrderPayment(OrderPayment orderPayment);
        void CreatedOrderDelivery(OrderDelivery orderDelivery);
        IList<Order> GetUserOrders(Guid userGuid, int skip, int take, out int total);
    }
}
