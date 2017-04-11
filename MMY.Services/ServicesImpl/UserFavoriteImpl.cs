using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Services.ServicesImpl
{
    public class UserFavoriteImpl : IUserFavorite
    {
        private IRepository<UserFavorite> _userFavoriteRepository;
        public UserFavoriteImpl(IRepository<UserFavorite> userFavoriteRepository)
        {
            _userFavoriteRepository = userFavoriteRepository;
        }
        public void Add(Guid userGuid, Guid productGuid)
        {
            UserFavorite favorite = new UserFavorite();
            favorite.Guid = Guid.NewGuid();
            favorite.IsDeleted = false;
            favorite.ProductGuid = productGuid;
            favorite.TimeCreated = DateTime.Now;
            favorite.UserGuid = userGuid;
            _userFavoriteRepository.Insert(favorite);
        }

        public IList<UserFavorite> GetUserFavorites(Guid userGuid,int skip, int take, out int total)
        {
            var query = _userFavoriteRepository.Table.Where(q => !q.IsDeleted && q.UserGuid==userGuid);
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated)
                    .Skip(skip)
                    .Take(take)
                    .ToList();
        }

        public void Delete(Guid uid)
        {
            var entity = _userFavoriteRepository.Table.FirstOrDefault(q => q.Guid == uid);
            entity.IsDeleted = true;
            _userFavoriteRepository.Update(entity);
        }
    }
}
