using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JK.Framework.Web.Model;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class SupplierQueryModel:QueryBase
    {
        public string SupplierName { set; get; }
        public string SupplierAddress { set; get; }

    }
}