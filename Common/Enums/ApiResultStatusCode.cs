using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 0,

        [Display(Name = "خطایی در سرور رخ داده است")]
        InternalServerError = 1,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 2,

        [Display(Name = "یافت نشد")]
        NotFound = 3,

        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 4,

        [Display(Name = "خطای احراز هویت")]
        Unauthorized = 5,

        [Display(Name = "خطای عدم دسترسی")]
        Forbidden = 6,

        [Display(Name = "نیازمند بروز رسانی توکن")]
        NeedRefreshToken = 7,

        [Display(Name = "کاربر غیر فعال")]
        InActive = 8
    }
}
