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
            return _productCategoryRepository.Table.Where(q => !q.IsDeleted && q.ParentGuid == Guid.Empty).OrderByDescending(q=>q.DisplayOrder).ToList();
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


    }
}
