using System;
using MMY.Data.Model;

namespace MMY.FrontSite.WebApi.Models.Product
{
    public class ProductListViewModel
    {
        public System.DateTime TimeCreated { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsSpecialOffer { get; set; }
        public decimal PromotionPrice { get; set; }
        public decimal Price { get; set; }
        public string SaleSubTitle { get; set; }
        public string SaleTitle { get; set; }
        public string DefaultPic { get; set; }
        public int ProductNumber { get; set; }
        public System.Guid CategoryGuid { get; set; }
        public System.Guid Guid { get; set; }
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }
        public int VisitedTotal { get; set; }
        public int SoldTotal { get; set; }

        public static ProductListViewModel CopyFrom(ProductV productV)
        {
            ProductListViewModel list = new ProductListViewModel();
            list.Guid = productV.Guid;
            list.CategoryGuid = productV.Guid;
            list.CategoryName = productV.ProductName;
            list.DefaultPic = productV.DefaultPic;
            list.DisplayOrder = productV.DisplayOrder;
            list.IsRecommended = productV.IsRecommended;
            list.IsSpecialOffer = productV.IsSpecialOffer;
            list.Price = Convert.ToDecimal(productV.Price) / 100;
            list.PromotionPrice = Convert.ToDecimal(productV.PromotionPrice) / 100;
            list.ProductNumber = productV.ProductNumber;
            list.SaleSubTitle = productV.SaleSubTitle;
            list.SaleTitle = productV.SaleTitle;
            list.VisitedTotal = productV.VisitedTotal;
            list.TimeCreated = productV.TimeCreated;
            return list;
        }
    }
}