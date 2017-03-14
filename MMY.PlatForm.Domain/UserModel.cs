using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Services.ServiceModel;

namespace MMY.PlatForm.Domain
{
    public  class UserModel
    {
        public Guid UserGuid { set; get; }
        public string UserName { set; get; }
        public string NickName { set; get; }

        public IList<UserMenuModel> UserMenuModels { set; get; }
    }
    
}
