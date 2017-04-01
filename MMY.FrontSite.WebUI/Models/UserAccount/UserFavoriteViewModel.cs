using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.FrontSite.WebUI.Models.UserAccount
{
    public class UserFavoriteViewModel
    {

        public Guid Guid { get; set; }
        public string DefaultPic { get; set; }
        public string SaleTitle { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }

        public static UserFavoriteViewModel CopyFrom(UserFavorite favorite)
        {
            UserFavoriteViewModel model=new UserFavoriteViewModel();
            model.Guid = favorite.Guid;
            model.DefaultPic = favorite.Product.DefaultPic;
            model.SaleTitle = favorite.Product.SaleTitle;
            model.Price = Convert.ToDecimal(favorite.Product.Price) / 100;
            model.PromotionPrice = Convert.ToDecimal(favorite.Product.PromotionPrice) / 100;
            return model;
        }
    }
}