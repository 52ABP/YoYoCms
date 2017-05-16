using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using YoYo.Cms.Auditing.Dto;
using YoYo.Cms.UserManagerment.Users;
using Abp.Extensions;

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

      

        public async Task<PagedResultDto<AuditLogListDto>> GetPagedAuditLogsAsync(GetAuditLogsInput input)
        {
            var query = CreateAuditLogAndUsersQuery(input);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();




            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 创建审计日志用户的查询服务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IQueryable<AuditLogAndUser> CreateAuditLogAndUsersQuery(GetAuditLogsInput input)
        {
            var query = from auditLog in _auditLogRepository.GetAll()
                        join user in _userRepository.GetAll() on auditLog.UserId equals user.Id into userJoin
                        from joinedUser in userJoin.DefaultIfEmpty()
                        where auditLog.ExecutionTime >= input.StartDate && auditLog.ExecutionTime <= input.EndDate
                        select new AuditLogAndUser { AuditLogInfo = auditLog, UserInfo = joinedUser };

            query = query
                .WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.UserInfo.UserName.Contains(input.UserName))
                .WhereIf(!input.ServiceName.IsNullOrWhiteSpace(), item => item.AuditLogInfo.ServiceName.Contains(input.ServiceName))
                .WhereIf(!input.MethodName.IsNullOrWhiteSpace(), item => item.AuditLogInfo.MethodName.Contains(input.MethodName))
                .WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(), item => item.AuditLogInfo.BrowserInfo.Contains(input.BrowserInfo))
                .WhereIf(input.MinExecutionDuration.HasValue && input.MinExecutionDuration > 0, item => item.AuditLogInfo.ExecutionDuration >= input.MinExecutionDuration.Value)
                .WhereIf(input.MaxExecutionDuration.HasValue && input.MaxExecutionDuration < int.MaxValue, item => item.AuditLogInfo.ExecutionDuration <= input.MaxExecutionDuration.Value)
                .WhereIf(input.HasException == true, item => item.AuditLogInfo.Exception != null && item.AuditLogInfo.Exception != "")
                .WhereIf(input.HasException == false, item => item.AuditLogInfo.Exception == null || item.AuditLogInfo.Exception == "");
            return query;
        }


    }
}