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
      //  private IDbContext DbContext;
        private ICacheManager CacheManager;
        private const string PRODUCTS_BY_ID_KEY = "MMY.product.uid-{0}";
        //public ProductSupplierImpl(IRepository<ProductSupplier> useraccountRepository, IDbContextGetter dbContext, ICacheManager cacheManager)
        //{
        //    ProductSupplierRepository = useraccountRepository;
        //    DbContext = dbContext.GetByName<IDbContext>("MMYEntities");
        //    CacheManager = cacheManager;
        //}

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

        public IList<ProductSupplier> Query()
        {
            return ProductSupplierRepository.Table.Where(q=>!q.IsDeleted).ToList();
        }
    }
}
