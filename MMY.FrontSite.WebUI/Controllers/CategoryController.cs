using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Model;
using MMY.FrontSite.WebUI.Models.Product;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private IProductCategory _productCategory;
        public CategoryController(IProductCategory productCategory)
        {
            _productCategory = productCategory;
        }

        public ActionResult List()
        {
           var categories= _productCategory.GetAllParentCategory();
            var resultList = categories.Select(item => CategoryListViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(categories.Count, resultList);
        }
    }
}