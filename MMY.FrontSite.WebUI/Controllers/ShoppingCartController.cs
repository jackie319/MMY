﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.FrontSite.Domain;
using MMY.FrontSite.WebUI.Models.ShoppingCart;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCart _shoppingCart;
        public ShoppingCartController(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public ActionResult Cart()
        {
            return View();
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult Add(AddShoppingCartViewModel cart)
        {
            var mmyUser = (UserModel)HttpContext.User;
            _shoppingCart.Add(mmyUser.UserGuid, cart.ProductGuid,cart.ClassificationGuid,cart.ProductNum);
            return this.ResultSuccess();
        }
        /// <summary>
        /// 我的购物车
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult List()
        {
            var query = new JK.Framework.Core.QueryBase();
            var mmyUser = (UserModel)HttpContext.User;
            int total;
            var list = _shoppingCart.GetList(mmyUser.UserGuid, query.Skip, query.Take, out total);
            var resultList = list.Select(item => ShoppingCartViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(total, resultList);
        }

        /// <summary>
        /// 更改数量
        /// </summary>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult UpdateNum(Guid shopppingCartGuid,int num)
        {
            _shoppingCart.UpdateShoppingCartNum(shopppingCartGuid,num);
            return this.ResultSuccess();
        }

        /// <summary>
        /// 更改颜色分类
        /// </summary>
        /// <param name="shoppingCartGuid"></param>
        /// <param name="classificationGuid"></param>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult UpdateClassification(Guid shoppingCartGuid,Guid classificationGuid)
        {
            _shoppingCart.UpdateClassification(shoppingCartGuid,classificationGuid);
            return this.ResultSuccess();
        }
        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="shoppingCartGuid"></param>
        /// <returns></returns>
        [JKAuthorize]
        public ActionResult Delete(Guid shoppingCartGuid)
        {
            _shoppingCart.Delete(shoppingCartGuid);
            return this.ResultSuccess();
        }
    }
}