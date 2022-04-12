﻿using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.User;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IUserService
    {
        Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd);
        Task<CrudResult> ForgotPassword(string phone, string newPwd);
        Task<CrudResult> CreateUser(UserRequest request);
    }

    public class UserService : IUserService
    {
        private readonly IUserProxy _userProxy;
        public UserService(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        public Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd)
        {
            return _userProxy.ChangePassword(phone, oldPwd, newPwd);
        }

        public Task<CrudResult> ForgotPassword(string phone, string newPwd)
        {
            return _userProxy.ForgotPassword(phone, newPwd);
        }

        public Task<CrudResult> CreateUser(UserRequest request)
        {
            return _userProxy.CreateUser(request);
        }
    }
}
