using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.PlatForm.WebUI.Models
{
    public class ResultModel<T>
    {
        public bool Success { set; get; }
        public string ErrorInfo { set; get; }
        public T Model { set; get; }

        public int Total { set; get; }
    }
}