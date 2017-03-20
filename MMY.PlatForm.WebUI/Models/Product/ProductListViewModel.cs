﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;
using MMY.Services.ServiceModel;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class ProductListViewModel
    {
        public System.DateTime TimeCreated { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsSpecialOffer { get; set; }
        public string Status { get; set; }
        public decimal PromotionPrice { get; set; }
        public decimal Price { get; set; }
        public string SaleSubTitle { get; set; }
        public string SaleTitle { get; set; }
        public string DefaultPic { get; set; }
        public int ProductNumber { get; set; }
        public System.Guid CategoryGuid { get; set; }
        public string ProductName { get; set; }
        public System.Guid Guid { get; set; }
        public Nullable<System.Guid> ParentGuid { get; set; }
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime TimeOnShelf { get; set; }
        public System.DateTime TimeOffShelf { get; set; }
        public int ImaginaryNumber { get; set; }
        public int VisitedTotal { get; set; }
        public int SoldTotal { get; set; }

        public static ProductListViewModel CopyFrom(ProductV productV)
        {
            ProductListViewModel list=new ProductListViewModel();
            list.Guid = productV.Guid;
            list.CategoryGuid = productV.Guid;
            list.CategoryName = productV.ProductName;
            list.DefaultPic = productV.DefaultPic;
            list.DisplayOrder = productV.DisplayOrder;
            list.ImaginaryNumber = productV.ImaginaryNumber;
            list.IsRecommended = productV.IsRecommended;
            list.IsSpecialOffer = productV.IsSpecialOffer;
            list.ParentGuid = productV.ParentGuid;
            list.Price = Convert.ToDecimal(productV.Price)/100;
            list.PromotionPrice = Convert.ToDecimal(productV.PromotionPrice) / 100;
            list.ProductName = productV.ProductName;
            list.ProductNumber = productV.ProductNumber;
            list.SaleSubTitle = productV.SaleSubTitle;
            list.SaleTitle = productV.SaleTitle;
            list.Status = CovertProductStatus(productV.Status);
            list.TimeOnShelf = productV.TimeOnShelf;
            list.TimeOffShelf = productV.TimeOffShelf;
            list.VisitedTotal = productV.VisitedTotal;
            list.TimeCreated = productV.TimeCreated;
            return list;
        }

        public static string CovertProductStatus(string statusStr)
        {
            ProductStatusEnum status;
            ProductStatusEnum.TryParse(statusStr, true,out status);
            string result = string.Empty;
            switch (status)
            {
                    case ProductStatusEnum.Default:
                    result = "未上架";
                    break;
                    case ProductStatusEnum.OffShelf:
                    result = "已下架";
                    break;
                    case ProductStatusEnum.OnShelf:
                    result = "已上架";
                    break;
            }
            return result;
        }
    }
}