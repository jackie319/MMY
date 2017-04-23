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
    public class SmsTest
    {
        private ISms _sms;
        [TestInitialize]
        public void Initialize()
        {
            Initial.RegisterAutofac();
            _sms = Initial._container.Resolve<ISms>();
        }

        [TestMethod]
        public void TestSendRegisteCode()
        {
           _sms.SendRegisteCode("18288215197");
            Assert.IsTrue(true);
        }

        public void SubmitSource()
        {
            Console.WriteLine("Hello world");
        }
    }
}
