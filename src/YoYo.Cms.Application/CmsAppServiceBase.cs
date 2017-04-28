using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using YoYo.Cms.MultiTenancy;
using YoYo.Cms.UserManagerment.Users;
using Microsoft.AspNet.Identity;

namespace YoYo.Cms
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CmsAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected CmsAppServiceBase()
        {
            LocalizationSourceName = CmsConsts.LocalizationSourceName;
        }
        /// <summary>
        /// 返回当前用户信息
        /// </summary>
        /// <returns></returns>
        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}