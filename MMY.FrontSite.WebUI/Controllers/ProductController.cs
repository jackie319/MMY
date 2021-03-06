﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Model;
using MMY.FrontSite.WebUI.Models.Product;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }
        // GET: Product
        public ActionResult ProductClassify()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult ProductDetail()
        {
            return View();
        }

        /// <summary>
        /// 某分类下的商品
        /// </summary>
        /// <param name="categoryGuid"></param>
        /// <returns></returns>
        public ActionResult ListOfCategory(Guid categoryGuid)
        {
            var query=new JK.Framework.Core.QueryBase();
            int total;
            var list=_product.GetProductVs("", categoryGuid, ProductStatusEnum.OffShelf, false, false, null, null, query.Skip, query.Take, out total);
            var resultList = list.Select(item => ProductListViewModel.CopyFrom(item));
            return this.ResultListModel(total, resultList);
        }
        /// <summary>
        /// 首页搜索
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public ActionResult Search(string searchName)
        {
            var query = new JK.Framework.Core.QueryBase();
            int total;
            var list = _product.GetProductVs(searchName, null, ProductStatusEnum.OffShelf, false, false, null, null, query.Skip, query.Take, out total);
            var resultList = list.Select(item => ProductListViewModel.CopyFrom(item));
            return this.ResultListModel(total, resultList);
        }
        /// <summary>
        /// 推荐
        /// </summary>
        /// <returns></returns>
        public ActionResult Recommended()
        {
            var query = new JK.Framework.Core.QueryBase();
            int total;
            var list = _product.GetProductVs(null,null, ProductStatusEnum.OffShelf, false,true, null, null, query.Skip, query.Take, out total);
            var resultList = list.Select(item => ProductListViewModel.CopyFrom(item));
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 特卖
        /// </summary>
        /// <returns></returns>
        public ActionResult SpecialOffer()
        {
            var query = new JK.Framework.Core.QueryBase();
            int total;
            var list = _product.GetProductVs(null, null, ProductStatusEnum.OffShelf, true, false, null, null, query.Skip, query.Take, out total);
            var resultList = list.Select(item => ProductListViewModel.CopyFrom(item));
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 热销榜
        /// </summary>
        /// <returns></returns>
        public ActionResult Hot()
        {
            var query = new JK.Framework.Core.QueryBase();
            int total;
            var list=_product.HotList(query.Skip,query.Take,out total);
            var resultList = list.Select(item => ProductListViewModel.CopyFrom(item));
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 最新
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var query = new JK.Framework.Core.QueryBase();
            int total;
            var list = _product.NewList(query.Skip, query.Take, out total);
            var resultList = list.Select(item => ProductListViewModel.CopyFrom(item));
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="productGuid"></param>
        /// <returns></returns>
        public ActionResult Detail(Guid productGuid)
        {
            var product = _product.FindProduct(productGuid);
            var result = ProductViewModel.CopyFrom(product);
            return this.ResultModel(result);
        }



    }
}