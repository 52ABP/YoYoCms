
// 项目展示地址:"http://www.ddxc.org/"
// 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
//<Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-04-24T21:56:54. All Rights Reserved.
//<生成时间>2017-04-24T21:56:54</生成时间>
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace YoYo.Cms.UserManagerment.Users.Dtos
{
    /// <summary>
    /// 用户信息列表Dto
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>
    {
        public string EmailAddress { get; set; }
        /// <summary>
        /// Istheconfirmed.
        /// </summary>
        [DisplayName("Istheconfirmed.")]
        public bool IsEmailConfirmed { get; set; }
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
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Istheconfirmed.
        /// </summary>
        [DisplayName("Istheconfirmed.")]
        public bool IsPhoneNumberConfirmed { get; set; }
        /// <summary>
        /// Isthisuseractive?Ifasuserisnotactive,he/shecannotusetheapplication.
        /// </summary>
        [DisplayName("Isthisuseractive?Ifasuserisnotactive,he/shecannotusetheapplication.")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Username.Usernamemustbeuniqueforit'stenant.
        /// </summary>
        [DisplayName("Username.Usernamemustbeuniqueforit'stenant.")]
        public string UserName { get; set; }
        /// <summary>
        /// Thelasttimethisuserenteredtothesystem.
        /// </summary>
        [DisplayName("Thelasttimethisuserenteredtothesystem.")]
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
