using System.IO;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using Abp.Extensions;
using YoYo.Cms.Web.Bundles;

namespace YoYo.Cms.Web.App.Startup
{
    public class AppBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            AddAppAssetsCss(bundles);
            AddAppAssetsScript(bundles);


            //~/Bundles/App/Main/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/Main/css")
                     .IncludeDirectory("~/Common/Scripts", "*.css", true)
               
                );

            //~/Bundles/App/Main/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/Common/js")
                    .IncludeDirectory("~/Common/Scripts", "*.js", true)
                );

            bundles.Add(
               new ScriptBundle("~/Bundles/App/directives/js")
                   .IncludeDirectory("~/App/Admin/directives", "*.js", true)
                   .IncludeDirectory("~/App/Admin/filters", "*.js", true)
                   .IncludeDirectory("~/App/Admin/services", "*.js", true)
               );
            bundles.Add(
            new ScriptBundle("~/Bundles/App/views/js")
                .IncludeDirectory("~/App/Admin/views", "*.js", true)
            
            );
        }


        private static void AddAppAssetsScript(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Bundles/App/js").Include(
                "~/Abp/Framework/scripts/utils/ie10fix.js"
                , "~/libs/json2/json2.js"
                , "~/libs/modernizr/modernizr-2.8.3.js"
                , "~/libs/jquery/jquery.js"
                 , "~/libs/bootstrap/js/bootstrap.js"
                , "~/libs/moment/moment-with-locales.js"
                , "~/libs/moment/moment-timezone-with-data.js"
                , "~/libs/jquery-validation/js/jquery.validate.js"
                , "~/libs/jquery.blockUI/jquery.blockUI.js"
                , "~/libs/toastr2.1.1/toastr.js"
                , "~/libs/sweetalert/sweet-alert.js"
                , "~/libs/spinjs/spin.js"
                , "~/libs/spinjs/jquery.spin.js"
                , "~/libs/angular/angular.js"
                , "~/libs/angular-animate/angular-animate.js"
                , "~/libs/angular-sanitize/angular-sanitize.js"
                , "~/libs/angular-ui-router/angular-ui-router.js"
                , "~/libs/angular-ui/ui-bootstrap.js"
                , "~/libs/angular-ui/ui-bootstrap-tpls.js"
                , "~/libs/angular-ui/ui-utils.js"
                , "~/Abp/Framework/scripts/abp.js"
                , "~/Abp/Framework/scripts/libs/abp.jquery.js"
                , "~/Abp/Framework/scripts/libs/abp.toastr.js"
                , "~/Abp/Framework/scripts/libs/abp.blockUI.js"
                , "~/Abp/Framework/scripts/libs/abp.spin.js"
                , "~/Abp/Framework/scripts/libs/abp.sweet-alert.js"
                , "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js"
                , "~/libs/signalR/jquery.signalR-2.2.1.js"
              
            ).ForceOrdered());








        }


        private static void AddAppAssetsCss(BundleCollection bundles)
        {
            //加载第三方通用组件css
            bundles.Add(new StyleBundle("~/Bundles/App/css")
                .Include(
                    "~/libs/bootstrap/css/bootstrap.css",
                    "~/libs/animate/animate.css",
                    "~/libs/font-awesome-4.7.0/css/font-awesome.css"
                    , "~/libs/toastr2.1.1/toastr.css"
                    , "~/libs/sweetalert/sweet-alert.css"
                    , "~/libs/flags/famfamfam-flags.css"
                ).ForceOrdered()
            );


            //加载admin模板的架子css
            bundles.Add(new StyleBundle("~/Bundles/App/creativeTim/css")
                .Include(
                    "~/creativeTim/assets/css/light-bootstrap-dashboard.css",
                    "~/creativeTim/assets/css/components-md.css",
                    "~/creativeTim/assets/css/pe-icon-7-stroke.css"
                ).ForceOrdered()
            );
        }

        public static string AngularLocalization
        {
            get
            {
                return GetLocalizationFileForjAngularOrNull(Thread.CurrentThread.CurrentUICulture.Name.ToLower())
                       ?? GetLocalizationFileForjAngularOrNull(Thread.CurrentThread.CurrentUICulture.Name.Left(2).ToLower())
                       ?? "~/libs/i18n/angular-locale_en-us.js";
            }
        }
        private static string GetLocalizationFileForjAngularOrNull(string cultureCode)
        {
            try
            {
                var relativeFilePath = "~/libs/i18n/angular-locale_" + cultureCode + ".js";
                var physicalFilePath = HttpContext.Current.Server.MapPath(relativeFilePath);
                if (File.Exists(physicalFilePath))
                {
                    return relativeFilePath;
                }
            }
            catch { }

            return null;
        }

    }
}