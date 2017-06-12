using JK.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMY.FrontSite.WebApi.Models.Query
{
    public class QueryBaseApi
    {
        public int Skip { get; set; }
        public int Take { get; set; }

    }
}