using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Web.Core.Auth;
using Falcon.Web.Core.Caching;
using Falcon.Web.Core.Helpers;
using Falcon.Web.Mvc.PageList;
using Phoenix.Server.Services.Constants;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.Helper;
using Phoenix.Server.Services.MainServices.Auth.Models;

namespace Phoenix.Server.Services.MainServices.Auth
{
    public class RoleService
    {

        private readonly DataContext _masterContext;
        private readonly ICacheManager _cacheManager;

        public RoleService(DataContext masterContext
            , ICacheManager cacheManager)
        {
            _masterContext = masterContext;
            _cacheManager = cacheManager;
        }

        public async Task<int> Create(Role role)
        {
            _masterContext.Roles.Add(role);
            _cacheManager.Remove(UserAuthService.AllRolesKey);
            return await _masterContext.SaveChangesAsync();
        }
       
        public Role GetRoleBySystemName(string systemName)
        {
            var role = _masterContext.Roles.AsNoTracking().FirstOrDefault(d => d.SystemName == systemName);
            return role;
        }

        public async Task<List<Role>> GetRoleByUserId(int id)
        {
            var user = await _masterContext.Users.FindAsync(id);
            if (user == null) return null;
            var roleUser = user.Roles.SplitIds();
            var role = _masterContext.Roles.Where(d => roleUser.Contains(d.SystemName));
            return role.ToList();
        }
        public async Task UpdateRole(Role role)
        {
            var p = _masterContext.Roles.FirstOrDefault(d => d.SystemName == role.SystemName);
            if (p == null) return;

            p.Display = role.Display;

            await _masterContext.SaveChangesAsync();
        }

        public async Task DeleteRole(string systemName)
        {
            var role = _masterContext.Roles.FirstOrDefault(d => d.SystemName == systemName);
            if (role == null) return;

            _masterContext.Roles.Remove(role);
            await _masterContext.SaveChangesAsync();
        }

        public async Task<List<RoleModel>> GetAllRoles()
        {
            var role = await _masterContext.Roles.AsNoTracking().ToListAsync();
            var roleModel = role.Select(d => d.MapTo<RoleModel>()).ToList();
            foreach (var model in roleModel)
            {
                var modelExculde = roleModel.Where(d => d.SystemName != model.SystemName).ToList();
                while (modelExculde.Any(d => d.Id == model.Id))
                {
                    model.Id++;
                }
            }

            return roleModel;
        }

        public async Task<IPageList<RoleModel>> SearchRole(SearchRoleRequest request)
        {
            var query = _masterContext.Roles.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(request.Display))
            {
                query = query.Where(d => d.Display.Contains(request.Display));
            }
            query = query.OrderByDescending(d => d.SystemName);
            var role = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
            return new PageList<RoleModel>(role.MapTo<RoleModel>(), request.Page, request.PageSize, query.Count());
        }

        public async Task UpdateRole(RoleModel role)
        {
            var p = _masterContext.Roles.FirstOrDefault(d => d.SystemName == role.SystemName);
            if (p == null) return;

            p.Display = role.Display;
            p.Active = role.Active;

            await _masterContext.SaveChangesAsync();
        }

        public async Task<List<RoleClaimModel>> GetRoleClaim()
        {
            var role = await _masterContext.Roles.AsNoTracking().ToListAsync();
            var res = new List<RoleClaimModel>();
            role.ForEach(d =>
            {
                res.Add(new RoleClaimModel()
                {
                    SystemName = d.SystemName,
                    Display = d.Display,
                    Claim = GetAllClaimByRole(d.SystemName)
                });
            });
            return res;
        }

        private List<ClaimModel> GetAllClaimByRole(string role)
        {
            var res = new List<ClaimModel>();
            //tất cả claim
            var claim = ServerPermissions.DefaultClaims.Select(d => d.Value).Distinct();
            //claim của model default
            //var claimRole = Roles.DefaultClaims.Where(x => x.Key == model).Select(x => x.Value).ToList();
            //claim được thêm mới vào model
            var claimOrther = new string[] { };

            //các quyền được thêm mới không phải default
            var claimRoleOrther = _masterContext.Roles.FirstOrDefault(d => d.SystemName == role);
            if (claimRoleOrther != null)
                claimOrther = claimRoleOrther.Permissons.SplitIds();

            foreach (var value in claim)
            {
                if (claimOrther.Contains(value))
                {
                    res.Add(new ClaimModel()
                    {
                        SystemName = value,
                        Name = TranslateClaimVn(value),
                        Checked = true
                    });
                    continue;
                }
                res.Add(new ClaimModel()
                {
                    SystemName = value,
                    Name = TranslateClaimVn(value),
                });

            }
            return res;
        }

        public async Task UpdateClaimByRole(RoleClaimUpdateModel model)
        {
            var query = model.RoleClaims.GroupBy(d => d.RoleSystemName).ToList();
            var roles = _masterContext.Roles.ToList();
            roles.ForEach(v =>
            {
                var r = query.FirstOrDefault(d => d.Key == v.SystemName);
                if (r == null)
                {
                    v.Permissons = string.Empty;
                    return;
                }
                v.Permissons = string.Join(";", r.Select(f => f.ClaimSystemName));
            });
            await _masterContext.SaveChangesAsync();
            //await _localApisCacheManagerService.ClearCampusCache(CacheResetTypes.Roles);
        }

        public bool CheckRoleSystemNameExist(string newSystemName, string oldSystemName = "")
        {
            var role = _masterContext.Roles.Where(d => d.SystemName == newSystemName);
            if (!string.IsNullOrEmpty(oldSystemName))
                role = role.Where(d => d.SystemName != oldSystemName);
            return role.Any();
        }

        public string TranslateClaimVn(string claim)
        {
            switch (claim)
            {
                case ServerPermissions.AccessAdminPanel:
                    return "Truy cập trang quản trị";
                case ServerPermissions.ManageFalconUsers:
                    return "Quản lý User";
                default:
                        return claim;
            }
        }
    }
}


