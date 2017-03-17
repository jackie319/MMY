using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;
using MMY.Services.ServiceModel;

namespace MMY.Services.IServices
{
    public interface IProduct
    {
        IList<ProductV> GetProductVs(string productName, Guid? categoryGuid, ProductStatusEnum status,
            bool? isSpecialOffer, bool? isRecommended,
            DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total);

        void  CreatedProduct(Product product);
    }
}
