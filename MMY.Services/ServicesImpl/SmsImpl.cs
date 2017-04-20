using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using JK.Framework.Extensions;
using JK.Framework.Sms.Netease;
using log4net;
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
        private SmsCode smsCode;
        public SmsImpl(IRepository<SmsRecords> smsRecordsRepository)
        {
            _SmsRecordsRepository = smsRecordsRepository;
            smsCode = new SmsCode(Appkey, AppSecret, RegisteCodeTemplateid, RegisteUrl);
        }

        public SmsRecords AddRegisteRecord(string phone)
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
            return record;
        }

        public void UpdateRegisteRecord(Guid  recordGuid,string code,string obj,string msg)
        {
            var entity = _SmsRecordsRepository.Table.FirstOrDefault(q => q.Guid == recordGuid);
            entity.RadomCode = obj;
            entity.ResultStatusCode = code+":"+msg;
            entity.TimeUpdate=DateTime.Now;
            _SmsRecordsRepository.Update(entity);
        }

        public void Validate(Guid recordGuid)
        {
            var entity = _SmsRecordsRepository.Table.FirstOrDefault(q => q.Guid == recordGuid);
            if (entity != null)
            {
                entity.IsValidated = true;
                entity.TimeUpdate = DateTime.Now;
                _SmsRecordsRepository.Update(entity);
            }
          
        }

        public SmsRecords FindRegisteRecord(string phone)
        {
            var type = SmsTypeEnum.Registe.ToString();
            var list = _SmsRecordsRepository.Table.Where(q => !q.IsValidated && q.SmsType.Equals(type) && q.Phone.Equals(phone)).ToList();
            var entity= list.OrderByDescending(q => q.TimeCreated).FirstOrDefault();
            if (entity!=null)
            {
                if ((DateTime.Now - entity.TimeUpdate).TotalMinutes > 10)
                {
                    throw  new CommonException("验证码已过期");
                }
            }
            
            return entity;
        }


        public void SendRegisteCode(string phone)
        {
            var receord = AddRegisteRecord(phone);
            var model =smsCode.SendRegisteCode(phone) ;
            UpdateRegisteRecord(receord.Guid, model.code, model.obj ?? string.Empty, model.msg ?? string.Empty);
            if (!model.code.Equals("200"))
            {
                var logger = LogManager.GetLogger(typeof(SmsImpl));
                logger.Error("发送短信返回错误：" + model.code + ":" + model.msg ?? string.Empty);
            }
        }

    }
}
