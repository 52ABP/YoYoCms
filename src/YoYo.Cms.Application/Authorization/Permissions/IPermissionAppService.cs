using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YoYo.Cms.Authorization.Permissions.Dto;

namespace YoYo.Cms.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
