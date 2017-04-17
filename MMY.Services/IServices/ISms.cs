using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;

namespace MMY.Services.IServices
{
    public interface ISms
    {
        void SendRegisteCode(string phone);
        SmsRecords FindRegisteRecord(string phone);
        void Validate(Guid recordGuid);
    }
}
