using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace YoYo.Cms.UserManagerment.UserAnalysis.Dtos
{
    /// <summary>
    /// 修改用户的密码
    /// </summary>
    public class ChangeUserPasswordDto
    {
        /// <summary>
        /// 当前密码
        /// </summary>
        [Required]
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}