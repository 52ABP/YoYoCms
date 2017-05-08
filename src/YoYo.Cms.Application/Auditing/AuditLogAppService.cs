using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using YoYo.Cms.Auditing.Dto;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Auditing
{
    [DisableAuditing]
    
    public class AuditLogAppService:CmsAppServiceBase,IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<User, long> _userRepository;

        public AuditLogAppService(IRepository<AuditLog, long> auditLogRepository, IRepository<User, long> userRepository)
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
        }

        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input)
        {
            throw new System.NotImplementedException();
        }







    }
}