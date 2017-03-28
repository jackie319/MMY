using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Web.Filter;

namespace MMY.FrontSite.Domain
{
    public class UserModel : UserBase
    {
        public Guid UserGuid { set; get; }
        public string UserName { set; get; }
        public string NickName { set; get; }


        public UserModel(Guid userGuid, string userName, string nickName, Boolean isAuthenticated) : base(userName, "MMY", isAuthenticated)
        {
            UserGuid = userGuid;
            UserName = userName;
            NickName = nickName;
        }
    }
}
