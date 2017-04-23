using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.PlatForm.WebUI.Models.Product
{
    public class ProductClassificationListViewModel
    {
        public System.Guid Guid { get; set; }
        public string Name { get; set; }

        public static ProductClassificationListViewModel CopyFrom(ProductClassification classClassification)
        {
            ProductClassificationListViewModel model=new ProductClassificationListViewModel();
            model.Guid = classClassification.Guid;
            model.Name = classClassification.Name;
            return model;
        }
    }
}