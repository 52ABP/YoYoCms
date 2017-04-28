using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using YoYo.Cms.Authorization;
using YoYo.Cms.Authorization.Roles;
using YoYo.Cms.MultiTenancy;
using YoYo.Cms.Configuration;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class CmsCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            ////Adding setting providers
            Configuration.Settings.Providers.Add<AppSettingProvider>();

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = CmsConsts.MultiTenancyEnabled;

            ////Adding setting providers
            Configuration.Settings.Providers.Add<AppSettingProvider>();

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CmsConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "YoYo.Cms.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<CmsAuthorizationProvider>();

            //if (DebugHelper.IsDebug)
            //{
            //    //Disabling email sending in debug mode
            //    IocManager.Register<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
            //}
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
