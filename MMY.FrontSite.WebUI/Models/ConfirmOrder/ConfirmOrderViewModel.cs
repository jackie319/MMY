using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Services.ServicesImpl;

namespace MMY.FrontSite.WebUI.Models.ConfirmOrder
{
    public class ConfirmOrderViewModel
    {
        /// <summary>
        /// 产品guid
        /// </summary>
        public Guid ProductGuid { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string DefaultPic { get; set; }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string SaleTitle { get; set; }
        /// <summary>
        /// 商品副标题
        /// </summary>
        public string SaleSubTitle { get; set; }

        /// <summary>
        /// 颜色分类名
        /// </summary>
        public string ClassificationName { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品优惠价格
        /// </summary>
        public decimal PromotionPrice { get; set; }
        /// <summary>
        /// 商品克数
        /// </summary>
        public int Grams { get; set; }
     
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        public static ConfirmOrderViewModel CopyFrom(Data.Model.Product product)
        {
            ConfirmOrderViewModel model=new ConfirmOrderViewModel();
            model.ProductGuid = product.Guid;
            model.SaleTitle = product.SaleTitle;
            model.SaleSubTitle = product.SaleSubTitle;
            model.DefaultPic = AppSetting.Instance().PictureUrl+product.DefaultPic;
            model.Price = Convert.ToDecimal(product.Price)/100;
            model.PromotionPrice = Convert.ToDecimal(product.PromotionPrice)/100;
            return model;
        }
    }
}