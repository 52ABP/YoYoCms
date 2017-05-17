using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YoYo.Cms.Authorization.Roles.Dto;

namespace YoYo.Cms.Authorization.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 更新指定角色的授权信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);

        /// <summary>
        /// 根据权限取用户角色 默认全量数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);

        /// <summary>
        /// 取指定角色信息（包含授权列表、拥有权限列表）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleForEditOutput> GetRoleForEditAsync(NullableIdDto input);

        /// <summary>
        /// 更新或新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateRoleAsync(CreateOrUpdateRoleInput input);


        /// <summary>
        /// 删除指定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRole(EntityDto input);
    }
}
