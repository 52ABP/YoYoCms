using System.Data.Entity.ModelConfiguration;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.EntityMapper.Users
{
    public class UserCfg:EntityTypeConfiguration<User>
    {
        public UserCfg()
        {
            ToTable("Users", CmsConsts.SchemaName.ABP);
         
         
            Property(a => a.EmailAddress).IsOptional();
        }
    }
}