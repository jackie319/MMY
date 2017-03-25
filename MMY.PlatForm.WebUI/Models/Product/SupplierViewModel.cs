using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class SupplierViewModel
    {
        public Guid Guid { set; get; }
        /// <summary>
        /// 供货商名称
        /// </summary>
        [Required(ErrorMessage = "供货商名称必填")]
        public string SupplierName { get; set; }
        /// <summary>
        /// 供货商电话
        /// </summary>
        [Required(ErrorMessage = "供货商电话必填")]
        public string SupplierPhone { get; set; }
        /// <summary>
        /// 供货商地址
        /// </summary>
        [Required(ErrorMessage = "供货商地址必填")]
        public string SupplierAddress { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime?  TimeCreated { get; set; }

        public static SupplierViewModel CopyFrom(ProductSupplier supplier)
        {
            SupplierViewModel model = new SupplierViewModel();
            model.Guid = supplier.Guid;
            model.SupplierName = supplier.SupplierName;
            model.SupplierAddress = supplier.SupplierAddress;
            model.SupplierPhone = supplier.SupplierPhone;
            model.TimeCreated = supplier.TimeCreated;
            return model;
        }

        public ProductSupplier CopyTo()
        {
            ProductSupplier supplier = new ProductSupplier();
            supplier.Guid = Guid;
            supplier.SupplierName = SupplierName;
            supplier.SupplierPhone = SupplierPhone;
            supplier.SupplierAddress = SupplierAddress;
            return supplier;
        }
    }
}