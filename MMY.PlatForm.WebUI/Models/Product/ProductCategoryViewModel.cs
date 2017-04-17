using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class ProductCategoryListViewModel
    {
        public System.Guid Guid { get; set; }
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime TimeCreated { get; set; }

      

        public static ProductCategoryListViewModel CopyFrom(ProductCategory category)
        {
            ProductCategoryListViewModel model =new ProductCategoryListViewModel();
            model.Guid = category.Guid;
            model.CategoryName = category.CategoryName;
            model.DisplayOrder = category.DisplayOrder;
            model.TimeCreated = category.TimeCreated;
            return model;
        }
    }

    public class ProductCategoryViewModel
    {
        public System.Guid Guid { get; set; }
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }

        public ProductCategory CopyTo()
        {
            ProductCategory category = new ProductCategory();
            category.Guid = Guid;
            category.CategoryName = CategoryName;
            category.DisplayOrder = DisplayOrder;
            return category;
        }

        public static ProductCategoryViewModel CopyFrom(ProductCategory category)
        {
            ProductCategoryViewModel model = new ProductCategoryViewModel();
            model.Guid = category.Guid;
            model.CategoryName = category.CategoryName;
            model.DisplayOrder = category.DisplayOrder;
            return model;
        }
    }
}