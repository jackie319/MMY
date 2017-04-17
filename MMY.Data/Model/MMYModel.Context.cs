﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MMYEntities : DbContext
    {
        public MMYEntities()
            : base("name=MMYEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<AuthorityFunction> AuthorityFunction { get; set; }
        public virtual DbSet<AuthorityRole> AuthorityRole { get; set; }
        public virtual DbSet<AuthorityRoleInFunction> AuthorityRoleInFunction { get; set; }
        public virtual DbSet<AuthorityUserInRole> AuthorityUserInRole { get; set; }
        public virtual DbSet<Merchant> Merchant { get; set; }
        public virtual DbSet<MerchantProduct> MerchantProduct { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDelivery> OrderDelivery { get; set; }
        public virtual DbSet<OrderEvaluation> OrderEvaluation { get; set; }
        public virtual DbSet<OrderPayRecords> OrderPayRecords { get; set; }
        public virtual DbSet<PositionCity> PositionCity { get; set; }
        public virtual DbSet<PositionDistrict> PositionDistrict { get; set; }
        public virtual DbSet<PositionProvince> PositionProvince { get; set; }
        public virtual DbSet<PositionZipCode> PositionZipCode { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAlbum> ProductAlbum { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductQRCode> ProductQRCode { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserDeliveryAddress> UserDeliveryAddress { get; set; }
        public virtual DbSet<UserFavorite> UserFavorite { get; set; }
        public virtual DbSet<UserOperationRecords> UserOperationRecords { get; set; }
        public virtual DbSet<UserShoppingCart> UserShoppingCart { get; set; }
        public virtual DbSet<OrderV> OrderV { get; set; }
        public virtual DbSet<ProductV> ProductV { get; set; }
        public virtual DbSet<OrderPayment> OrderPayment { get; set; }
        public virtual DbSet<ProductClassification> ProductClassification { get; set; }
        public virtual DbSet<ProductPurchaseRecords> ProductPurchaseRecords { get; set; }
        public virtual DbSet<SmsRecords> SmsRecords { get; set; }
    }
}
