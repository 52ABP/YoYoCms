using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YoYo.Cms.Auditing.Dto;

namespace YoYo.Cms.Auditing
{
    /// <summary>
    /// 审计日志服务
    /// </summary>
    public interface IAuditLogAppService:IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetPagedAuditLogsAsync(GetAuditLogsInput input);
    }
}