using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;

namespace MMY.Services.IServices
{
    public interface IProductCategory
    {
        IList<ProductCategory> GetAllParentCategory();

        IList<ProductCategory> GetCategoriesByParent(Guid parentGuid);
    }
}
