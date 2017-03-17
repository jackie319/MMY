using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.Services.ServiceModel
{
    public enum ProductStatusEnum
    {
        /// <summary>
        /// 未上架
        /// </summary>
        Default,
        /// <summary>
        /// 已上架
        /// </summary>
        OnShelf,
        /// <summary>
        /// 已下架
        /// </summary>
        OffShelf,
        /// <summary>
        /// 所有
        /// </summary>
        All,
    }
}
