using JK.Framework.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.FrontSite.Domain
{
    public class SessionManager
    {
     
        public static string GetSessionKey()
        {
            return Guid.NewGuid().ToString("N");
        }

      
    }
}
