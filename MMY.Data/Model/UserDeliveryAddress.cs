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
    
    public partial class UserDeliveryAddress
    {
        public System.Guid Guid { get; set; }
        public System.Guid UserGuid { get; set; }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string ReceiverName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual UserAccount UserAccount { get; set; }
    }
}
