using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay24.Data.Dtos.Site.Admin
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        [EmailAddress(ErrorMessage = "فرمت آدرس ایمیل صحیح نیست")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "به خاطر سپردن")]
        public bool IsRemember { get; set; }
    }
}
