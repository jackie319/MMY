using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using JK.Framework.Data;
using MMY.FrontSite.WebUI;
using MMY.PlatForm.WebUI;
using MMY.Services.IServices;
using MMY.Services.ServicesImpl;

namespace MMY.Test
{
    public class Initial
    {
        public static IContainer _container;
        public static void RegisterAutofac()
        {
            string connectionStr = System.Web.Configuration.WebConfigurationManager.
                ConnectionStrings["MMYEntities"].ConnectionString;

            RegisterAutofacForJK.Register(connectionStr, AutoFacRegister.RegisterAutofacDelegate);
            _container = RegisterAutofacForJK._container;

        }

    }
}
