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
    
    public partial class AuthorityFunction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthorityFunction()
        {
            this.AuthorityRoleInFunction = new HashSet<AuthorityRoleInFunction>();
        }
    
        public System.Guid FunctionGuid { get; set; }
        public System.Guid ParentGuid { get; set; }
        public string FunctionName { get; set; }
        public string DisplayName { get; set; }
        public int DisplayOrder { get; set; }
        public string ActionUrl { get; set; }
        public string FunctionType { get; set; }
        public string PlatForm { get; set; }
        public bool Enable { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthorityRoleInFunction> AuthorityRoleInFunction { get; set; }
    }
}
