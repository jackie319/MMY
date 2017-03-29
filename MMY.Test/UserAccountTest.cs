using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JK.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMY.Services.IServices;

namespace MMY.Test
{
    [TestClass]
    public class UserAccountTest
    {
        private IUserAccount _userAccount;
        [TestInitialize]
        public void Initialize()
        {
            Initial.RegisterAutofac();
            _userAccount=Initial._container.Resolve<IUserAccount>();
        }

        [TestMethod]
        public void TestRegister()
        {
            string mobile = "18585854545";
            string password = "123456".ToMd5();
            _userAccount.Register(mobile,password,"");
        }
    }
}
