using System;
using System.Collections.Generic;
using System.Linq;
using MMY.Data.Model;

namespace MMY.FrontSite.WebApi.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public System.Guid Guid { get; set; }
        public Guid ProductGuid { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int ProductNum { get; set; }
        public string DefaultPic { get; set; }
        public string SaleTitle { get; set; }

        public string SaleSubTitle { get; set; }
        /// <summary>
        /// 单价（被划线）
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 颜色分类
        /// </summary>
        public Guid ClassificationGuid { get; set; }

        public IList<ProductClassificationViewModel> Classifications;

        public static ShoppingCartViewModel CopyFrom(UserShoppingCart shoppingCart)
        {
            ShoppingCartViewModel model=new ShoppingCartViewModel();
            model.Guid = shoppingCart.Guid;
            model.ProductGuid = shoppingCart.ProductGuid;
            model.ProductNum = shoppingCart.ProductNum;
            model.DefaultPic = shoppingCart.Product.DefaultPic;
            model.SaleTitle = shoppingCart.Product.SaleTitle;
            model.SaleSubTitle = shoppingCart.Product.SaleSubTitle;
            model.Price = Convert.ToDecimal(shoppingCart.Product.Price)/100;
            model.PromotionPrice = Convert.ToDecimal(shoppingCart.Product.PromotionPrice)/100;
            model.TotalPrice = model.PromotionPrice * model.ProductNum;
            model.ClassificationGuid = shoppingCart.ClassificationGuid;
            model.Classifications =
                shoppingCart.Product.ProductClassification.Select(item => ProductClassificationViewModel.CopyFrom(item))
                    .ToList();
            return model;
        }
    }

    public class ProductClassificationViewModel
    {
        public System.Guid Guid { get; set; }
        //public System.Guid ProductGuid { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int PromotionPrice { get; set; }
        //public System.Guid AlbumGuid { get; set; }

        public static ProductClassificationViewModel CopyFrom(ProductClassification productClassification)
        {
            ProductClassificationViewModel model=new ProductClassificationViewModel();
            model.Guid = productClassification.Guid;
            model.Name = productClassification.Name;
            model.Price = productClassification.Price;
            model.PromotionPrice = productClassification.PromotionPrice;
            return model;
        }
    }
}