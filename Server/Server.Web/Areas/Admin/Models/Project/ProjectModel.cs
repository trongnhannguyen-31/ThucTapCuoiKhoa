using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Falcon.Web.Mvc.HtmlExtensions;

namespace SpotCheck.Sub.Web.Areas.Admin.Models.Project
{
    public class ProjectModel : BaseFalconEntityModel
    {
        public ProjectModel()
        {
            StartAt = DateTimeOffset.Now.ToString("dd/MM/yyyy HH:mm:ss");
            EndAt = DateTimeOffset.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Active = true;
        }

        [Required(ErrorMessage = "Tên chương trình không được để trống!")]
        public string Name { get; set; }
        [AllowHtml]
        public string Note { get; set; }
        public int? ImageRecordId { get; set; }
        public string ImageUrl { get; set; }
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public bool Active { get; set; }
        public string MailTo { get; set; }
        /// <summary>
        /// Campaign optSettings
        /// </summary>
        public string Settings { get; set; }
        //[Required(ErrorMessage = "Vui lòng chọn nhà phân phối!")]
        //public int ZoneId { get; set; }
        //public string ZoneName { get; set; }
    }
}