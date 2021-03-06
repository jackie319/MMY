﻿using MMY.Data.Model;

namespace MMY.FrontSite.WebApi.Models.Product
{
    public class CategoryListViewModel
    {
        /// <summary>
        /// 分类Guid
        /// </summary>
        public System.Guid Guid { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
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