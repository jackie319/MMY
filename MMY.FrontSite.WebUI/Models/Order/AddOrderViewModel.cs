using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.FrontSite.WebUI.Models.Order
{
    public class AddOrderViewModel
    {
        
        public System.Guid ProductGuid { get; set; }
        public System.Guid ClassificationGuid { get; set; }
        public int ProductNumber { get; set; }
        public System.Guid DeliveryAddressGuid { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public int ProductPrice { get; set; }
        public int OrderAmount { get; set; }
    }
}