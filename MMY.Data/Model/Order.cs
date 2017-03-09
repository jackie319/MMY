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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderEvaluation = new HashSet<OrderEvaluation>();
            this.OrderPayRecords = new HashSet<OrderPayRecords>();
        }
    
        public System.Guid Guid { get; set; }
        public string OrderNo { get; set; }
        public int OrderAmount { get; set; }
        public System.Guid ProductGuid { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int productNumber { get; set; }
        public System.Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string UserNickName { get; set; }
        public System.Guid DeliveryAddressGuid { get; set; }
        public string DeliveryAddress { get; set; }
        public System.Guid PaymentGuid { get; set; }
        public string PaymentName { get; set; }
        public string PayBatch { get; set; }
        public System.Guid DeliveryGuid { get; set; }
        public string DeliveryName { get; set; }
        public string OrderStatus { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual OrderDelivery OrderDelivery { get; set; }
        public virtual OrderPayment OrderPayment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderEvaluation> OrderEvaluation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayRecords> OrderPayRecords { get; set; }
    }
}
