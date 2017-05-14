using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class ProductViewModel
    {
        public System.Guid? Guid { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public System.Guid CategoryGuid { get; set; }
        public int ProductNumber { get; set; }
        [Required]
        public string DefaultPic { get; set; }
        [Required]
        public string SaleTitle { get; set; }
        [Required]
        public string SaleSubTitle { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public Decimal PromotionPrice { get; set; }
        [Required]
        public string ProductDetail { get; set; }
        [Required]
        public string ProductRemark { get; set; }
        public bool IsSpecialOffer { get; set; }
        public bool IsRecommended { get; set; }
      //  public int DisplayOrder { get; set; }
        public int ImaginaryNumber { get; set; }

        public IList<ProductClassificationViewModel> Classifications{set;get;}

        public  IList<ProductAlbumViewModel> Albums { set; get; }

        public static ProductViewModel CopyFrom(Data.Model.Product product)
        {
            ProductViewModel productViewModel=new ProductViewModel();
            productViewModel.Guid = product.Guid;
            productViewModel.CategoryGuid = product.CategoryGuid;
            productViewModel.DefaultPic = product.DefaultPic;
            //productViewModel.DisplayOrder = product.DisplayOrder;
            productViewModel.ImaginaryNumber = product.ImaginaryNumber;
            productViewModel.IsRecommended = product.IsRecommended;
            productViewModel.IsSpecialOffer = product.IsSpecialOffer;
            productViewModel.Price = Convert.ToDecimal(product.Price) / 100;
            productViewModel.PromotionPrice = Convert.ToDecimal(product.PromotionPrice) / 100;
            productViewModel.ProductDetail = product.ProductDetail;
            productViewModel.ProductName = product.ProductName;
            productViewModel.ProductRemark = product.ProductRemark;
            productViewModel.SaleTitle = product.SaleTitle;
            productViewModel.SaleSubTitle = product.SaleSubTitle;
            productViewModel.ProductNumber = product.ProductNumber;
            var classes= product.ProductClassification.Select(item => ProductClassificationViewModel.CopyFrom(item)).ToList();
            var albumList = product.ProductAlbum.Select(item => ProductAlbumViewModel.CopyFrom(item)).ToList();
            productViewModel.Classifications = classes;
            productViewModel.Albums = albumList;
            return productViewModel;
        }

        public Data.Model.Product CopyTo()
        {
            Data.Model.Product product=new Data.Model.Product();
            product.Guid = Guid??System.Guid.Empty;
            product.CategoryGuid = CategoryGuid;
            product.DefaultPic = DefaultPic;
            product.ImaginaryNumber = ImaginaryNumber;
            product.IsRecommended = IsRecommended;
            product.IsSpecialOffer = IsSpecialOffer;
            product.Price = Convert.ToInt32(Price * 100);
            product.PromotionPrice = Convert.ToInt32(PromotionPrice * 100);
            product.ProductDetail = ProductDetail;
            product.ProductName = ProductName;
            product.ProductRemark = ProductRemark;
            product.SaleTitle = SaleTitle;
            product.SaleSubTitle = SaleSubTitle;
            product.ProductNumber = ProductNumber;
            return product;
        }
    }

    public  class ProductClassificationViewModel
    {
        public System.Guid? Guid { get; set; }
        [Required]
        public System.Guid ProductGuid { get; set; }
        [Required]
        public string Name { get; set; }
        public int Number { get; set; }
        /// <summary>
        /// 克数
        /// </summary>
        public int Grams { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public Decimal PromotionPrice { get; set; }
        [Required]
        public string PicUrl { get; set; }

        public static ProductClassificationViewModel CopyFrom(ProductClassification productClassification)
        {
            ProductClassificationViewModel viewModel=new ProductClassificationViewModel();
            viewModel.Guid = productClassification.Guid;
            viewModel.ProductGuid = productClassification.ProductGuid;
            viewModel.Name = productClassification.Name;
            viewModel.Number = productClassification.Number;
            viewModel.Price = Convert.ToDecimal(productClassification.Price) / 100;
            viewModel.PromotionPrice = Convert.ToDecimal(productClassification.PromotionPrice) / 100;
            viewModel.PicUrl = productClassification.PicUrl;
            viewModel.Grams = productClassification.Grams;
            return viewModel;
        }
        public ProductClassification  CopyTo()
        {
            ProductClassification classification=new ProductClassification();
            classification.Guid = Guid??System.Guid.Empty;
            classification.ProductGuid = ProductGuid;
            classification.Name = Name;
            classification.Number = Number;
            classification.Price = Convert.ToInt32(Price * 100);
            classification.PromotionPrice = Convert.ToInt32(PromotionPrice * 100);
            classification.PicUrl = PicUrl;
            classification.Grams = Grams;
            return classification;
        }

    }

    public  class ProductAlbumViewModel
    {
        public System.Guid? Guid { get; set; }
        [Required]
        public System.Guid ProductGuid { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; }

        public static ProductAlbumViewModel CopyFrom(ProductAlbum album)
        {
            ProductAlbumViewModel model=new ProductAlbumViewModel();
            model.Guid = album.Guid;
            model.ProductGuid = album.ProductGuid;
            model.ImageUrl = album.ImageUrl;
            model.DisplayOrder = album.DisplayOrder;
            return model;
        }

        public ProductAlbum CopyTo()
        {
            ProductAlbum album=new ProductAlbum();
            album.Guid = Guid??System.Guid.Empty;
            album.ProductGuid = ProductGuid;
            album.ImageUrl = ImageUrl;
            album.DisplayOrder = DisplayOrder;
            return album;
        }
    }
}