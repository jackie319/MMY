using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using MMY.Data.Model;

namespace MMY.Services.IServices
{
    public interface IUserAccount
    {
        UserAccount Login(string userName,string password);

        void ChangePwd(string userName,string oldPassowrdMd5, string newPasswordMd5);

        Boolean IsUserNameExist(string userName);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <param name="smsCode"></param>
        /// <exception cref="CommonException">CommonException</exception>
        void Register(string mobilePhone, string password, string smsCode);
    }
}
