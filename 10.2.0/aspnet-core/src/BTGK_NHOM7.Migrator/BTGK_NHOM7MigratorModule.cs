using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BTGK_NHOM7.Configuration;
using BTGK_NHOM7.EntityFrameworkCore;
using BTGK_NHOM7.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace BTGK_NHOM7.Migrator;

[DependsOn(typeof(BTGK_NHOM7EntityFrameworkModule))]
public class BTGK_NHOM7MigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public BTGK_NHOM7MigratorModule(BTGK_NHOM7EntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(BTGK_NHOM7MigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            BTGK_NHOM7Consts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(BTGK_NHOM7MigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
