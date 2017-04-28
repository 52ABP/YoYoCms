using YoYo.Cms.EntityFramework;
using EntityFramework.DynamicFilters;

namespace YoYo.Cms.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly CmsDbContext _context;

        public InitialHostDbBuilder(CmsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
