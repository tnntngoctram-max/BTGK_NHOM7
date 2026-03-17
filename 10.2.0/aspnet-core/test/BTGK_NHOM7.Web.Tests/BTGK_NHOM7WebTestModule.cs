using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BTGK_NHOM7.EntityFrameworkCore;
using BTGK_NHOM7.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace BTGK_NHOM7.Web.Tests;

[DependsOn(
    typeof(BTGK_NHOM7WebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class BTGK_NHOM7WebTestModule : AbpModule
{
    public BTGK_NHOM7WebTestModule(BTGK_NHOM7EntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(BTGK_NHOM7WebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(BTGK_NHOM7WebMvcModule).Assembly);
    }
}