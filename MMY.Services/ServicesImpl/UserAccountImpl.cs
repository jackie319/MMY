using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using JK.Framework.Extensions;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.Services.ServicesImpl
{
    public class UserAccountImpl:IUserAccount
    {
        private IRepository<UserAccount> _userAccountRepository;
        private IRepository<UserDeliveryAddress> _userDeliveryAddressRepository;
        private readonly string _Salt = "_MMYPlatform";
        public UserAccountImpl(IRepository<UserAccount> useraccountRepository,IRepository<UserDeliveryAddress> userDeliveryAddressRepository  )
        {
            _userAccountRepository = useraccountRepository;
            _userDeliveryAddressRepository = userDeliveryAddressRepository;
        }
        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount Login(string userName,string password)
        {
            var userAccount= _userAccountRepository.Table.FirstOrDefault(q => !q.IsDeleted && q.UserName.Equals(userName));
             if(userAccount==null)throw new CommonException("用户不存在");
            var passwordSalt = password.ToMd5WithSalt(_Salt);
             if(!userAccount.Password.Equals(passwordSalt)) throw new CommonException("密码错误");
            userAccount.Password = "";
            return userAccount;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userAccount"></param>
        public void UpdateUserInfo(UserAccount userAccount)
        {
            var entity =
                _userAccountRepository.Table.FirstOrDefault(
                    q => q.UserName == userAccount.UserName && q.Guid == userAccount.Guid);
            if (entity == null) throw new CommonException("用户不存在");
            entity.NickName = userAccount.NickName;
            entity.AvatarImgUrl = userAccount.AvatarImgUrl??string.Empty;
            entity.Birthday = userAccount.Birthday;
            entity.Email = userAccount.Email??string.Empty;
            entity.Gender = userAccount.Gender;
            _userAccountRepository.Update(entity);
        }

        public UserAccount FindUserAccount(Guid userAccountGuid)
        {
            return _userAccountRepository.Table.FirstOrDefault(q => q.Guid == userAccountGuid && !q.IsDeleted);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassowrdMd5"></param>
        /// <param name="newPasswordMd5"></param>
        public void ChangePwd(string userName,string oldPassowrdMd5, string newPasswordMd5)
        {
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q => !q.IsDeleted && q.UserName.Equals(userName));
            if (userAccount == null) throw new CommonException("用户不存在");
            var passwordSalt = oldPassowrdMd5.ToMd5WithSalt(_Salt);
            if (!userAccount.Password.Equals(passwordSalt)) throw new CommonException("原密码错误");
            var newPasswordSalt = newPasswordMd5.ToMd5WithSalt(_Salt);
            userAccount.Password = newPasswordSalt;
            _userAccountRepository.Update(userAccount);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Boolean IsUserNameExist (string userName)
        {
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q =>q.UserName.Equals(userName));
            if (userAccount == null) return false;
            return true;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <param name="smsCode"></param>
        /// <exception cref="CommonException">CommonException</exception>
        public void Register(string mobilePhone,string password,string smsCode)
        {
            if(IsUserNameExist(mobilePhone))throw new CommonException("用户已存在");
            //TODO:先调用匹配验证码方法
            UserAccount account=new UserAccount();
            account.Guid = Guid.NewGuid();
            account.AvatarImgUrl = string.Empty;
            account.UserType = UserTypeEnum.Member.ToString();
            account.Birthday=DateTime.MinValue;
            account.CountVisited = 0;
            account.Email = string.Empty;
            account.Gender = GenderEnum.NotSet.ToString();
            account.IPv4LastVisited = string.Empty;
            account.IsDeleted = false;
            account.IsEmailValidated = false;
            account.IsMobilePhoneValidated = true;
            account.UserName = mobilePhone;
            account.MobilePhone = mobilePhone;
            account.NickName = mobilePhone;
            account.Password = password.ToMd5WithSalt(_Salt);
            account.TimeCreated=DateTime.Now;
            account.TimeLastVisited=DateTime.Now;
            account.Status = UserStatusEnum.Default.ToString();
            _userAccountRepository.Insert(account);
        }
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <param name="smsCode"></param>
        public void GetBackPassword(string mobilePhone, string password, string smsCode)
        {
            //TODO:先调用匹配验证码方法
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q => q.UserName.Equals(mobilePhone));
            if(userAccount==null) throw new CommonException("用户不存在");
            userAccount.Password = password.ToMd5WithSalt(_Salt);
            _userAccountRepository.Update(userAccount);
        }

        /// <summary>
        /// 我的收货地址
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="total"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public IList<UserDeliveryAddress> GetList(Guid userGuid,int skip,int take,out int total)
        {
            var query=_userDeliveryAddressRepository.Table.Where(q=>!q.IsDeleted && q.UserGuid==userGuid);
            total = query.Count();
            return query.OrderBy(q=>q.IsDefault).ThenByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public UserDeliveryAddress FinDeliveryAddress(Guid addressGuid)
        {
            return _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == addressGuid && !q.IsDeleted);
        }

        public void AddDeliveryAddress(UserDeliveryAddress userDeliveryAddress)
        {
            userDeliveryAddress.TimeCreated=DateTime.Now;
            userDeliveryAddress.Guid = Guid.NewGuid();
            userDeliveryAddress.IsDeleted = false;
            if (userDeliveryAddress.IsDefault)
            {
                CancleDefaultDeliveryAddress();
            }
            _userDeliveryAddressRepository.Insert(userDeliveryAddress);
        }

        public void UpdateDeliveryAddress(UserDeliveryAddress userDeliveryAddress)
        {
            var entity= _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == userDeliveryAddress.Guid && !q.IsDeleted);
            if (entity == null) throw new CommonException("此收货地址不存在");
            entity.IsDefault = userDeliveryAddress.IsDefault;
            if (entity.IsDefault)
            {
                CancleDefaultDeliveryAddress();
            }
            entity.Address = userDeliveryAddress.Address;
            entity.Phone = userDeliveryAddress.Phone;
            entity.ReceiverName = userDeliveryAddress.ReceiverName;
            //TODO:省市区邮编待改
            _userDeliveryAddressRepository.Update(entity);
        }

        /// <summary>
        /// 设置默认收货地址
        /// </summary>
        /// <param name="addressGuid"></param>
        public void SetDefaultDeliveryAddress(Guid addressGuid)
        {
           
            var entity = _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == addressGuid && !q.IsDeleted);
            if (entity == null) throw new CommonException("此收货地址不存在");
            CancleDefaultDeliveryAddress();
            entity.IsDefault = true;
            _userDeliveryAddressRepository.Update(entity);
        }


        private void CancleDefaultDeliveryAddress()
        {
            var entity = _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.IsDefault && !q.IsDeleted);
            if (entity == null) return;
            entity.IsDefault = false;
            _userDeliveryAddressRepository.Update(entity);
        }

        public void DeleteDeliveryAddress(Guid addressGuid)
        {
            var entity = _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == addressGuid && !q.IsDeleted);
            if(entity==null) throw new CommonException("此收货地址不存在");
            entity.IsDeleted = true;
            _userDeliveryAddressRepository.Update(entity);
        }
    }
}
