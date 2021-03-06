﻿using System;
using MMY.Data.Model;

namespace MMY.FrontSite.WebApi.Models.UserAccount
{
    public class UserFavoriteViewModel
    {

        public Guid Guid { get; set; }

        public Guid ProductGuid { get; set; }
        public string DefaultPic { get; set; }
        public string SaleTitle { get; set; }

        public string SaleSubTitle { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }

        public static UserFavoriteViewModel CopyFrom(UserFavorite favorite)
        {
            UserFavoriteViewModel model=new UserFavoriteViewModel();
            model.Guid = favorite.Guid;
            model.ProductGuid = favorite.ProductGuid;
            model.DefaultPic = favorite.Product.DefaultPic;
            model.SaleTitle = favorite.Product.SaleTitle;
            model.SaleSubTitle = favorite.Product.SaleSubTitle;
            model.Price = Convert.ToDecimal(favorite.Product.Price) / 100;
            model.PromotionPrice = Convert.ToDecimal(favorite.Product.PromotionPrice) / 100;
            return model;
        }
    }
}