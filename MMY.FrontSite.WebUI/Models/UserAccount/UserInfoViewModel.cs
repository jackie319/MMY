using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MMY.Services.ServiceModel;

namespace MMY.FrontSite.WebUI.Models.UserAccount
{
    public class UserInfoViewModel
    {
        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarImgUrl { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public GenderEnum Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public System.DateTime Birthday { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        public Data.Model.UserAccount CopyTo()
        {
            Data.Model.UserAccount account=new Data.Model.UserAccount();
            account.NickName = NickName;
            account.AvatarImgUrl = AvatarImgUrl;
            account.Gender = Gender.ToString();
            account.Birthday = Birthday;
            account.Email = Email;
            return account;
        }

        public static UserInfoViewModel CopyFrom(Data.Model.UserAccount entity)
        {
            UserInfoViewModel model=new UserInfoViewModel();
            model.NickName = entity.NickName;
            model.AvatarImgUrl = entity.AvatarImgUrl;
            model.Gender = CovertToGenderEnum(entity.Gender);
            model.Birthday = entity.Birthday;
            model.Email = entity.Email;
            return model;
        }

        public static GenderEnum CovertToGenderEnum(string gender)
        {
            if(gender.Equals(GenderEnum.FeMale.ToString()))return GenderEnum.FeMale;
            if(gender.Equals(GenderEnum.Male.ToString()))return GenderEnum.Male;
            return GenderEnum.NotSet;
        }
    }
}