using System;
using System.Collections.Generic;
using System.IO;
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

        private const string Appkey = "34a5fc42e8d6bf0e9a3817a1af8e901a";
        private const string AppSecret = "7e68c58203ed";
        private const string RegisteCodeTemplateid = "3050311";
        private const string RegisteUrl = "https://api.netease.im/sms/sendcode.action";

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
                string nonce = Guid.NewGuid().ToString("N");
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime now = DateTime.Now;
                string curTime = (now - dt).TotalSeconds.ToString();

                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
                webClient.Headers.Add("AppKey",Appkey);
                webClient.Headers.Add("Nonce",nonce);
                webClient.Headers.Add("CurTime",curTime);
                webClient.Headers.Add("CheckSum",Sha1(nonce,curTime));
                string url = RegisteUrl;
                string data = "mobile=" + phone + "&templateid =" + RegisteCodeTemplateid;
                var result = webClient.UploadString(url, "POST", data);
                SendRegisteCodeResult model = JsonConvert.DeserializeObject<SendRegisteCodeResult>(result);
                if (model.code.Equals("200"))
                {

                }
                else
                {
                    throw new CommonException("发送短信返回错误："+model.code+":"+model.msg);
                }
            }
        }

        public string Sha1(string nonce,string curTime)
        {
            string str = AppSecret + nonce + curTime;
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1byte= sha1.ComputeHash(stream);
            return sha1byte.ToString();
        }
    }
}
