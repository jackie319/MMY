using System;

namespace MMY.FrontSite.WebApi.Models.ShoppingCart
{
    public class AddShoppingCartViewModel
    {
        /// <summary>
        /// 产品guid
        /// </summary>
        public Guid ProductGuid { get; set; }
        /// <summary>
        /// 颜色分类guid
        /// </summary>
        public Guid ClassificationGuid { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int ProductNum { set; get; }
    }
}