using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JK.Framework.Web.Filter;
using JK.Framework.Web.Model;
using log4net;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class WechatPayController : Controller
    {
        private IOrder _order;
        private IPay _pay;
        private ILog _log;
        public WechatPayController(IOrder order, IPay pay)
        {
            _order = order;
            _pay = pay;
            _log=LogManager.GetLogger(typeof(FilterConfig));
        }
        [JKAuthorize]
        public ActionResult PayBatch(IList<Guid> orderGuids)
        {
            foreach (var orderGuid in orderGuids)
            {
                _pay.WechatPay(orderGuid);
            }

            return this.ResultSuccess();
        }

        [JKAuthorize]
        public ActionResult Pay(Guid orderGuid)
        {
            _pay.WechatPay(orderGuid);

            return this.ResultSuccess();
        }

        public ActionResult Notify()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();
            var xmlstring = builder.ToString();
            _log.Debug($"收到微信支付通知：{xmlstring}");
            //转换数据格式并验证签名

            //更新相应订单信息
            return null;
        }
    }
}