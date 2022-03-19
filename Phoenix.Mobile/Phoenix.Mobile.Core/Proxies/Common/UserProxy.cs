using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Core;
using Phoenix.Framework.Core;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IUserProxy
    {
        Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd);
        Task<CrudResult> ForgotPassword(string phone, string newPwd);
    }

    public class UserProxy : BaseProxy, IUserProxy
    {
        public async Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd)
        {
            try
            {
                var api = RestService.For<IUserApi>(GetHttpClient());
                var result = await api.ChangePassword(phone, oldPwd, newPwd);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<CrudResult> ForgotPassword(string phone, string newPwd)
        {
            try
            {
                var api = RestService.For<IUserApi>(GetHttpClient());
                var result = await api.ForgotPassword(phone, newPwd);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface IUserApi
        {
            [Post("/user/changepwd")]
            [Headers("Authorization: Bearer")]
            Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd);

            [Post("/user/forgotpwd")]
            Task<CrudResult> ForgotPassword(string phone, string newPwd);
        }
    }
}
