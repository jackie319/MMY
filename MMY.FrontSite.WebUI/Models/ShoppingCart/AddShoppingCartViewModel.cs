using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.FrontSite.WebUI.Models.ShoppingCart
{
    public class AddShoppingCartViewModel
    {
        public Guid ProductGuid { get; set; }
        public Guid ClassificationGuid { get; set; }
        public int ProductNum { set; get; }
    }
}