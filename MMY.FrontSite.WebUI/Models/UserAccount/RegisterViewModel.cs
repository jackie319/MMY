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
        [MinLength(6, ErrorMessage = "密码不能少于6位")]
        [MaxLength(32, ErrorMessage = "密码最长为32位")]
        public string PasswordMd5 { set; get; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        [Required]
        [MinLength(2)]
        public string SmsCode { get; set; }
    }
}