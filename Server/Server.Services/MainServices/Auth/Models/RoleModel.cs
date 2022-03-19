using System.ComponentModel.DataAnnotations;

namespace Phoenix.Server.Services.MainServices.Auth.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            Active = true;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên vai trò")]
        public string Display { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên hệ thống")]
        public string SystemName { get; set; }
        public bool Active { get; set; }
        public bool ContinueEditing { get; set; }
    }
}
