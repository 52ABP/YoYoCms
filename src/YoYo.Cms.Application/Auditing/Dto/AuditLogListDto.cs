using System;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;

namespace YoYo.Cms.Auditing.Dto
{
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogListDto : EntityDto<long>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 模拟租户Id
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }
        /// <summary>
        /// 模拟用户Id
        /// </summary>
        public long? ImpersonatorUserId { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public int ExecutionDuration { get; set; }
        /// <summary>
        /// 客户端ip地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 客户端
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }

        public string CustomData { get; set; }
    }
}