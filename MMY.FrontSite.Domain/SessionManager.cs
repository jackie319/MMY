using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.FrontSite.Domain
{
    public class SessionManager
    {
        /// <summary>
        /// 身份标识
        /// </summary>
        public string SessionKey { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserModel UserInfo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime TimeCreated { get; set; }
    }
}
