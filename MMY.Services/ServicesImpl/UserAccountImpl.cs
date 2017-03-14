using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Services.ServicesImpl
{
    public class UserAccountImpl:IUserAccount
    {
        private IRepository<UserAccount> _userAccountRepository;
        public UserAccountImpl(IRepository<UserAccount> useraccountRepository )
        {
            _userAccountRepository = useraccountRepository;
        }
        public void Login(string userName,string password)
        {
            var userAccount= _userAccountRepository.Table.FirstOrDefault(q => !q.IsDeleted && q.UserName.Equals(userName));
             if(userAccount==null)throw new CommonException("用户名或密码错误");
             if(!userAccount.Password.Equals(password)) throw new CommonException("用户名或密码错误");

        }
    }
}
