using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.UserManagerment.UserAnalysis.Dtos
{
    /// <summary>
    /// 修改用户资料信息dto
    /// </summary>
    [AutoMap(typeof(User))]
    public class ChangeUserProfileDto
    {


        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 下次登录是否需要修改密码
        /// </summary>
        public  bool ShouldChangePasswordOnNextLogin { get; set; }


    }
}