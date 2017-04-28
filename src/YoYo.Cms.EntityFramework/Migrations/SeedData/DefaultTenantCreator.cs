using System.Linq;
using YoYo.Cms.EntityFramework;
using YoYo.Cms.MultiTenancy;

namespace YoYo.Cms.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly CmsDbContext _context;

        public DefaultTenantCreator(CmsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
