using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class ProductPurchaseRecordsListViewModel
    {
        public System.Guid Guid { get; set; }
        public Guid ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string ClassificationName { get; set; }
        public decimal BuyingPrice { get; set; }
        public string SupplierName { get; set; }
        public string OperatorName { get; set; }
        public string Purchaser { get; set; }
        public int Number { get; set; }
        public string Remark { get; set; }
        public System.DateTime TimeCreated { get; set; }

        public static ProductPurchaseRecordsListViewModel CopyFrom(ProductPurchaseRecords records)
        {
            ProductPurchaseRecordsListViewModel result =new ProductPurchaseRecordsListViewModel();
            result.Guid = records.Guid;
            result.ProductGuid = records.ProductGuid;
            result.ProductName = records.ProductName;
            result.SupplierName = records.SupplierName;
            result.OperatorName = records.OperatorName;
            result.Purchaser = records.Purchaser;
            result.Number = records.Number;
            result.Remark = records.Remark;
            result.BuyingPrice = Convert.ToDecimal(records.BuyingPrice) / 100;
            result.ClassificationName = records.ClassificationName;//TODO：冗余的字段，有可能不是最新的。
            result.TimeCreated = records.TimeCreated;
            return result;
        }

    }

    public class ProductPurchaseRecordsViewModel
    {
        public System.Guid ProductGuid { get; set; }

        public Guid ClassificationGuid { get; set; }
        public System.Guid SupplierGuid { get; set; }
        [Required]
        public string Purchaser { get; set; }
        public int Number { get; set; }
        public string Remark { get; set; }

        public int Grams { get; set; }
        public decimal BuyingPrice { get; set; }
       
        public ProductPurchaseRecords CopyTo()
        {
            ProductPurchaseRecords records=new ProductPurchaseRecords();
            records.ProductGuid = ProductGuid;
            records.SupplierGuid = SupplierGuid;
            records.ClassificationGuid = ClassificationGuid;
            records.Purchaser = Purchaser;
            records.Number = Number;
            records.Remark = Remark??string.Empty;
            records.BuyingPrice = Convert.ToInt32(BuyingPrice * 100);
            records.Grams = Grams;
            return records;
        }
    }
}