using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMY.FrontSite.WebUI.Models.UserAccount
{
    public class RegisterViewModel
    {
        [Required]
        public string MobilePhone { set; get; }
        [Required]
        public string PasswordMd5 { set; get; }

        public string SmsCode { get; set; }
    }
}