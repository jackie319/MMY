using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.Services.ServicesImpl
{
    public class ShoppingCartImpl:IShoppingCart
    {
        private IRepository<UserShoppingCart> _userShoppingCartRepository;
        public ShoppingCartImpl(IRepository<UserShoppingCart> userShoppingCartRepository)
        {
            _userShoppingCartRepository = userShoppingCartRepository;
        }

        public void Add(Guid userGuid, Guid productGuid, Guid classificationGuid, int num)
        {
            UserShoppingCart shoppingCart=new UserShoppingCart();
            shoppingCart.Guid = Guid.NewGuid();
            shoppingCart.IsDeleted = false;
            shoppingCart.UserGuid = userGuid;
            shoppingCart.ProductGuid = productGuid;
            shoppingCart.TimeCreated=DateTime.Now;
            shoppingCart.ClassificationGuid = classificationGuid;
            shoppingCart.ProductNum = num;
            shoppingCart.Status = ShoppingCartEnum.Default.ToString();
            _userShoppingCartRepository.Insert(shoppingCart);
        }

        public IList<UserShoppingCart> GetList(Guid userGuid,int skip,int take,out int total)
        {
            var query = _userShoppingCartRepository.Table.Where(q=>!q.IsDeleted && q.UserGuid==userGuid);
            total=query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public void Delete(Guid shoppingCartGuid)
        {
            var entity=_userShoppingCartRepository.Table.FirstOrDefault(q => q.Guid == shoppingCartGuid);
            if (entity == null) return;
            entity.IsDeleted = true;
            _userShoppingCartRepository.Update(entity);
        }

        /// <summary>
        /// 更改数量
        /// </summary>
        /// <param name="shoppingCartGuid"></param>
        /// <param name="num"></param>
        public void UpdateShoppingCartNum(Guid shoppingCartGuid, int num)
        {

            var entity = _userShoppingCartRepository.Table.FirstOrDefault(q => q.Guid == shoppingCartGuid);
            if (entity == null) return;
            entity.ProductNum = num;
            _userShoppingCartRepository.Update(entity);
        }
        /// <summary>
        /// 更改分类
        /// </summary>
        /// <param name="shoppingCartGuid"></param>
        /// <param name="classificationGuid"></param>
        public void UpdateClassification(Guid shoppingCartGuid, Guid classificationGuid)
        {
            var entity = _userShoppingCartRepository.Table.FirstOrDefault(q => q.Guid == shoppingCartGuid);
            if (entity == null) return;
            entity.ClassificationGuid = classificationGuid;
            _userShoppingCartRepository.Update(entity);
        }
    }
}
