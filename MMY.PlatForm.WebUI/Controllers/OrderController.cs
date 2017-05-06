using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Model;
using MMY.PlatForm.WebUI.Models.Order;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }
        public ActionResult List(OrderQueryModel model)
        {
            int total;
            var list=_order.GetList(model.OrderNo,model.Status, model.UserNickName, model.TimeCreatedBegin, model.TimeCreatedEnd, model.Skip,
                model.Take, out total);
            var resultList = list.Select(q => OrderListViewModel.CopyFrom(q)).ToList();
            return this.ResultListModel(total,resultList);
        }
    }
}