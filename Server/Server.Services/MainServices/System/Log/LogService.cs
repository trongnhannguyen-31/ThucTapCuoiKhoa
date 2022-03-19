using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Web.Core.Helpers;
using Falcon.Web.Mvc.PageList;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices.System.Log.Models;

namespace Phoenix.Server.Services.MainServices.System.Log
{
    public class LogService
    {
        private readonly DataContext _dataContext;

        public LogService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Log Master
        public async Task<IPageList<LogModel>> SearchLog(SearchLogRequest request)
        {
            var query = _dataContext.Logs.AsNoTracking()
                .Where(d => d.CreatedAt >= request.DateForm
                            && d.CreatedAt < request.DateTo);
            query = query.OrderByDescending(d => d.Id);
            var logs = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
            return new PageList<LogModel>(logs.MapTo<LogModel>(), request.Page, request.PageSize);
        }

        public LogModel GetLogById(int id)
        {
            var log = _dataContext.Logs.Find(id);
            if (log == null)
                return null;
            return log.MapTo<LogModel>();
        }

        public async Task DeleteLog(int id)
        {
            var log = _dataContext.Logs.Find(id);
            if (log == null) return;
            _dataContext.Logs.Remove(log);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAllLog()
        {
            _dataContext.Logs.RemoveRange(_dataContext.Logs);
            await _dataContext.SaveChangesAsync();
        }
        #endregion
    }
}
