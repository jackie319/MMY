using System.ComponentModel.DataAnnotations;

namespace MMY.FrontSite.WebApi.Models.UserAccount
{
    public class RegisterViewModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string MobilePhone { set; get; }
        /// <summary>
        /// MD5密码
        /// </summary>
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