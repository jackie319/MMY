using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;
using MMY.Services.ServiceModel;

namespace MMY.Services.IServices
{
   public  interface IAuthority
   {
        /// <summary>
        /// 用户所属角色的所有权限
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
       IList<UserMenuModel> GetUserMenu(Guid roleGuid, Guid parentGuid);
   }
}
