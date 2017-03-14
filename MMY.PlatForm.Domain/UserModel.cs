using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Web.Filter;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.PlatForm.Domain
{
    public  class UserModel:UserBase
    {
        public Guid UserGuid { set; get; }
        public string UserName { set; get; }
        public string NickName { set; get; }

        public IList<UserMenuModel> UserMenuModels { set; get; }

        public UserModel(Guid userGuid, string userName, string nickName,IList<UserMenuModel> menus, Boolean isAuthenticated) :base(userName,"MMY", isAuthenticated)
        {
            UserGuid = userGuid;
            UserName = userName;
            NickName = nickName;
            UserMenuModels = menus;
        }
    }
    
}
