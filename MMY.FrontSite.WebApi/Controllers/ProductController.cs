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
    /// <summary>
    /// 商品详情
    /// </summary>
    public class ProductController : ApiController
    {
        private IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }
        ///// <summary>
        ///// 首页搜索
        ///// </summary>
        ///// <param name="searchName"></param>
        ///// <returns></returns>
        //[System.Web.Http.HttpGet]
        //public IList<ProductListViewModel> Search(string searchName)
        //{
        //    var query = new QueryBase();
        //    int total;
        //    var list = _product.GetProductVs(searchName, null, ProductStatusEnum.OffShelf, false, false, null, null, query.Skip, query.Take, out total);
        //    var resultList = list.Select(item => ProductListViewModel.CopyFrom(item)).ToList();
        //    return resultList;
        //}


        ///// <summary>
        ///// 推荐
        ///// </summary>
        ///// <returns></returns>
        //[System.Web.Http.HttpGet]
        //public IList<ProductListViewModel> Recommended()
        //{
        //    var query = new QueryBase();
        //    int total;
        //    var list = _product.GetProductVs(null, null, ProductStatusEnum.OffShelf, false, true, null, null, query.Skip, query.Take, out total);
        //    var resultList = list.Select(item => ProductListViewModel.CopyFrom(item)).ToList();
        //    return resultList;
        //}

        ///// <summary>
        ///// 特卖
        ///// </summary>
        ///// <returns></returns>
        //[System.Web.Http.HttpGet]
        //public IList<ProductListViewModel> SpecialOffer()
        //{
        //    var query = new QueryBase();
        //    int total;
        //    var list = _product.GetProductVs(null, null, ProductStatusEnum.OffShelf, true, false, null, null, query.Skip, query.Take, out total);
        //    var resultList = list.Select(item => ProductListViewModel.CopyFrom(item)).ToList();
        //    return resultList;
        //}

        ///// <summary>
        ///// 热销榜
        ///// </summary>
        ///// <returns></returns>
        //[System.Web.Http.HttpGet]
        //public IList<ProductListViewModel> Hot()
        //{
        //    var query = new QueryBase();
        //    int total;
        //    var list = _product.HotList(query.Skip, query.Take, out total);
        //    var resultList = list.Select(item => ProductListViewModel.CopyFrom(item)).ToList();
        //    return resultList;
        //}

        ///// <summary>
        ///// 最新
        ///// </summary>
        ///// <returns></returns>
        //[System.Web.Http.HttpGet]
        //public IList<ProductListViewModel> New()
        //{
        //    var query = new QueryBase();
        //    int total;
        //    var list = _product.NewList(query.Skip, query.Take, out total);
        //    var resultList = list.Select(item => ProductListViewModel.CopyFrom(item)).ToList();
        //    return resultList;
        //}

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="productGuid"></param>
        /// <returns></returns>
        [System.Web.Http.Route("~/api/account")]
        [System.Web.Http.HttpGet]
        public ProductViewModel Detail(Guid productGuid)
        {
            var product = _product.FindProduct(productGuid);
            var result = ProductViewModel.CopyFrom(product);
            return result;
        }
    }
}
