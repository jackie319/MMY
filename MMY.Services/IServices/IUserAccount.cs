using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;

namespace MMY.Services.IServices
{
    public interface IUserAccount
    {
        UserAccount Login(string userName,string password);

        void ChangePwd(string userName,string oldPassowrdMd5, string newPasswordMd5);
    }
}
