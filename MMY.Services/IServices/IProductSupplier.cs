using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;

namespace MMY.Services.IServices
{
    public interface IProductSupplier
    {
        IList<ProductSupplier> Query(string supplierName,string supplierAddress,int skip,int take,out int total);
        ProductSupplier Find(Guid uid);
        ProductSupplier FindWithCache(Guid uid);
    }
}
