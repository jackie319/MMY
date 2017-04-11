using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.FrontSite.WebUI.Models.ShoppingCart
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
        public string ClassificationName { get; set; }

        public static ShoppingCartViewModel CopyFrom(UserShoppingCart shoppingCart)
        {
            ShoppingCartViewModel model=new ShoppingCartViewModel();
            model.Guid = shoppingCart.Guid;
            model.ProductGuid = shoppingCart.ProductGuid;
           
            return model;
        }
    }
}