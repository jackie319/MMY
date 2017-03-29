using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using JK.Framework.Extensions;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.Services.ServicesImpl
{
    public class UserAccountImpl:IUserAccount
    {
        private IRepository<UserAccount> _userAccountRepository;
        private readonly string _Salt = "_MMYPlatform";
        public UserAccountImpl(IRepository<UserAccount> useraccountRepository )
        {
            _userAccountRepository = useraccountRepository;
        }
        public UserAccount Login(string userName,string password)
        {
            var userAccount= _userAccountRepository.Table.FirstOrDefault(q => !q.IsDeleted && q.UserName.Equals(userName));
             if(userAccount==null)throw new CommonException("用户不存在");
            var passwordSalt = password.ToMd5WithSalt(_Salt);
             if(!userAccount.Password.Equals(passwordSalt)) throw new CommonException("密码错误");
            userAccount.Password = "";
            return userAccount;
        }

        public void ChangePwd(string userName,string oldPassowrdMd5, string newPasswordMd5)
        {
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q => !q.IsDeleted && q.UserName.Equals(userName));
            if (userAccount == null) throw new CommonException("用户不存在");
            var passwordSalt = oldPassowrdMd5.ToMd5WithSalt(_Salt);
            if (!userAccount.Password.Equals(passwordSalt)) throw new CommonException("原密码错误");
            var newPasswordSalt = newPasswordMd5.ToMd5WithSalt(_Salt);
            userAccount.Password = newPasswordSalt;
            _userAccountRepository.Update(userAccount);
        }

        public Boolean IsUserNameExist (string userName)
        {
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q =>q.UserName.Equals(userName));
            if (userAccount == null) return false;
            return true;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <param name="smsCode"></param>
        /// <exception cref="CommonException">CommonException</exception>
        public void Register(string mobilePhone,string password,string smsCode)
        {
            if(IsUserNameExist(mobilePhone))throw new CommonException("用户已存在");
            //TODO:先调用匹配验证码方法
            UserAccount account=new UserAccount();
            account.Guid = Guid.NewGuid();
            account.AvatarImgUrl = string.Empty;
            account.UserType = UserTypeEnum.Member.ToString();
            account.Birthday=DateTime.MinValue;
            account.CountVisited = 0;
            account.Email = string.Empty;
            account.Gender = GenderEnum.NotSet.ToString();
            account.IPv4LastVisited = string.Empty;
            account.IsDeleted = false;
            account.IsEmailValidated = false;
            account.IsMobilePhoneValidated = true;
            account.UserName = mobilePhone;
            account.MobilePhone = mobilePhone;
            account.NickName = mobilePhone;
            account.Password = password.ToMd5WithSalt(_Salt);
            account.TimeCreated=DateTime.Now;
            account.TimeLastVisited=DateTime.Now;
            account.Status = UserStatusEnum.Default.ToString();
            _userAccountRepository.Insert(account);
        }
    }
}
