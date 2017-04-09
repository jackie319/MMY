using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JK.Framework.Web.Model;
using MMY.Services.ServiceModel;

namespace MMY.PlatForm.WebUI.Models.Order
{
    public class OrderQueryModel:QueryBase
    {
       public string OrderNo { set; get; }
        public string UserNickName { set; get; }

        public OrderStatusEnum? Status { get; set; }
        public DateTime? TimeCreatedBegin { set; get; }
        public DateTime? TimeCreatedEnd { set; get; }


    }
}