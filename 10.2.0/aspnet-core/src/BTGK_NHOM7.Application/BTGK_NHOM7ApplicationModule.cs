using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BTGK_NHOM7.Authorization;

namespace BTGK_NHOM7;

[DependsOn(
    typeof(BTGK_NHOM7CoreModule),
    typeof(AbpAutoMapperModule))]
public class BTGK_NHOM7ApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<BTGK_NHOM7AuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(BTGK_NHOM7ApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
