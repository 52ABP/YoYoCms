using Abp.MultiTenancy;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}