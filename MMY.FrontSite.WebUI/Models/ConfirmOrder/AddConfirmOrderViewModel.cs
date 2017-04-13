using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMY.FrontSite.WebUI.Models.ConfirmOrder
{
    public class AddConfirmOrderViewModel
    {
        [Required]
        public Guid ProductGuid { get; set; }
        [Required]
        public Guid ClassificationGuid { get; set; }
        [Required]
        public int ProductNum { set; get; }
    }
}