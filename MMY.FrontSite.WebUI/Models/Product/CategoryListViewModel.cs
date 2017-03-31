using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.FrontSite.WebUI.Models.Product
{
    public class CategoryListViewModel
    {
        public System.Guid Guid { get; set; }
        public string CategoryName { get; set; }

        public static CategoryListViewModel CopyFrom(ProductCategory category)
        {
            CategoryListViewModel model=new CategoryListViewModel();
            model.Guid = category.Guid;
            model.CategoryName = category.CategoryName;
            return model;
        }
    }
}