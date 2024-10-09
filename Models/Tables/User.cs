using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TICKETBOX.Models.Tables;

public partial class User
{
    public int UserId { get; set; }
    [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
    [MinLength(3, ErrorMessage = "Tên đăng nhập phải có ít nhất 3 ký tự!")]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Tên đăng nhập không được chứa ký tự đặc biệt!")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Mật khẩu không được để trống.")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!")]
    public string? UserPassword { get; set; }
    [Required(ErrorMessage = "Email không được để trống.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
    public string? UserEmail { get; set; }

    public string? FullName { get; set; }

    public string? UserPhoneNumber { get; set; }

    public DateTime? DoB { get; set; }

    public string? Sex { get; set; }

    public string? UserAddress { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
