using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var list=_product.GetProductVs(query.ProductName, query.CategoryGuid, query.Status, query.IsSpecialOffer,
                query.IsRecommended, query.TimeCreatedBegin, query.TimeCreatedEnd, query.Skip, query.Take, out total);
            var resultList = list.Select(q => ProductListViewModel.CopyFrom(q)).ToList();
            return this.ResultListModel(total,resultList);
        }

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

        [ValidationFilter]
        [HttpPost]
        public ActionResult Save(ProductViewModel model)
        {
            var albums = model.Albums.Select(item => item.CopyTo()).ToList();
            var classifications = model.Classifications.Select(item => item.CopyTo()).ToList();
            if (model.Guid == Guid.Empty)
            {
              
                _product.CreatedProduct(model.CopyTo(), classifications, albums);
            }
            else
            {
                _product.UpdateProduct(model.CopyTo(), classifications, albums);
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
            _product.OnShelf(productGuid);
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