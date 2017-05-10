﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;
using MMY.Services.ServiceModel;

namespace MMY.Services.ServicesImpl
{
    public class AuthorityImpl:IAuthority
    {
        private IRepository<AuthorityFunction> _AuthorityRepository;
        public AuthorityImpl(IRepository<AuthorityFunction> authorityRepository)
        {
            _AuthorityRepository = authorityRepository;
        }
        public IList<UserMenuModel> GetUserMenu(Guid roleGuid,Guid parentGuid)
        {
            IList<UserMenuModel> functionModels = new List<UserMenuModel>();
            var functions = GetRoleFunctionsByParentGuid(roleGuid, parentGuid);
            if (functions.Count > 0)
            {
                foreach (var item in functions)
                {
                    var model = UserMenuModel.CopyFrom(item);
                    if (model.FunctionType.Equals(FunctionType.Parent.ToString()))
                        model.ChildrenModels = GetUserMenu(roleGuid, item.FunctionGuid);
                    functionModels.Add(model);
                }
            }
            return functionModels;
        }

        /// <summary>
        /// 某角色的所有权限
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        private IList<AuthorityFunction> GetRoleFunctionsByParentGuid(Guid roleGuid, Guid parentGuid)
        {
            //TODO：暂时取出系统所有权限
            var resultList = _AuthorityRepository.Table.Where(q => q.Enable && q.ParentGuid==parentGuid).OrderBy(q=>q.DisplayOrder).ToList();
            return resultList;
        }
    }
}
