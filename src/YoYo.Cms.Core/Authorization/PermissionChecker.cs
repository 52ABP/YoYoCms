using Abp.Authorization;
using YoYo.Cms.Authorization.Roles;
using YoYo.Cms.MultiTenancy;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Authorization
{
    public class PermissionChecker : PermissionChecker< Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
