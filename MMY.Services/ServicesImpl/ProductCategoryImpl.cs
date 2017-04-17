using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Services.ServicesImpl
{
    public class ProductCategoryImpl:IProductCategory
    {
        private IRepository<ProductCategory> _productCategoryRepository;
        public ProductCategoryImpl(IRepository<ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        /// <summary>
        /// 所有一级分类
        /// </summary>
        /// <returns></returns>
        public IList<ProductCategory> GetAllParentCategory()
        {
            return _productCategoryRepository.Table.Where(q => !q.IsDeleted && q.ParentGuid == Guid.Empty)
                .OrderByDescending(q=>q.DisplayOrder).ThenByDescending(q=>q.TimeCreated).ToList();
        }
        /// <summary>
        /// 某分类的所有子分类
        /// </summary>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        public IList<ProductCategory> GetCategoriesByParent(Guid parentGuid)
        {
            return _productCategoryRepository.Table.Where(q => !q.IsDeleted && q.ParentGuid == parentGuid).OrderByDescending(q=>q.DisplayOrder).ToList();
        }

        public void AddCategory(ProductCategory category)
        {
            category.Guid = Guid.NewGuid();
            category.TimeCreated=DateTime.Now;
            category.IsDeleted = false;
            _productCategoryRepository.Insert(category);
        }

        public void UpdateCategory(ProductCategory category)
        {
            var entity=_productCategoryRepository.Table.FirstOrDefault(q => q.Guid == category.Guid && !q.IsDeleted);
            if (entity != null)
            {
                entity.CategoryName = category.CategoryName;
                entity.DisplayOrder = category.DisplayOrder;
                _productCategoryRepository.Update(entity);
            }
        }

        public ProductCategory FindCategory(Guid categoryGuid)
        {
            return _productCategoryRepository.Table.FirstOrDefault(q => q.Guid == categoryGuid && !q.IsDeleted);
        }

        public void DeleteCategory(Guid categoryGuid)
        {
            var entity = _productCategoryRepository.Table.FirstOrDefault(q => q.Guid == categoryGuid && !q.IsDeleted);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _productCategoryRepository.Update(entity);
            }
        }
    }
}
