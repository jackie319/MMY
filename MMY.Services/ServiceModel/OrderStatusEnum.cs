using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMY.Services.ServiceModel
{
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 未支付
        /// </summary>
        Default,
        /// <summary>
        /// 已取消
        /// </summary>
        Cancel,
        /// <summary>
        /// 支付中
        /// </summary>
        Paying,
        /// <summary>
        /// 已支付
        /// </summary>
        Paid,
        /// <summary>
        /// 支付失败
        /// </summary>
        PayFailure,
        /// <summary>
        /// 已发货
        /// </summary>
        Delivered,
        /// <summary>
        /// 已完成
        /// </summary>
        Finished,
        /// <summary>
        /// 已退款
        /// </summary>
        Refund

    }

    public enum ShoppingCartEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 已转化为订单
        /// </summary>
        BeOrder

    }

    public enum PaymentEnum
    {
        Alipay,
        WechatPay
    }
}
