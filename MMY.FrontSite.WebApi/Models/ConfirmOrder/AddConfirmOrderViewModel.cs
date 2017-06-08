using System;
using System.ComponentModel.DataAnnotations;

namespace MMY.FrontSite.WebApi.Models.ConfirmOrder
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