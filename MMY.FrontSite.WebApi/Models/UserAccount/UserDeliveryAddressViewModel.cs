using System.ComponentModel.DataAnnotations;
using MMY.Data.Model;

namespace MMY.FrontSite.WebApi.Models.UserAccount
{
    public class UserDeliveryAddressViewModel
    {
        public System.Guid Guid { get; set; }
        /// <summary>
        /// 省市区
        /// </summary>
        [Required]
        public string Region { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [Required]
        public string ZipCode { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        [Required]
        public string ReceiverName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Phone { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// 是否是默认收货地址
        /// </summary>
        public bool IsDefault { get; set; }

        public static UserDeliveryAddressViewModel CopyTo(UserDeliveryAddress address)
        {
            UserDeliveryAddressViewModel model = new UserDeliveryAddressViewModel();
            model.Guid = address.Guid;
            model.Address = address.Address;
            model.IsDefault = address.IsDefault;
            model.Phone = address.Phone;
            model.ReceiverName = address.ReceiverName;
            model.Region = address.Region;
            model.ZipCode = address.ZipCode;
            return model;

        }

        public UserDeliveryAddress CopyFrom()
        {
            UserDeliveryAddress address = new UserDeliveryAddress();
            address.Guid = Guid;
            address.ReceiverName = ReceiverName;
            address.Phone = Phone;
            address.Address = Address;
            address.IsDefault = IsDefault;
            address.Region = Region;
            address.ZipCode = ZipCode;
            return address;
        }
    }
}