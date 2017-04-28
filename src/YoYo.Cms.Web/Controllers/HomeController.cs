using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace YoYo.Cms.Web.Controllers
{
	[AbpMvcAuthorize]
	public class HomeController : CmsControllerBase
	{
        public ActionResult Index()
        {
            return View("~/App/admin/views/layout/layout.cshtml"); //Layout of the angular application.
        }
 

        public ActionResult Test()
		{
			return View();
		}

	}
}