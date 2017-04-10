using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMY.Data.Model;

namespace MMY.FrontSite.WebUI.Models.UserAccount
{
    public class UserDeliveryAddressViewModel
    {
        public System.Guid Guid { get; set; }
        //public System.Guid ProvinceGuid { get; set; }
        //public System.Guid CityGuid { get; set; }
        //public System.Guid DistrictGuid { get; set; }
        //public System.Guid ZipCodeGuid { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
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
            return address;
        }
    }
}