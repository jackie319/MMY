using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Services.ServicesImpl
{
    public class SmsImpl:ISms
    {
        private IRepository<SmsRecords> _SmsRecordsRepository;

        public SmsImpl(IRepository<SmsRecords> smsRecordsRepository)
        {
            _SmsRecordsRepository = smsRecordsRepository;
        }
    }
}
