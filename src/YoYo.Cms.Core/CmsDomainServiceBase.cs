using Abp.Domain.Services;

namespace YoYo.Cms
{
    public class CmsDomainServiceBase:DomainService
    {
        protected CmsDomainServiceBase()
        {
            LocalizationSourceName = CmsConsts.LocalizationSourceName;
        }
    }
}