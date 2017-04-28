using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YoYo.Cms.UserManagerment.UserAnalysis.Dtos;

namespace YoYo.Cms.UserManagerment.UserAnalysis
{
    /// <summary>
    /// 用户行为分析服务
    /// </summary>
    public interface IUserAnalysisAppService : IApplicationService
    {
        /// <summary>
        /// 获取最近10条用户登录记录
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttemptsTenRecordsAsync();

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns></returns>
        Task ChangeUserPasswordAsync(ChangeUserPasswordDto input);

        /// <summary>
        /// 修改用户资料Dto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangeUserProfileInfoAsync(ChangeUserProfileDto input);
    }
}
