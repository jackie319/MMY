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
    
    public partial class ProductQRCode
    {
        public System.Guid Guid { get; set; }
        public System.Guid ProductGuid { get; set; }
        public System.Guid ClassGuid { get; set; }
        public System.Guid SupplierGuid { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
