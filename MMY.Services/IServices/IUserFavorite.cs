using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data;
using MMY.Data.Model;

namespace MMY.Services.IServices
{
    public interface IUserFavorite
    {
        void Add(Guid userGuid,Guid productGuid);
        IList<UserFavorite> GetUserFavorites(int skip,int take,out int total);
        void Delete(Guid uid);
    }
}
