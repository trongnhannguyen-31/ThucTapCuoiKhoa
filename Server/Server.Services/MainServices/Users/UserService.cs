using Falcon.Web.Core.Security;
using Falcon.Core.Domain.Users;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Auth;
using Falcon.Services.Users;
using System.Linq;
using Falcon.Web.Core.Auth;
using Phoenix.Shared.Common;
using System.Threading.Tasks;
using Phoenix.Shared.User;
using System.Data.Entity;
using Falcon.Web.Core.Helpers;
using System;
using Phoenix.Shared.Core;

namespace Phoenix.Server.Services.MainServices.Users
{
    public interface IUserService
    {
        User GetUserById(int id);

        Task<BaseResponse<UserDto>> GetAllUsers(UserRequest request);

        ////
        Task<CrudResult> CreateUser(UserRequest request);

    }

    public class UserService : IUserService
    {
        private const int SaltLenght = 6;
        private readonly DataContext _dataContext;
        private readonly IEncryptionService _encryptionService;
        private readonly UserAuthService _userAuthService;
        public UserService(DataContext dataContext, IEncryptionService encryptionService, UserAuthService userAuthService)
        {
            _dataContext = dataContext;
            _encryptionService = encryptionService;
            _userAuthService = userAuthService;
        }
        public User GetUserById(int id) => _dataContext.Users.Find(id);
        //public LoginResponse ValidateUser(LoginRequest request)
        //{
        //    var result = new LoginResponse();
        //    var loginResult = ValidateFalconUser(request.UserName, request.Password);
        //    if (loginResult == FalconUserLoginResults.Successful)
        //    {
        //        var user = _dataContext.FalconUsers.FirstOrDefault(r => r.Username == request.UserName);
        //        if (user != null)
        //        {
        //            var employee = _dataContext.WMS_Employees.FirstOrDefault(r => r.FalconUserId == user.Id);
        //            if (employee == null || !employee.Active || employee.Deleted)
        //            {
        //                result.LoginResult = LoginResult.UserNotFound;
        //                return result;
        //            }
        //            result.EmployeeId = employee.Id;
        //            result.Name = employee.FullName;
        //            result.IsSuccess = true;
        //        }
        //    }
        //    return result;
        //}
        private FalconUserLoginResults ValidateFalconUser(string username, string password)
        {
            return FalconUserLoginResults.Successful;
        }

        // Get List User
        public async Task<BaseResponse<UserDto>> GetAllUsers(UserRequest request)
        {
            var result = new BaseResponse<UserDto>();
            try
            {
                //setup query
                var query = _dataContext.Users.AsQueryable();
                //filter
                if (!string.IsNullOrEmpty(request.UserName))
                {
                    query = query.Where(d => d.UserName.Contains(request.UserName));
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<UserDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<CrudResult> CreateUser(UserRequest request)
        {
            var User = new User();
            User.UserName = request.UserName;
            User.DisplayName = request.DisplayName;
            var salt = _encryptionService.CreateSaltKey(SaltLenght);
            User.Salt = salt;
            User.Password = _encryptionService.CreatePasswordHash(request.Password, salt);
            // User.Salt = request.Salt;
            User.Active = true;
            User.Roles = "Admin";
            User.Deleted = request.Deleted;

            _dataContext.Users.Add(User);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}