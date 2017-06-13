using JK.Framework.API.Model;
using JK.Framework.Core;
using JK.Framework.Web.Model;
using MMY.FrontSite.WebApi.Models.Product;
using MMY.FrontSite.WebApi.Models.Query;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MMY.FrontSite.WebApi.Controllers
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class CategorysController : ApiController
    {
        private IProductCategory _productCategory;
        private IProduct _product;
        public CategorysController(IProductCategory productCategory,IProduct product)
        {
            _productCategory = productCategory;
            _product = product;
        }

        /// <summary>
        /// 所有商品分类
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("~/api/categorys")]
        [System.Web.Http.HttpGet]
        public IList<CategoryListViewModel> List()
        {
            var categories = _productCategory.GetAllParentCategory();
            var resultList = categories.Select(item => CategoryListViewModel.CopyFrom(item)).ToList();
            BaseApiController.AppendHeader(resultList.Count);
            return resultList;
        }

        /// <summary>
        /// 某分类下的商品
        /// </summary>
        /// <param name="categoryGuid"></param>
        /// <returns></returns>
         [System.Web.Http.Route("~/api/categorys/{categoryGuid}/products")]
        [System.Web.Http.HttpGet]
        public IList<ProductListViewModel> ListOfCategory(Guid categoryGuid, [FromUri] QueryBase query)
        {
            //var query = new QueryBase();
            int total;
            var list = _product.GetProductVs("", categoryGuid, ProductStatusEnum.OffShelf, false, false, null, null, query.Skip, query.Take, out total);
            var resultList = list.Select(item => ProductListViewModel.CopyFrom(item)).ToList();
            BaseApiController.AppendHeader(total);
            return resultList;
        }
    }
}
