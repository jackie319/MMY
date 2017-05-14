using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.Services.ServicesImpl
{
    public class ProductImpl:IProduct
    {
        private IRepository<Product> _productRepository;

        private IRepository<ProductV> _productVRepository;

        private IRepository<ProductPurchaseRecords> _productPurchaseRepository;

        private IRepository<ProductClassification> _productClassificationrRepository;
        private IRepository<ProductAlbum> _productAlbumRepository;

        private IRepository<ProductSupplier> _productSupplieRepository;
        public ProductImpl(IRepository<Product> productRepository, IRepository<ProductV> productVRepository, IRepository<ProductPurchaseRecords> purchaseRepository,
            IRepository<ProductClassification> productClassificationRepository, IRepository<ProductAlbum> productAlbumRepository,IRepository<ProductSupplier> productSupplieRepository)
        {
            _productRepository = productRepository;
            _productVRepository = productVRepository;
            _productPurchaseRepository = purchaseRepository;
            _productClassificationrRepository = productClassificationRepository;
            _productAlbumRepository = productAlbumRepository;
            _productSupplieRepository = productSupplieRepository;
        }

        public IList<ProductV> GetProductVs(string productName,Guid? categoryGuid, ProductStatusEnum? status,bool? isSpecialOffer,bool? isRecommended,
            DateTime? timeCreatedBegin,DateTime? timeCreatedEnd,int skip,int take,out int total)
        {
            var query = _productVRepository.Table.Where(q=>!q.IsDeleted);
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(q => q.ProductName.Contains(productName)|| q.SaleTitle.Contains(productName)||q.SaleSubTitle.Contains(productName));
            }
            if (categoryGuid != null && categoryGuid!=Guid.Empty)
            {
                query = query.Where(q => q.CategoryGuid == categoryGuid);
            }
            if (status !=null)
            {
                string statusStr = status.ToString();
                query = query.Where(q => q.Status.Equals(statusStr));
            }
            if (isSpecialOffer != null)
            {
                query = query.Where(q => q.IsSpecialOffer == isSpecialOffer);
            }
            if (isRecommended != null)
            {
                query = query.Where(q => q.IsRecommended == isRecommended);
            }
            if (timeCreatedBegin != null && timeCreatedBegin!=DateTime.MinValue)
            {
                query = query.Where(q => q.TimeCreated >= timeCreatedBegin);
            }
            if (timeCreatedEnd != null && timeCreatedEnd!=DateTime.MinValue)
            {
                query = query.Where(q => q.TimeCreated < timeCreatedEnd);
            }
            total = query.Count();
            return query.OrderByDescending(q=>q.DisplayOrder)
                .ThenBy(q=>q.IsRecommended)
                .ThenBy(q=>q.IsSpecialOffer).
                ThenByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public IList<ProductV> GetAdminProductVs(string productName, Guid? categoryGuid, ProductStatusEnum? status, bool? isSpecialOffer,
            bool? isRecommended, DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total)
        {
            var query = _productVRepository.Table.Where(q => !q.IsDeleted);
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(q => q.ProductName.Contains(productName) || q.SaleTitle.Contains(productName) || q.SaleSubTitle.Contains(productName));
            }
            if (categoryGuid != null && categoryGuid != Guid.Empty)
            {
                query = query.Where(q => q.CategoryGuid == categoryGuid);
            }
            if (status != null)
            {
                string statusStr = status.ToString();
                query = query.Where(q => q.Status.Equals(statusStr));
            }
            if (isSpecialOffer != null)
            {
                query = query.Where(q => q.IsSpecialOffer == isSpecialOffer);
            }
            if (isRecommended != null)
            {
                query = query.Where(q => q.IsRecommended == isRecommended);
            }
            if (timeCreatedBegin != null && timeCreatedBegin != DateTime.MinValue)
            {
                query = query.Where(q => q.TimeCreated >= timeCreatedBegin);
            }
            if (timeCreatedEnd != null && timeCreatedEnd != DateTime.MinValue)
            {
                query = query.Where(q => q.TimeCreated < timeCreatedEnd);
            }
            total = query.Count();
            return query.OrderByDescending (q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public IList<ProductV> HotList(int skip, int take, out int total)
        {
            var query = _productVRepository.Table.Where(q=>q.SoldTotal>0);
            total = query.Count();
            return query.OrderByDescending(q => q.SoldTotal).Skip(0).Take(take).ToList();
        }

        public IList<ProductV> NewList(int skip,int take,out int total)
        {
            var query = _productVRepository.Table;
             total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public Product FindProduct(Guid productGuid)
        {
            return _productRepository.Table.FirstOrDefault(q => q.Guid == productGuid && !q.IsDeleted);
        }


        public IList<ProductClassification> GetClassifications(Guid productGuid)
        {
            return _productClassificationrRepository.Table.Where(q => q.ProductGuid == productGuid && !q.IsDeleted).ToList();
        }

        public IList<ProductAlbum> GetAlbums(Guid productGuid)
        {
            return _productAlbumRepository.Table.Where(q => q.ProductGuid == productGuid && !q.IsDeleted).OrderByDescending(q=>q.TimeCreated).ToList();
        }
        public void CreatedProduct(Product product,IList<ProductClassification> classifications,IList<ProductAlbum> albums)
        {
            Guid uid = Guid.NewGuid();
            product.Guid = uid;
            product.TimeCreated=DateTime.Now;
            product.TimeOnShelf = product.TimeCreated;
            product.TimeOffShelf = product.TimeCreated;
            product.ProductNumber = 0; //产品数量不能编辑，程序自动计算
            product.SoldTotal = 0;
            product.VisitedTotal = 0;
            product.Status = ProductStatusEnum.Default.ToString();
            product.IsDeleted = false;
            _productRepository.Insert(product);

            foreach (var item in classifications)
            {
                item.ProductGuid = uid;
                CreateProductClassification(item);
            }

            foreach (var item in albums)
            {
                item.ProductGuid = uid;
                CreateProductAlbum(item);
            }
          
        }

        public void UpdateProduct(Product product, IList<ProductClassification> classifications, IList<ProductAlbum> albums)
        {
            var entity = _productRepository.Table.FirstOrDefault(q => q.Guid == product.Guid && !q.IsDeleted);

            if (entity == null) return;
            if (entity.Status == ProductStatusEnum.OnShelf.ToString())
            {
                throw new CommonException("编辑产品前请先下架该产品");
            }
            entity.CategoryGuid = product.CategoryGuid;
            entity.DefaultPic = product.DefaultPic;
            entity.DisplayOrder = product.DisplayOrder;
            entity.IsRecommended = product.IsRecommended;
            entity.IsSpecialOffer = product.IsSpecialOffer;
            entity.Price = product.Price;
            entity.ProductDetail = product.ProductDetail;
            entity.ProductName = product.ProductName;
            entity.PromotionPrice = product.PromotionPrice;
            entity.ProductRemark = product.ProductRemark;
            entity.SaleSubTitle = product.SaleSubTitle;
            entity.SaleTitle = product.SaleTitle;
            entity.ImaginaryNumber = product.ImaginaryNumber;
            _productRepository.Update(entity);

            DeleteProductClassification(product.Guid);
            foreach (var item in classifications)
            {
                if (item.Guid == Guid.Empty)
                {
                    item.ProductGuid = entity.Guid;
                    CreateProductClassification(item);
                }
                else
                {
                    UpdateProductClassification(item);
                }
              
            }

            DeleteProductAlbum(product.Guid);
            foreach (var item in albums)
            {
                item.ProductGuid = entity.Guid;
                CreateProductAlbum(item);
            }
        }
        /// <summary>
        /// 上下架不在详情里，必须单独修改
        /// </summary>
        /// <param name="productGuid"></param>
        public void OnShelf(Guid productGuid)
        {
            var entity = _productRepository.Table.FirstOrDefault(q => q.Guid == productGuid && !q.IsDeleted);
            if (entity == null) throw new CommonException("找不到改产品");
            if (entity.ProductNumber==0)throw new CommonException("库存不足，不能上架，请先添加库存");
 
            entity.Status = ProductStatusEnum.OnShelf.ToString();
            entity.TimeOnShelf=DateTime.Now;
            _productRepository.Update(entity);
        }

        public void OffShelf(Guid productGuid)
        {
            var entity = _productRepository.Table.FirstOrDefault(q => q.Guid == productGuid && !q.IsDeleted);
            if (entity == null) return;
            entity.Status = ProductStatusEnum.OffShelf.ToString();
            entity.TimeOffShelf=DateTime.Now;
            _productRepository.Update(entity);
        }

        public void Delete(Guid productGuid)
        {
            var entity = _productRepository.Table.FirstOrDefault(q => q.Guid == productGuid && !q.IsDeleted);
            if (entity == null) return;
            if (entity.Status == ProductStatusEnum.OnShelf.ToString())
            {
                throw new CommonException("删除产品前请先下架该产品");
            }
            entity.IsDeleted = true;
            _productRepository.Update(entity);
        }
        /// <summary>
        /// 进货记录 。某个产品的进货记录，某个供货商的进货记录，系统所有进货记录
        /// </summary>
        /// <param name="productGuid"></param>
        /// <param name="supplierGuid"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<ProductPurchaseRecords> GetProductPurchaseRecords(Guid? productGuid,Guid? supplierGuid,int skip,int take,out int total)
        {
            var query = _productPurchaseRepository.Table;
            if (productGuid != null && productGuid!=Guid.Empty)
            {
                query = query.Where(q => q.ProductGuid == productGuid);
            }
            if (supplierGuid != null && supplierGuid != Guid.Empty)
            {
                query = query.Where(q => q.SupplierGuid == supplierGuid);
            }
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }
        /// <summary>
        /// 新增进货记录
        /// </summary>
        /// <param name="records"></param>
        public void AddPurchaseRecords(ProductPurchaseRecords records)
        {
            var product = FindProduct(records.ProductGuid);
            records.Guid = Guid.NewGuid();
            records.TimeCreated=DateTime.Now;
            records.IsDeleted = false;
            records.ProductName = product.ProductName;
            var supplier = _productSupplieRepository.Table.FirstOrDefault(q => q.Guid == records.SupplierGuid);
            if (supplier != null)
            {
                records.SupplierName = supplier.SupplierName;
            }
            var classification = product.ProductClassification.FirstOrDefault(q => q.Guid == records.ClassificationGuid);
            records.ClassificationName = classification.Name;
            _productPurchaseRepository.Insert(records);
           
            //TODO:更新产品分类数量
          
            if (classification != null)
            {
                classification.Number += records.Number;

            }
            product.ProductNumber += records.Number;
            _productRepository.Update(product);//TODO:事务
        }

        private void CreateProductClassification(ProductClassification classification)
        {
            classification.TimeCreated=DateTime.Now;
            classification.Guid = Guid.NewGuid();
            classification.IsDeleted = false;
            _productClassificationrRepository.Insert(classification);
        }


        private void UpdateProductClassification(ProductClassification classification)
        {
            var entity = _productClassificationrRepository.Table.FirstOrDefault(q => q.Guid == classification.Guid);
            if (entity == null) return;
            entity.Name = classification.Name;
            entity.PicUrl = classification.PicUrl;
            entity.Grams = classification.Grams;
            entity.Price = classification.Price;
            entity.PromotionPrice = classification.PromotionPrice;
            entity.IsDeleted = false;
            var product = _productRepository.Table.FirstOrDefault(q => q.Guid == classification.ProductGuid);
            _productClassificationrRepository.Update(entity);

            if (product != null)
            {
                product.ProductNumber += entity.Number;
            }
        }

        private void DeleteProductClassification(Guid productGuid)
        {
            var entities=_productClassificationrRepository.Table.Where(q => q.ProductGuid == productGuid && !q.IsDeleted).ToList();
            var product = _productRepository.Table.FirstOrDefault(q => q.Guid == productGuid);
            foreach (var item in entities)
            {
                item.IsDeleted = true;
                _productClassificationrRepository.Update(item);

                if (product != null)
                {
                    product.ProductNumber -= item.Number;
                }
           

            }
        }

        private void CreateProductAlbum(ProductAlbum album)
        {
            album.Guid = Guid.NewGuid();
            album.TimeCreated=DateTime.Now;
            album.IsDeleted = false;
            _productAlbumRepository.Insert(album);
        }

        private void DeleteProductAlbum(Guid productGuid)
        {
            var entities = _productAlbumRepository.Table.Where(q => q.ProductGuid == productGuid).ToList();
            foreach (var item in entities)
            {
                item.IsDeleted = true;
                _productAlbumRepository.Update(item);
            }
        }

        public IList<Product> FindProducts(IList<Guid> productGuids)
        {
            return _productRepository.Table.Where(q => productGuids.Contains(q.Guid)).ToList();
        }
    }
}
