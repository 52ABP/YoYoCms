using System.Collections.Generic;
using YoYo.Cms.Authorization.Permissions.Dto;

namespace YoYo.Cms.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}