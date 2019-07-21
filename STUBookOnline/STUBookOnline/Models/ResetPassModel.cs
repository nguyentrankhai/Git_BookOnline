using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STUBookOnline.Models
{
    public class ResetPassModel
    {
        [Required(ErrorMessage ="Tên đăng nhập không được để trống", AllowEmptyStrings =false)]
        public string userID { get; set; }

        [Required(ErrorMessage = "Email không được để trống", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        [Required(ErrorMessage = "Mật khẩu không được để trống", AllowEmptyStrings = false)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu nhập lại không trùng khớp với Mật khẩu", AllowEmptyStrings = false)]
        public string RePassword { get; set; }
    }
}