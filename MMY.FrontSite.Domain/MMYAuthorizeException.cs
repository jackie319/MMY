using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.FrontSite.Domain
{
    public class MMYAuthorizeException:Exception
    {
        public MMYAuthorizeException(string msg) : base(msg)
        {

        }
    }
}
