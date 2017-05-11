using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models.Order
{
    public class OrderUpdatePriceViewModel
    {
        public Guid OrderGuid { get; set; }

        public Decimal Amount { get; set; }
    }
}