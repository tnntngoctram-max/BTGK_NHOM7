using Abp.Modules;
using Abp.Reflection.Extensions;
using BTGK_NHOM7.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BTGK_NHOM7.Web.Host.Startup
{
    [DependsOn(
       typeof(BTGK_NHOM7WebCoreModule))]
    public class BTGK_NHOM7WebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BTGK_NHOM7WebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BTGK_NHOM7WebHostModule).GetAssembly());
        }
    }
}
