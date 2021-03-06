﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.FrontSite.WebUI.Models.Product
{
    public class ProductViewModel
    {
        public System.Guid Guid { get; set; }
        public System.Guid CategoryGuid { get; set; }
        public int ProductNumber { get; set; }
        public string DefaultPic { get; set; }
        public string SaleTitle { get; set; }
        public string SaleSubTitle { get; set; }
        public Decimal Price { get; set; }
        public Decimal PromotionPrice { get; set; }
        public string ProductDetail { get; set; }
        public string ProductRemark { get; set; }
        public bool IsSpecialOffer { get; set; }
        public bool IsRecommended { get; set; }
        //  public int DisplayOrder { get; set; }

        public IList<ProductClassificationViewModel> Classifications { set; get; }

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
        public string Name { get; set; }
        public int Number { get; set; }
        public Decimal Price { get; set; }
        public Decimal PromotionPrice { get; set; }
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
        public string ImageUrl { get; set; }
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