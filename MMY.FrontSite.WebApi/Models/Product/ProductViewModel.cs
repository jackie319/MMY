using System;
using System.Collections.Generic;
using System.Linq;
using MMY.Data.Model;

namespace MMY.FrontSite.WebApi.Models.Product
{
    public class ProductViewModel
    {
        public System.Guid Guid { get; set; }
        public System.Guid CategoryGuid { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int ProductNumber { get; set; }
        /// <summary>
        /// 封面图片
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
        /// 价格
        /// </summary>
        public Decimal Price { get; set; }
        /// <summary>
        /// 优惠价格
        /// </summary>
        public Decimal PromotionPrice { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        public string ProductDetail { get; set; }
        /// <summary>
        /// 商品备注
        /// </summary>
        public string ProductRemark { get; set; }
        /// <summary>
        /// 是否特卖
        /// </summary>
        public bool IsSpecialOffer { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommended { get; set; }
        //  public int DisplayOrder { get; set; }

        /// <summary>
        /// 颜色分类
        /// </summary>
        public IList<ProductClassificationViewModel> Classifications { set; get; }

        /// <summary>
        /// 商品相册
        /// </summary>
        public IList<ProductAlbumViewModel> Albums { set; get; }

        public static ProductViewModel CopyFrom(Data.Model.Product product)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Guid = product.Guid;
            productViewModel.CategoryGuid = product.CategoryGuid;
            productViewModel.DefaultPic = product.DefaultPic;
            //productViewModel.DisplayOrder = product.DisplayOrder;
            productViewModel.IsRecommended = product.IsRecommended;
            productViewModel.IsSpecialOffer = product.IsSpecialOffer;
            productViewModel.Price = Convert.ToDecimal(product.Price) / 100;
            productViewModel.PromotionPrice = Convert.ToDecimal(product.PromotionPrice) / 100;
            productViewModel.ProductDetail = product.ProductDetail;
            productViewModel.ProductRemark = product.ProductRemark;
            productViewModel.SaleTitle = product.SaleTitle;
            productViewModel.SaleSubTitle = product.SaleSubTitle;
            productViewModel.ProductNumber = product.ProductNumber;
            var classes = product.ProductClassification.Select(item => ProductClassificationViewModel.CopyFrom(item)).ToList();
            var albumList = product.ProductAlbum.Select(item => ProductAlbumViewModel.CopyFrom(item)).ToList();
            productViewModel.Classifications = classes;
            productViewModel.Albums = albumList;
            return productViewModel;
        }

      
    }

    public class ProductClassificationViewModel
    {
        public System.Guid Guid { get; set; }
        public System.Guid ProductGuid { get; set; }
        /// <summary>
        /// 颜色分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public Decimal Price { get; set; }
        /// <summary>
        /// 优惠价格
        /// </summary>
        public Decimal PromotionPrice { get; set; }
        /// <summary>
        /// 颜色分类图片
        /// </summary>
        public string PicUrl { get; set; }

        public static ProductClassificationViewModel CopyFrom(ProductClassification productClassification)
        {
            ProductClassificationViewModel viewModel = new ProductClassificationViewModel();
            viewModel.Guid = productClassification.Guid;
            viewModel.ProductGuid = productClassification.ProductGuid;
            viewModel.Name = productClassification.Name;
            viewModel.Number = productClassification.Number;
            viewModel.Price = Convert.ToDecimal(productClassification.Price) / 100;
            viewModel.PromotionPrice = Convert.ToDecimal(productClassification.PromotionPrice) / 100;
            viewModel.PicUrl = productClassification.PicUrl;
            return viewModel;
        }
      

    }

    public class ProductAlbumViewModel
    {
        public System.Guid Guid { get; set; }
        public System.Guid ProductGuid { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        public static ProductAlbumViewModel CopyFrom(ProductAlbum album)
        {
            ProductAlbumViewModel model = new ProductAlbumViewModel();
            model.Guid = album.Guid;
            model.ProductGuid = album.ProductGuid;
            model.ImageUrl = album.ImageUrl;
            model.DisplayOrder = album.DisplayOrder;
            return model;
        }

     
    }
}