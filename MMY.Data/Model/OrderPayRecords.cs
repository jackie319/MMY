//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMY.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderPayRecords
    {
        public System.Guid Guid { get; set; }
        public System.Guid OrderGuid { get; set; }
        public System.Guid PaymentGuid { get; set; }
        public System.Guid PayBatch { get; set; }
        public string PayResult { get; set; }
        public string Remark { get; set; }
        public System.DateTime TimeUpdate { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
