using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.Services.ServicesImpl
{
    public class AppSetting
    {
        private static AppSetting _AppSetting;

        private AppSetting()
        {
        }

        public static AppSetting Instance()
        {
            if (_AppSetting == null) _AppSetting= new AppSetting();
            return _AppSetting;
        }
        public string PictureUrl { get; set; }
    }
}
