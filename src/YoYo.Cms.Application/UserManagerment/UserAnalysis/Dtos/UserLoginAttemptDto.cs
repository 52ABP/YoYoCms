using System;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace YoYo.Cms.UserManagerment.UserAnalysis.Dtos
{
    /// <summary>
    /// 用户尝试登录记录
    /// </summary>
    [AutoMap(typeof(UserLoginAttempt))]
    public class UserLoginAttemptDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// 用户名或者邮箱
        /// </summary>
        public string UserNameOrEmail { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string ClientIpAddress { get; set; }

        public string ClientName { get; set; }

        public string BrowserInfo { get; set; }

        public string Result { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
