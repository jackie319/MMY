using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;
using Newtonsoft.Json;

namespace MMY.Services.ServicesImpl
{
    public class SmsImpl : ISms
    {
        private IRepository<SmsRecords> _SmsRecordsRepository;

        public SmsImpl(IRepository<SmsRecords> smsRecordsRepository)
        {
            _SmsRecordsRepository = smsRecordsRepository;
        }

        public void AddRegisteRecord(string phone)
        {
            SmsRecords record = new SmsRecords();
            record.Guid = Guid.NewGuid();
            record.RadomCode = string.Empty;
            record.Remark = "注册验证码";
            record.Phone = phone;
            record.IsValidated = false;
            record.SmsType = SmsTypeEnum.Registe.ToString();
            record.ResultStatusCode = string.Empty;
            record.TimeCreated = DateTime.Now;
            record.TimeUpdate = DateTime.MaxValue;
            _SmsRecordsRepository.Insert(record);
        }

        public void UpdateRegisteRecord(Guid  recordGuid,string code,string obj,string msg)
        {
            var entity = _SmsRecordsRepository.Table.FirstOrDefault(q => q.Guid == recordGuid);
            entity.RadomCode = obj;
            entity.ResultStatusCode = code;
            entity.TimeUpdate=DateTime.Now;
            _SmsRecordsRepository.Update(entity);
        }

        public SmsRecords FindRegisteRecord(string phone)
        {
            var type = SmsTypeEnum.Registe.ToString();
            var list = _SmsRecordsRepository.Table.Where(q => !q.IsValidated && q.SmsType.Equals(type) && q.Phone.Equals(phone)).ToList();
            return list.OrderByDescending(q => q.TimeCreated).FirstOrDefault();
        }

        public void SendRegisteCode(string phone)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
                string url = "https://api.netease.im/sms/sendcode.action";
                string data = "mobile=" + phone + "&templateid =" + 3050311;
                var result = webClient.UploadString(url, "POST", data);
                SendRegisteCodeResult model = JsonConvert.DeserializeObject<SendRegisteCodeResult>(result);
                if (model.code.Equals("200"))
                {

                }
                else
                {
                    throw new CommonException("发送短信返回错误："+model.code);
                }
            }
        }
    }
}
