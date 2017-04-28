using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using YoYo.Cms.EntityFramework;

namespace YoYo.Cms.Migrations.SeedData
{
    public class DefaultSettingsCreator
    {
        private readonly CmsDbContext _context;

        public DefaultSettingsCreator(CmsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@yoyocms.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "admin");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Port, "587");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Host, "smtp.qq.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UserName, "admin");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Password, "admin");
            AddSettingIfNotExists(EmailSettingNames.Smtp.EnableSsl, "true");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UseDefaultCredentials, "false");

            //Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "zh-CN");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}