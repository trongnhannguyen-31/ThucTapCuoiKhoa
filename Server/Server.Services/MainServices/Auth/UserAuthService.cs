using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Web.Api.Security;
using Falcon.Web.Core.Auth;

using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.Framework;
using Phoenix.Server.Services.Helper;
using Phoenix.Server.Services.Settings;
using Phoenix.Shared.Auth;
using Phoenix.Shared.Common;
using Falcon.Web.Core.Caching;
using Falcon.Web.Core.Security;
using Phoenix.Shared.Core;

namespace Phoenix.Server.Services.MainServices.Auth
{
    public class UserAuthService
    {
        public const string AllRolesKey = "UserAuthService.AllRoles";
        private readonly IEncryptionService _encryptionService;
        private readonly ICacheManager _cacheManager;
        private readonly SettingService _settingService;
        private readonly ITokenValidation _tokenValidation;
        private const byte _saltLen = 6;
        private readonly DataContext _dataContext;
        public UserAuthService(IEncryptionService encryptionService
            , ICacheManager cacheManager
            , SettingService settingService
            , ITokenValidation tokenValidation,
            DataContext dataContext)
        {
            _encryptionService = encryptionService;
            _cacheManager = cacheManager;
            _settingService = settingService;
            _tokenValidation = tokenValidation;
            _dataContext = dataContext;
        }

        /// <summary>
        /// for api
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TokenResponse> LoginAsync(TokenRequest request)
        {
            var result = new TokenResponse();
            //get user

            var user = await Validate(request.UserName, request.Password);
            if (user != null && user.UserId > 0)
            {
                try
                {
                    var token = TokenAuthHelper.CreateToken(new TokenData()
                    {
                        LifeTime = 86400 * 30,
                        UserId = user.UserId
                    });

                    //result.ExpiresIn = 43200;//1 month                   
                    //var secret = _tokenValidation.GetEncryptKey();

                    //result.AccessToken = TokenHelper.CreateToken(secret,
                    //    result.ExpiresIn, new Ticket()
                    //    {
                    //        UserId = customer.Id,
                    //        Claims = claims
                    //    });
                    //customer info
                    var userInfo = await _dataContext.Users.FindAsync(user.UserId);

                    result.AccessToken = token;
                    result.UserId = user.UserId;
                    result.UserName = userInfo.UserName;
                    result.FullName = userInfo.DisplayName;
                    result.ExpiresIn = 86400 * 30;
                    //result.RefreshToken = refreshToken = $"{id}.{Guid.NewGuid()}";
                }
                catch (Exception ex)
                {
                    throw new UnauthorizedAccessException("get token failed");
                }
            }
            return result;
        }
        public async Task<TokenResponse> Refresh(string refreshToken)
        {
            var result = new TokenResponse();

            var customerId = int.Parse(refreshToken.Split('.').First());
            var customer = _dataContext.Users.Find(customerId);
            if (customer == null || !customer.Active ) return result;
            var securitySettings = _settingService.LoadSetting<SecuritySettings>();
            result.ExpiresIn = securitySettings.TokenLifeTime * 30;
            result.AccessToken = TokenHelper.CreateToken(_tokenValidation.GetEncryptKey(),
                result.ExpiresIn, new Ticket()
                {
                    UserId = customer.Id,
                    Claims = customer.UserClaims.ToDictionary(x => x.ClaimName, y => y.ClaimValue)
                });
            //result.RefreshToken = customer.RefreshToken = $"{customer.Id}.{Guid.NewGuid()}";
            await _dataContext.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Validate user for web
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Ticket> Validate(string userName, string password)
        {
            var result = new Ticket();
            var user = await _dataContext.Users.AsNoTracking().FirstOrDefaultAsync(aa => aa.UserName == userName && aa.Active && !aa.Deleted);
            //check password
            if (user == null
                || user.Password != _encryptionService.CreatePasswordHash(password, user.Salt)) return result;

            //find user claims
            result.UserId = user.Id;
            //todo: role for web later
            result.Claims = user.UserClaims.ToDictionary(x => x.ClaimName, x => x.ClaimValue);
            if (!string.IsNullOrEmpty(user.Roles))
            {
                var roles = user.Roles.Split(SharedValues.Delimiter);
                foreach (var role in roles)
                {
                    //default claims of the role
                    var systemRole = await GetRole(role);
                    if (systemRole == null) throw new Exception("Invalid role" + role);
                    var rolePermissions = systemRole.Permissons.Split(SharedValues.Delimiter);
                    foreach (var permission in rolePermissions)
                    {
                        if (!result.Claims.ContainsKey(permission))
                        {
                            result.Claims.Add(permission, "");
                        }
                    }
                }
            }
            return result;
        }

        public async Task<bool> ChangePassword(string userName, string password)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.UserName == userName && x.Active);
            if (user == null) return false;
            var salt = _encryptionService.CreateSaltKey(_saltLen);
            user.Salt = salt;
            user.Password = _encryptionService.CreatePasswordHash(password, salt);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<CrudResult> ChangePasswordNew(string phone, string oldPwd, string newPwd)
        {
            var ticUser = await Validate(phone, oldPwd);
            if (ticUser == null || ticUser.UserId < 1) return new CrudResult()
            {
                ErrorCode = CommonErrorStatus.KeyNotFound,
                ErrorDescription = "Mật khẩu nhập vào không đúng"
            };
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == phone && x.Active);
            if (user == null) return null;
            var salt = _encryptionService.CreateSaltKey(_saltLen);
            user.Salt = salt;
            user.Password = _encryptionService.CreatePasswordHash(newPwd, salt);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }


        public async Task<Role> GetRole(string name)
        {
            return (await GetAll()).FirstOrDefault(x => x.SystemName == name);
        }
        public async Task<Role[]> GetAll()
        {
            var settings = _settingService.LoadSetting<SystemSettings>();
            //return await _cacheManager.GetAsync(AllRolesKey, async () =>
            //        await _dataContext.Roles.AsNoTracking().Where(x => x.Active).ToArrayAsync()
            //    , settings.CacheLong);
            //return new Role[] { };
            return await _dataContext.Roles.AsNoTracking().Where(x => x.Active).ToArrayAsync();
        }

        public List<string> GetClaimByUserId(int id)
        {
            var claim = new List<string>();
            var user = _dataContext.Users.Find(id);
            if (user == null || user.Deleted)
                return claim;
            var role = user.Roles.SplitIds();

            foreach (var r in role)
            {
                var systemRole = _dataContext.Roles.AsNoTracking().FirstOrDefault(d => d.SystemName == r);
                if (systemRole == null) throw new Exception("Invalid role" + role);
                var rolePermissions = systemRole.Permissons.SplitIds();
                foreach (var permission in rolePermissions)
                {
                    claim.Add(permission);
                }
            }

            return claim.Distinct().ToList();
        }
        public List<string> GetClaimById(int id)
        {
            var claim = new List<string>();
            var user = _dataContext.Users.Find(id);
            if (user == null || user.Deleted)
                return claim;
            var role = user.Roles.SplitIds();

            foreach (var r in role)
            {
                var systemRole = _dataContext.UserClaims.AsNoTracking().FirstOrDefault(d => d.ClaimName == r && d.UserId== user.Id);
                if (systemRole == null) return claim;
                var rolePermissions = systemRole.ClaimValue.SplitIds();
                foreach (var permission in rolePermissions)
                {
                    claim.Add(permission);
                }
            }

            return claim.Distinct().ToList();
        }

        //public async Task<CrudResult> ForgotPassword(string phone, string newPass)
        //{
        //    var clearlyPhone = phone.Trim();
        //    var customerByPhone = await _dataContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Phone == clearlyPhone && x.Active && !x.Deleted);
        //    if (customerByPhone == null || !customerByPhone.UserId.HasValue) return new CrudResult() { ErrorCode = CommonErrorStatus.KeyNotFound, ErrorDescription = "Không tìm thấy thông tin khách hàng" };
        //    var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == customerByPhone.UserId.Value && x.Active && !x.Deleted);
        //    if (user == null) return new CrudResult() { ErrorCode = CommonErrorStatus.KeyNotFound, ErrorDescription = "Lỗi thông tin khách hàng" };

        //    var salt = _encryptionService.CreateSaltKey(_saltLen);
        //    user.Password = _encryptionService.CreatePasswordHash(newPass, salt);
        //    user.Salt = salt;

        //    await _dataContext.SaveChangesAsync();
        //    return new CrudResult() { IsOk = true };
        //}
    }
}
