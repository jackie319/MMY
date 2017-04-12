using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;

namespace MMY.Services.IServices
{
    public interface IShoppingCart
    {
        void Add(Guid userGuid, Guid productGuid,Guid classificationGuid,int num);
        IList<UserShoppingCart> GetList(Guid userGuid,int skip, int take, out int total);
        void Delete(Guid shoppingCartGuid);

        void UpdateShoppingCartNum(Guid shoppingCartGuid,int num);

        void UpdateClassification(Guid shoppingCartGuid, Guid classificationGuid);
    }
}
