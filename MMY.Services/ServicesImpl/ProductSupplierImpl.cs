using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Caching;
using JK.Framework.Core.Data;
using JK.Framework.Data;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Services.ServicesImpl
{
    public class ProductSupplierImpl:IProductSupplier
    {
        private IRepository<ProductSupplier> ProductSupplierRepository;
        private ICacheManager CacheManager;
        private const string PRODUCTS_BY_ID_KEY = "MMY.productSupplier.uid-{0}";

        public ProductSupplierImpl(IRepository<ProductSupplier> useraccountRepository,  ICacheManager cacheManager)
        {
            ProductSupplierRepository = useraccountRepository;
            CacheManager = cacheManager;
        }

        public ProductSupplier Find(Guid uid)
        {
            return ProductSupplierRepository.Table.FirstOrDefault(q => q.Guid == uid && !q.IsDeleted);
        }

        public ProductSupplier FindWithCache(Guid uid)
        {
            string key = String.Format(PRODUCTS_BY_ID_KEY, uid);
            return CacheManager.Get(key, () => ProductSupplierRepository.Table.FirstOrDefault(q => q.Guid == uid && !q.IsDeleted));
        }

        public IList<ProductSupplier> Query(string supplierName, string supplierAddress,int skip,int take,out int total)
        {
            var queryable = ProductSupplierRepository.Table.Where(q=>!q.IsDeleted);
            if (!string.IsNullOrEmpty(supplierName))
            {
                queryable = queryable.Where(q => q.SupplierName.Contains(supplierName));
            }
            if (!string.IsNullOrEmpty(supplierAddress))
            {
                queryable = queryable.Where(q => q.SupplierAddress.Contains(supplierAddress));
            }
            total = queryable.Count();
            return queryable.OrderByDescending(q=>q.TimeCreated).Skip(skip).Take(take).ToList();
        }
    }
}
