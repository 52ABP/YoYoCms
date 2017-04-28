using Abp.Configuration;
using Abp.Zero.Configuration;
using System.Collections.Generic;

namespace YoYo.Cms.Configuration
{
    /// <summary>
    /// Defines settings for the application.
    /// See <see cref="AppSettings"/> for setting names.
    /// </summary>
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            context.Manager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled).DefaultValue = false.ToString().ToLowerInvariant();

            return new[]
                   {
                       //Host settings
                        new SettingDefinition(AppSettings.General.WebSiteRootAddress, "http://localhost:16634/")
                   };
        }
    }
}
