using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Model;
using MMY.PlatForm.WebUI.Models.Product;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private IProductCategory _productCategory;

        public CategoryController(IProductCategory productCategory)
        {
            _productCategory = productCategory;
        }
        /// <summary>
        /// 所有一级分类
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var category=_productCategory.GetAllParentCategory();
            var model = category.Select(item=> ProductCategoryListViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(model.Count,model);
        }

        public ActionResult Save(ProductCategoryViewModel model)
        {
            if (model.Guid == Guid.Empty)
            {
                var category = model.CopyTo();
                category.ParentGuid = Guid.Empty;
                _productCategory.AddCategory(category);
            }
            else
            {
                _productCategory.UpdateCategory(model.CopyTo());
            }
            return this.ResultSuccess();
        }

        public ActionResult Detail(Guid categoryGuid)
        {
            var model = _productCategory.FindCategory(categoryGuid);
            var result = ProductCategoryViewModel.CopyFrom(model);
            return this.ResultModel(result);
        }

        public ActionResult Delete(Guid categoryGuid)
        {
            _productCategory.DeleteCategory(categoryGuid);
            return this.ResultSuccess();
        }
    }
}