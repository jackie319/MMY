using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMY.Services.IServices;

namespace MMY.FrontSite.WebUI.Controllers
{
    public class SmsController : Controller
    {
        private ISms _sms;
        public SmsController(ISms sms)
        {
            _sms = sms;
        }
        // GET: Sms
        public ActionResult Index()
        {
            return View();
        }
    }
}