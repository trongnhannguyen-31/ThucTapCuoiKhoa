using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Phoenix.Server.Services.MainServices.System.Setting.Models
{
    public class SettingModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên setting")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập giá trị setting")]
        [AllowHtml]
        public string Value { get; set; }

        public bool ContinueEditing { get; set; }
    }
}
