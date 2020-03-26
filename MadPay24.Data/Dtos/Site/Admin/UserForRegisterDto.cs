using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay24.Data.Dtos.Site.Admin
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        [EmailAddress(ErrorMessage = "فرمت آدرس ایمیل صحیح نیست")]
        public string UserName { get; set; }

        [StringLength(10, MinimumLength = 6, ErrorMessage = " {0} باید حداقل دارای 6 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تلفن")]
        public string PhoneNumber { get; set; }
    }
}
