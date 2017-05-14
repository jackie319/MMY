using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Core;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using MMY.PlatForm.WebUI.Models.Product;
using MMY.Services.IServices;

namespace MMY.PlatForm.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult List(ProductQueryModel query)
        {
            int total;
            var list = _product.GetAdminProductVs(query.ProductName, query.CategoryGuid, query.Status, query.IsSpecialOffer,
                query.IsRecommended, query.TimeCreatedBegin, query.TimeCreatedEnd, query.Skip, query.Take, out total);
            var resultList = list.Select(q => ProductListViewModel.CopyFrom(q)).ToList();
            return this.ResultListModel(total, resultList);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Detail(Guid productGuid)
        {
            var product = _product.FindProduct(productGuid);
            var resultProduct = ProductViewModel.CopyFrom(product);
            return this.ResultModel(resultProduct);
        }

        public ActionResult GetClassification(Guid productGuid)
        {
            var list = _product.GetClassifications(productGuid);
            var resultList = list.Select(item => ProductClassificationListViewModel.CopyFrom(item)).ToList();
            return this.ResultListModel(resultList.Count, resultList);
        }

        //[ValidationFilter]
        //[HttpPost]
        //public ActionResult Add(ProductViewModel model)
        //{
        //    var albums=model.Albums.Select(item => item.CopyTo()).ToList();
        //    var classifications = model.Classifications.Select(item => item.CopyTo()).ToList();
        //    _product.CreatedProduct(model.CopyTo(),classifications,albums);
        //    return this.ResultSuccess();
        //}
        //[ValidationFilter]
        //[HttpPost]
        //public ActionResult Update(ProductViewModel model)
        //{
        //    if ( model.Guid == Guid.Empty) return this.ResultError("产品Guid不能为空");
        //    var albums = model.Albums.Select(item => item.CopyTo()).ToList();
        //    var classifications = model.Classifications.Select(item => item.CopyTo()).ToList();
        //    _product.UpdateProduct(model.CopyTo(), classifications, albums);
        //    return this.ResultSuccess();
        //}

        /// <summary>
        /// 第一个颜色分类的价格赋值给产品价格
        /// 相册第一张赋值给产品默认图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidationFilter]
        [HttpPost]
        public ActionResult Save(ProductViewModel model)
        {
            var albums = model.Albums.Select(item => item.CopyTo()).ToList();
            var classifications = model.Classifications.Select(item => item.CopyTo()).ToList();
            //产品默认价格
            var product = model.CopyTo();
            product.Price = classifications[0].Price;
            product.PromotionPrice = classifications[0].PromotionPrice;
            if (model.Guid == Guid.Empty)
            {

                _product.CreatedProduct(product, classifications, albums);
            }
            else
            {
                _product.UpdateProduct(product, classifications, albums);
            }
            return this.ResultSuccess();
        }



        [HttpPost]
        public ActionResult Delete(Guid productGuid)
        {
            _product.Delete(productGuid);
            return this.ResultSuccess();
        }

        [HttpPost]
        public ActionResult OnShelf(Guid productGuid)
        {
            try
            {
                _product.OnShelf(productGuid);
            }
            catch (CommonException ce)
            {
                return this.ResultError(ce.Message);
            }
           
            return this.ResultSuccess();
        }

        [HttpPost]
        public ActionResult OffShelf(Guid productGuid)
        {
            _product.OffShelf(productGuid);
            return this.ResultSuccess();
        }
    }
}