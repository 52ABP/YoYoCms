using System.Threading.Tasks;
using Abp.Application.Services;
using YoYo.Cms.Roles.Dto;

namespace YoYo.Cms.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
