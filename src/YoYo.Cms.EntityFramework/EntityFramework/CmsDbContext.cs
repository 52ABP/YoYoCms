using Abp.Zero.EntityFramework;
using YoYo.Cms.Authorization.Roles;
using YoYo.Cms.EntityMapper.Users;
using YoYo.Cms.MultiTenancy;
using System.Data.Common;
using System.Data.Entity;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.EntityFramework
{
    public class CmsDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

   


        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public CmsDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in CmsDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of CmsDbContext since ABP automatically handles it.
         */
        public CmsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public CmsDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public CmsDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region AbpZero_Initial

            //修改ABP的默认表信息
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("", "ABP");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserCfg());

            #endregion

            //关闭启动校验
            Database.SetInitializer<CmsDbContext>(null);

            


            //   modelBuilder.Entity<User>().a => a.Name);

        }
    }
}
