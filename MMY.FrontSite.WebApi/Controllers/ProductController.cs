using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using JK.Framework.Web.Model;
using MMY.FrontSite.WebApi.Models.Product;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.FrontSite.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }
        /// <summary>
        /// 推荐
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IList<ProductListViewModel> Recommended()
        {
            var query = new QueryBase();
            int total;
            var list = _product.GetProductVs(null, null, ProductStatusEnum.OffShelf, false, true, null, null, query.Skip, query.Take, out total);
            var reusltList = list.Select(item => ProductListViewModel.CopyFrom(item)).ToList();
            return reusltList;
        }
    }
}
