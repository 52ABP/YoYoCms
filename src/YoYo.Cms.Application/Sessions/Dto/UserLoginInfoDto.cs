using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
