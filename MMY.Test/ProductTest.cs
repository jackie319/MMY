using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServicesImpl;

namespace MMY.Test
{
    [TestClass]
    public class ProductTest
    {
        private IProduct _product;
        [TestInitialize]
        public void Initial(IProduct product)
        {
            _product=product;
        }
        [TestMethod]
        public void TestAddProduct()
        {
            Product product=new Product();
            product.CategoryGuid = Guid.Parse("9A235806-6507-4783-9E46-6C7594D52841");
            product.DefaultPic = "";
            product.DisplayOrder = 1;
            product.ImaginaryNumber = 100;
            product.IsRecommended = true;
            product.IsSpecialOffer = true;
            product.Price = 200;
            product.PromotionPrice = 100;
            product.ProductDetail = "产品详情";
            product.ProductName = "产品名称";
            product.ProductNumber = 100;
            product.ProductRemark = "单元测试";
            product.SaleSubTitle = "商品副标题";
            product.SaleTitle = "商品标题";
            product.SoldTotal = 100;
            product.Status = "Default";
            product.TimeOnShelf=DateTime.Now;
            product.TimeOffShelf=DateTime.Now;
            IList<ProductClassification> classifications=new List<ProductClassification>();
            ProductClassification classification=new ProductClassification();
            classification.AlbumGuid = Guid.Empty;//TODO
            classification.Name = "55克";
            classification.Number = 12;
            classification.Price = 190;
            classification.PromotionPrice = 90;
            classifications.Add(classification);
            IList<ProductAlbum> albums=new List<ProductAlbum>();

            ProductAlbum album=new ProductAlbum();
            album.ImageUrl = "/upload/123.jpg";
            albums.Add(album);
            _product.CreatedProduct(product, classifications, albums);
        }
    }
}
