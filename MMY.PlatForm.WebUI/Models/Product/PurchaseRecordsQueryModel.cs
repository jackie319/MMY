using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JK.Framework.Web.Model;
using JK.Framework.Core;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class PurchaseRecordsQueryModel:QueryBase
    {
       public Guid? ProductGuid { set; get; }
        public Guid? SupplierGuid { set; get; }
        //TODO:查询条件待完善
    }
}