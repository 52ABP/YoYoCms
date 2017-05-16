using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using YoYo.Cms.Dto;

namespace YoYo.Cms.Auditing.Dto
{/// <summary>
/// 审计日志接收DTO
/// </summary>
    public class GetAuditLogsInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 是否包含异常
        /// </summary>
        public bool? HasException { get; set; }

        public int? MinExecutionDuration { get; set; }

        public int? MaxExecutionDuration { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ExecutionTime DESC";
            }

            if (Sorting.IndexOf("UserName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "UserInfo." + Sorting;
            }
            else
            {
                Sorting = "AuditLogInfo." + Sorting;
            }
        }
    }
}