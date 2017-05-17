using Abp.Application.Navigation;
using Abp.Localization;
using YoYo.Cms.Authorization;

namespace YoYo.Cms.Web.App.Startup
{

    /// <summary>
    /// 此处用于配置后端的菜单导航功能。
    /// </summary>
    public class AdminNavigationProvider : NavigationProvider
    {
        
        public override void SetNavigation(INavigationProviderContext context)
        {
             
            context.Manager.MainMenu.DisplayName = L("MainMenu");

    
            var administration=new MenuItemDefinition(
              "Administration",
                L("Administration"),
                "fa fa-info"
                );

            var user = new MenuItemDefinition(
             "User",
                L("User"),
                url: "users",
                icon: "fa fa-info"
                //  requiredPermissionName: UserAppPermissions.User
            );

            var dashboard = new MenuItemDefinition(
                "Dashboard",
              L("Dashboard"),
                url: "dashboard",
                icon: "fa fa-tachometer"
            );
            var about = new MenuItemDefinition(
                "About",
                    L("About"),
                url: "about",
                icon: "fa fa-tachometer"
            );
            var notifications = new MenuItemDefinition(
                "Notifications",
                L("Notifications"),
                url: "notifications",
                icon: "fa fa-tachometer"
            );
            var smsmanage = new MenuItemDefinition(
                "SmsManage",
                L("SmsManage"),
                url: "smsmanage",
                icon: "fa fa-mobile"
            );
            var orgManage = new MenuItemDefinition(
                "OrgManage",
                L("OrgManage"),
                url: "orgmanage",
                icon: "fa fa-share-alt"
            );
            var roleManage = new MenuItemDefinition(
                "roles",
                L("RoleManage"),
                url: "roles",
                icon: "fa fa-id-card-o"
            );
            var auditLog = new MenuItemDefinition(
                "AuditLog",
                L("AuditLog"),
                url: "auditlog",
                icon: "fa fa-briefcase"
            );
            administration
                .AddItem(user)
                .AddItem(notifications)
                .AddItem(smsmanage)
                .AddItem(orgManage)
                .AddItem(roleManage)
                .AddItem(auditLog);

            context.Manager.MainMenu.AddItem(dashboard)
                .AddItem(administration)
                .AddItem(about);











            #region 测试菜单栏


            //.AddItem(new MenuItemDefinition("Multilevel", new FixedLocalizableString("多级菜单测试"), "fa fa-beer")
            //    .AddItem(
            //        new MenuItemDefinition(
            //            "Home",
            //            new LocalizableString("HomePage", CmsConsts.LocalizationSourceName),
            //            url: "home",
            //            icon: "fa fa-home",
            //            requiresAuthentication: true
            //        ).AddItem(bankCard).AddItem(new MenuItemDefinition(
            //            "Users",
            //            L("Users"),
            //            url: "users",
            //            icon: "fa fa-users",
            //            requiredPermissionName: PermissionNames.Pages_Users)).AddItem(resourceAdministrator)
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "Tenants",
            //            L("Tenants"),
            //            url: "tenants",
            //            icon: "fa fa-globe",
            //            requiredPermissionName: PermissionNames.Pages_Tenants
            //        )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "Users",
            //            L("Users"),
            //            url: "users",
            //            icon: "fa fa-users",
            //            requiredPermissionName: PermissionNames.Pages_Users
            //        )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "About",
            //            new LocalizableString("About", CmsConsts.LocalizationSourceName),
            //            url: "about",
            //            icon: "fa fa-info"
            //        ))
            //).AddItem(resourceAdministrator);








            #endregion







        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CmsConsts.LocalizationSourceName);
        }
    }
}