using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.FrontSite.WebUI.Models.UserAccount
{
    public class LogInViewModel
    {
        public string UserName { set; get; }

        public string PasswordMd5 { set; get; }
    }
}