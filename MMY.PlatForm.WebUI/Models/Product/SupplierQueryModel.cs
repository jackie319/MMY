using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class SupplierQueryModel
    {
        public string SupplierName { set; get; }
        public string SupplierAddress { set; get; }

        public int Skip { set; get; }

        public int Take { set; get; }
    }
}