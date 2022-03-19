using System.ComponentModel.DataAnnotations;

namespace Phoenix.Server.Web.Areas.Admin.Models.Common
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}