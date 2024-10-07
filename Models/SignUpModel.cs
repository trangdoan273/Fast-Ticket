using System.ComponentModel.DataAnnotations;

namespace TICKETBOX.Models
{
    public partial class SignUpModel()
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có ít nhất 3 ký tự!")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Tên đăng nhập không được chứa ký tự đặc biệt!")]
        
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!")]
        public string UserPassword { get; set; }
    }
}