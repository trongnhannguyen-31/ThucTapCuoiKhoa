using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Web.Core.Helpers;
using Falcon.Web.Mvc.PageList;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices.System.Setting.Models;
using Setting = Falcon.Web.Core.Settings.Setting;

namespace Phoenix.Server.Services.MainServices.System.Setting
{
    public class SettingService
    {
        private readonly DataContext _dataContext;

        public SettingService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateSetting(Falcon.Web.Core.Settings.Setting setting)
        {
            //todo validate before insert
            _dataContext.Settings.Add(setting);
            await _dataContext.SaveChangesAsync();
            //await _localApisCacheManagerService.ClearCampusCache(CacheResetTypes.Setting);
        }

        public async Task<IPageList<SettingModel>> SearchSettings(SearchSettingRequest request)
        {
            var query = _dataContext.Settings.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(d => d.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Value))
            {
                query = query.Where(d => d.Value.Contains(request.Value));
            }
            query = query.OrderByDescending(d => d.Id);
            var settingss = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
            return new PageList<SettingModel>(settingss.MapTo<SettingModel>(), request.Page, request.PageSize);
        }

        public SettingModel GetSettingsById(int id)
        {
            var setting = _dataContext.Settings.Find(id);
            return setting?.MapTo<SettingModel>();
        }

        public async Task InsertSettings(SettingModel model)
        {
            _dataContext.Settings.Add(new Falcon.Web.Core.Settings.Setting()
            {
                Value = model.Value,
                Name = model.Name,
            });
            await _dataContext.SaveChangesAsync();
            //await _localApisCacheManagerService.ClearCampusCache(CacheResetTypes.Setting);
        }

        public async Task UpdateSettings(SettingModel model)
        {
            var settingsUpdate = _dataContext.Settings.Find(model.Id);
            if (settingsUpdate == null) return;

            settingsUpdate.Name = model.Name;
            settingsUpdate.Value = model.Value;

            await _dataContext.SaveChangesAsync();
            //await _localApisCacheManagerService.ClearCampusCache(CacheResetTypes.Setting);
        }

        public async Task DeleteSettings(int id)
        {
            var settings = _dataContext.Settings.Find(id);
            if (settings == null) return;
            _dataContext.Settings.Remove(settings);

            await _dataContext.SaveChangesAsync();
            //await _localApisCacheManagerService.ClearCampusCache(CacheResetTypes.Setting);
        }
    }
}
