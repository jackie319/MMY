using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models.Account
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPasswordMd5 { set; get; }
        [Required]
        public string NewPasswordMd5 { set; get; }
        [Required]
        public string NewPasswordMd5Confirm { get; set; }
    }
}