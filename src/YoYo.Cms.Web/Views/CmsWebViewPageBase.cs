using Abp.Web.Mvc.Views;

namespace YoYo.Cms.Web.Views
{
    public abstract class CmsWebViewPageBase : CmsWebViewPageBase<dynamic>
    {

    }

    public abstract class CmsWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected CmsWebViewPageBase()
        {
            LocalizationSourceName = CmsConsts.LocalizationSourceName;
        }
    }
}