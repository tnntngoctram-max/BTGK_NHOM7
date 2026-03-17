using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using BTGK_NHOM7.Authorization.Users;
using BTGK_NHOM7.Editions;

namespace BTGK_NHOM7.MultiTenancy;

public class TenantManager : AbpTenantManager<Tenant, User>
{
    public TenantManager(
        IRepository<Tenant> tenantRepository,
        IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
        EditionManager editionManager,
        IAbpZeroFeatureValueStore featureValueStore)
        : base(
            tenantRepository,
            tenantFeatureRepository,
            editionManager,
            featureValueStore)
    {
    }
}
