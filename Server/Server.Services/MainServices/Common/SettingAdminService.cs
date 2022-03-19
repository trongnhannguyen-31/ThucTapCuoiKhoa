using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Phoenix.Server.Services.Database;
using Falcon.Web.Core.Settings;

namespace Phoenix.Server.Services.MainServices.Common
{
    public class SettingAdminService
    {
        private readonly DataContext _dataContext;
        public SettingAdminService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Setting GetById(int Id) => _dataContext.Settings.Find(Id);
        public Setting GetSettingByValue(string value)
        {
            var setting = _dataContext.Settings.FirstOrDefault(x => x.Name.Contains(value));
            return setting;
        }
        public void Update(Setting entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var data = GetById(entity.Id);
            data.Value = entity.Value;
            _dataContext.Settings.AddOrUpdate(data);
            _dataContext.SaveChanges();
        }
        
    }
}
