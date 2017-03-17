using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ProductImpl(IRepository<Product> productRepository, IRepository<ProductV> productVRepository)
        {
            _productRepository = productRepository;
            _productVRepository = productVRepository;
        }

        public IList<ProductV> GetProductVs(string productName,Guid? categoryGuid, ProductStatusEnum status,bool? isSpecialOffer,bool? isRecommended,
            DateTime? timeCreatedBegin,DateTime? timeCreatedEnd,int skip,int take,out int total)
        {
            var query = _productVRepository.Table;
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(q => q.ProductName.Contains(productName)|| q.SaleTitle.Contains(productName));
            }
            if (categoryGuid != null)
            {
                query = query.Where(q => q.CategoryGuid == categoryGuid);
            }
            if (status != ProductStatusEnum.All)
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
            if (timeCreatedBegin != null)
            {
                query = query.Where(q => q.TimeCreated >= timeCreatedBegin);
            }
            if (timeCreatedEnd != null)
            {
                query = query.Where(q => q.TimeCreated < timeCreatedEnd);
            }
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public void CreatedProduct(Product product)
        {
            product.TimeCreated=DateTime.Now;
            product.TimeOnShelf = product.TimeCreated;
            product.TimeOffShelf = product.TimeCreated;
            product.IsDeleted = false;
            _productRepository.Insert(product);
        }
    }
}
