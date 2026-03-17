using Abp.MultiTenancy;
using BTGK_NHOM7.Authorization.Users;

namespace BTGK_NHOM7.MultiTenancy;

public class Tenant : AbpTenant<User>
{
    public Tenant()
    {
    }

    public Tenant(string tenancyName, string name)
        : base(tenancyName, name)
    {
    }
}
