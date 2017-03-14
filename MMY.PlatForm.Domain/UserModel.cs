using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.PlatForm.Domain
{
    public  class UserModel
    {

    }

    public class UserInfo
    {
        public Guid UserGuid { set; get; }
        public string UserName { set; get; }
        public string NickName { set; get; }
    }

    
}
