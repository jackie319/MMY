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
    public class ProductSupplierImpl : IProductSupplier
    {
        private IRepository<ProductSupplier> _ProductSupplierRepository;
        private ICacheManager _CacheManager;
        private const string PRODUCTS_BY_ID_KEY = "MMY.productSupplier.uid-{0}";

        public ProductSupplierImpl(IRepository<ProductSupplier> useraccountRepository, ICacheManager cacheManager)
        {
            _ProductSupplierRepository = useraccountRepository;
            _CacheManager = cacheManager;
        }

        public ProductSupplier Find(Guid uid)
        {
            return _ProductSupplierRepository.Table.FirstOrDefault(q => q.Guid == uid && !q.IsDeleted);
        }

        public ProductSupplier FindWithCache(Guid uid)
        {
            string key = String.Format(PRODUCTS_BY_ID_KEY, uid);
            return _CacheManager.Get(key, () => _ProductSupplierRepository.Table.FirstOrDefault(q => q.Guid == uid && !q.IsDeleted));
        }

        public IList<ProductSupplier> Query(string supplierName, string supplierAddress, int skip, int take, out int total)
        {
            var queryable = _ProductSupplierRepository.Table.Where(q => !q.IsDeleted);
            if (!string.IsNullOrEmpty(supplierName))
            {
                queryable = queryable.Where(q => q.SupplierName.Contains(supplierName));
            }
            if (!string.IsNullOrEmpty(supplierAddress))
            {
                queryable = queryable.Where(q => q.SupplierAddress.Contains(supplierAddress));
            }
            total = queryable.Count();
            return queryable.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public void Add(ProductSupplier supplier)
        {
            supplier.Guid = Guid.NewGuid();
            supplier.IsDeleted = false;
            supplier.TimeCreated = DateTime.Now;
            _ProductSupplierRepository.Insert(supplier);
        }

        public void Update(ProductSupplier supplier)
        {
            var entity = _ProductSupplierRepository.Table.FirstOrDefault(q => q.Guid == supplier.Guid && !q.IsDeleted);
            if (entity == null) return;
            entity.SupplierName = supplier.SupplierName;
            entity.SupplierPhone = supplier.SupplierPhone;
            entity.SupplierAddress = supplier.SupplierAddress;
            _ProductSupplierRepository.Update(entity);
        }

        public void Delete(Guid guid)
        {
            var entity = Find(guid);
            if (entity == null) return;
            entity.IsDeleted = true;
            _ProductSupplierRepository.Update(entity);
        }
    }
}
