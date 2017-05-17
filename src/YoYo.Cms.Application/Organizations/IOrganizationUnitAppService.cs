using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YoYo.Cms.Organizations.Dto;

namespace YoYo.Cms.Organizations
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        /// <summary>
        /// 获取组织机构（树形结构数据）
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync();

        /// <summary>
        /// 通过Id更新组织机构名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> UpdateOrganizationUnitAsync(UpdateOrganizationUnitInput input);

        /// <summary>
        /// 将用户移除组织机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task RemoveUserFromOrganizationUnitAsync(UserToOrganizationUnitInput input);

        /// <summary>
        /// 将用户移入组织机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddUserToOrganizationUnitAsync(UserToOrganizationUnitInput input);

        /// <summary>
        /// 新增组织机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> CreateOrganizationUnitAsync(CreateOrganizationUnitInput input);

        /// <summary>
        /// 将目标组织机构移入到指定组织机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> MoveOrganizationUnitAsync(MoveOrganizationUnitInput input);

        /// <summary>
        /// 删除指定组织机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteOrganizationUnitAsync(EntityDto<long> input);

        /// <summary>
        /// 用户是否已经在组织机构下面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> IsInOrganizationUnitAsync(UserToOrganizationUnitInput input);

        /// <summary>
        /// 指定组织机构下面所有用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input);
        
    }
}
