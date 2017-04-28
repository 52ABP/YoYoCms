using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using YoYo.Cms.UserManagerment.UserAnalysis.Dtos;

namespace YoYo.Cms.UserManagerment.UserAnalysis
{
    [AbpAuthorize]
    public class UserAnalysisAppService : CmsAppServiceBase, IUserAnalysisAppService
    {
        private readonly IRepository<UserLoginAttempt, long> _userLoginAttemptRepository;

        public UserAnalysisAppService(IRepository<UserLoginAttempt, long> userLoginAttemptRepository)
        {
            _userLoginAttemptRepository = userLoginAttemptRepository;
        }
        /// <summary>
        /// 获取最近10条用户登录记录
        /// </summary>
        /// <returns></returns>
        [DisableAuditing]//不记录到审计日志
        public async Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttemptsTenRecordsAsync()
        {
            var userId = AbpSession.GetUserId();

            var loginAttempts = await _userLoginAttemptRepository.GetAll()
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreationTime)
                .Take(10)
                .ToListAsync();


            var dtos = loginAttempts.MapTo<List<UserLoginAttemptDto>>();

            return new ListResultDto<UserLoginAttemptDto>()
            {
                Items = dtos
            };
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangeUserPasswordAsync(ChangeUserPasswordDto input)
        {

            var user = await GetCurrentUserAsync();
            CheckErrors(await UserManager.ChangePasswordAsync(user.Id, input.CurrentPassword, input.NewPassword));


       
        }
        /// <summary>
        /// 修改用户的资料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangeUserProfileInfoAsync(ChangeUserProfileDto input)
        {
            var user = await GetCurrentUserAsync();

            input.MapTo(user);


            CheckErrors(await UserManager.UpdateAsync(user));


             
        }
    }
}