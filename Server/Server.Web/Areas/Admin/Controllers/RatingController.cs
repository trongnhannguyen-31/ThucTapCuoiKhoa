using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Rating;
using Phoenix.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class RatingController : BaseController
    {
        // GET: Admin/Rating
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // List Rating
        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, RatingModel model)
        {
            var ratings = await _ratingService.GetAllRatings(new RatingRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Customer_Name = model.Customer_Name,
            });

            var gridModel = new DataSourceResult
            {
                Data = ratings.Data,
                Total = ratings.DataCount
            };
            return Json(gridModel);
        }

        /*// Delete Rating
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var project = _ratingService.GetRatingsById(id);
            if (project == null)
                //No email account found with the specified id
                return RedirectToAction("List");

            await _ratingService.Delete(project.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("List");
        }*/
    }
}