using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class ProductListViewModel
    {
        public Guid Guid { set; get; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierAddress { get; set; }
        public System.DateTime TimeCreated { get; set; }
    }

    
}