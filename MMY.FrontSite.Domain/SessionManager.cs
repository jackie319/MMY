using JK.Framework.Core.Caching;
using JK.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.FrontSite.Domain
{
    public class SessionManager
    {
        public static string GetSessionKey(Guid userGuid)
        {
            //TODO：sessionkey简单处理， 每个用户只有一个session
            // 后期考虑使用userGuid +当前时间 进行MD5（盐也放数据库） 并存入数据库
            //并根据ip地址，登录设备等做复杂处理
            return userGuid.ToString("N").ToMd5WithSalt("MMY"); ;
        }


    }
}
