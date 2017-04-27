using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models.Order
{
    public class ChangePriceViewModel
    {
        public Guid OrderGuid { get; set; }

        public int Price { get; set; }
    }
}