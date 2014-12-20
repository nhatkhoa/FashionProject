using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu tối thiểu 4 kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu Mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập Lại")]
        [Compare("NewPassword", ErrorMessage = "2 Mật khẩu không trùng nhau, vui lòng kiểm tra lại.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu Hiện Tại")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu tối thiểu 4 kí tự", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu Mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập Lại")]
        [Compare("NewPassword", ErrorMessage = "2 Mật khẩu không trùng nhau, vui lòng kiểm tra lại.")]
        public string ConfirmPassword { get; set; }
    }



}