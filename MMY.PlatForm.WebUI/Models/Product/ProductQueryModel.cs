using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JK.Framework.Web.Model;
using MMY.Services.ServiceModel;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class ProductQueryModel: QueryBase
    {
        public string ProductName { set; get; }
        public Guid CategoryGuid { set; get; }
        public ProductStatusEnum Status { set; get; }
        public Boolean IsSpecialOffer { set; get; }
        public Boolean IsRecommended { set; get; }
        public DateTime TimeCreatedBegin { set; get; }
        public DateTime TimeCreatedEnd { set; get; }
    }
}