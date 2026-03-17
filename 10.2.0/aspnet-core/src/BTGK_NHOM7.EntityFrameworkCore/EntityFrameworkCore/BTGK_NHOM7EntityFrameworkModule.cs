using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using BTGK_NHOM7.EntityFrameworkCore.Seed;

namespace BTGK_NHOM7.EntityFrameworkCore;

[DependsOn(
    typeof(BTGK_NHOM7CoreModule),
    typeof(AbpZeroCoreEntityFrameworkCoreModule))]
public class BTGK_NHOM7EntityFrameworkModule : AbpModule
{
    /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
    public bool SkipDbContextRegistration { get; set; }

    public bool SkipDbSeed { get; set; }

    public override void PreInitialize()
    {
        if (!SkipDbContextRegistration)
        {
            Configuration.Modules.AbpEfCore().AddDbContext<BTGK_NHOM7DbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    BTGK_NHOM7DbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    BTGK_NHOM7DbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(BTGK_NHOM7EntityFrameworkModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        if (!SkipDbSeed)
        {
            SeedHelper.SeedHostDb(IocManager);
        }
    }
}
