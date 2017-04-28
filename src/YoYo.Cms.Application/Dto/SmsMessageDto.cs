using System.ComponentModel.DataAnnotations;

namespace YoYo.Cms.Dto
{
    /// <summary>
    /// 接收验证码手机号
    /// </summary>
    public class SmsMessageDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号为空，请确认")]
        [RegularExpression(@"^1(3|4|5|7|8)[0-9]\d{8}$", ErrorMessage = "你的手机号码格式不正确，请重新输入！")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "请输入图形验证码")]
        public string VerificationCode { get; set; }
    }
}
