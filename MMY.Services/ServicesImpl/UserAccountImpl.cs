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
    }
}
