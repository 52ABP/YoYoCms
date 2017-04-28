
// 项目展示地址:"http://www.ddxc.org/"
// 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-04-24T21:56:53. All Rights Reserved.
//<生成时间>2017-04-24T21:56:53</生成时间>
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace YoYo.Cms.UserManagerment.Users.Dtos
{
    /// <summary>
    /// 用户信息编辑用Dto
    /// </summary>
    [AutoMap(typeof(User))]
    public class UserEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "邮箱格式不合法")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Authorizationsourcename.It'ssettoexternalauthenticationsourcenameifcreatedbyanexternalsource.Default:null.
        /// </summary>
        [DisplayName("Authorizationsourcename.It'ssettoexternalauthenticationsourcenameifcreatedbyanexternalsource.Default:null.")]
        public string AuthenticationSource { get; set; }

        /// <summary>
        /// Returnfullname(NameSurname)
        /// </summary>
        [DisplayName("Returnfullname(NameSurname)")]
        public string FullName { get; set; }

        /// <summary>
        /// Passwordoftheuser.
        /// </summary>
        [DisplayName("Passwordoftheuser.")]
        public string Password { get; set; }

        /// <summary>
        /// Istheconfirmed.
        /// </summary>
        [DisplayName("Istheconfirmed.")]
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Confirmationcodeforemail.
        /// </summary>
        [DisplayName("Confirmationcodeforemail.")]
        public string EmailConfirmationCode { get; set; }

        /// <summary>
        /// Resetcodeforpassword.It'snotvalidifit'snull.It'sforoneusageandmustbesettonullafterreset.
        /// </summary>
        [DisplayName("Resetcodeforpassword.It'snotvalidifit'snull.It'sforoneusageandmustbesettonullafterreset.")]
        public string PasswordResetCode { get; set; }

        /// <summary>
        /// Lockoutenddate.
        /// </summary>
        [DisplayName("Lockoutenddate.")]
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Getsorsetstheaccessfailedcount.
        /// </summary>
        [DisplayName("Getsorsetstheaccessfailedcount.")]
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Getsorsetsthelockoutenabled.
        /// </summary>
        [DisplayName("Getsorsetsthelockoutenabled.")]
        public bool IsLockoutEnabled { get; set; }

        /// <summary>
        /// Getsorsetsthephonenumber.
        /// </summary>
        [DisplayName("Getsorsetsthephonenumber.")]
        [RegularExpression(@"(13\d|14[57]|15[^4,\D]|17[678]|18\d)\d{8}|170[059]\d{7}", ErrorMessage = "手机号码只能输入11位纯数字")]
        public string PhoneNumber { get; set; }

        public bool ShouldResetPassword { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Istheconfirmed.
        /// </summary>
        [DisplayName("Istheconfirmed.")]
        public bool IsPhoneNumberConfirmed { get; set; }

        public bool IsActive { get; set; }

        public bool ShouldChangePasswordOnNextLogin { get; set; }

    }
}
