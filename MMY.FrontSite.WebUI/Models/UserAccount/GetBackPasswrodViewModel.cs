﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMY.FrontSite.WebUI.Models.UserAccount
{
    public class GetBackPasswrodViewModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "密码不能少于6位")]
        [MaxLength(32, ErrorMessage = "密码最长为32位")]
        public string PasswordMd5 { set; get; }

        public string SmsCode { get; set; }
    }
}