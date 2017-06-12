using System;
using MMY.Data.Model;

namespace MMY.FrontSite.WebApi.Models.Product
{
    public class ProductListViewModel
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime TimeCreated { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommended { get; set; }
        /// <summary>
        /// 是否特卖
        /// </summary>
        public bool IsSpecialOffer { get; set; }
        /// <summary>
        /// 优惠价格
        /// </summary>
        public decimal PromotionPrice { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品副标题
        /// </summary>
        public string SaleSubTitle { get; set; }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string SaleTitle { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string DefaultPic { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public int ProductNumber { get; set; }
        /// <summary>
        /// 分类Guid
        /// </summary>
        public System.Guid CategoryGuid { get; set; }
        /// <summary>
        /// 当前条目Guid
        /// </summary>
        public System.Guid Guid { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 人工排序
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 访问数
        /// </summary>
        public int VisitedTotal { get; set; }
        /// <summary>
        /// 销售数
        /// </summary>
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