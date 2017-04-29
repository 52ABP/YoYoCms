using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Json;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;

namespace YoYo.Cms.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(CmsApplicationModule))]
    public class CmsWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Modules.AbpWebApi().IsAutomaticAntiForgeryValidationEnabled = false;

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(CmsApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            //进行时间格式化
            var converters = Configuration.Modules.AbpWebApi().HttpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters;
         
            foreach (var converter in converters)
            {
                if (converter is AbpDateTimeConverter)
                {
                    var tmpConverter = converter as AbpDateTimeConverter;
                    tmpConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                }
            }

            ConfigureSwaggerUi();

        }


        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "YoYo.Cms的动态API文档");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    var applicationFileName = "bin\\" + typeof(CmsApplicationModule).Assembly.GetName().Name +
                                           ".XML";
                    var applicationFile = Path.Combine(baseDirectory, applicationFileName);
                    c.IncludeXmlComments(applicationFile);

                    var webapiFileName = "bin\\" + typeof(CmsWebApiModule).Assembly.GetName().Name + ".XML";

                    var webapiFile = Path.Combine(baseDirectory, webapiFileName);
                    c.IncludeXmlComments(webapiFile);



                })
               .EnableSwaggerUi("docs/{*assetPath}", c =>
               {
                   c.InjectJavaScript(Assembly.GetAssembly(typeof(CmsWebApiModule)), "YoYo.Cms.Api.Scripts.Swagger-Custom.js");
               });
        }


    }
}
