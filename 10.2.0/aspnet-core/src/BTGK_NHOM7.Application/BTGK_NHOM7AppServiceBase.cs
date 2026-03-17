using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using BTGK_NHOM7.Authorization.Users;
using BTGK_NHOM7.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BTGK_NHOM7;

/// <summary>
/// Derive your application services from this class.
/// </summary>
public abstract class BTGK_NHOM7AppServiceBase : ApplicationService
{
    public TenantManager TenantManager { get; set; }

    public UserManager UserManager { get; set; }

    protected BTGK_NHOM7AppServiceBase()
    {
        LocalizationSourceName = BTGK_NHOM7Consts.LocalizationSourceName;
    }

    protected virtual async Task<User> GetCurrentUserAsync()
    {
        var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
        if (user == null)
        {
            throw new Exception("There is no current user!");
        }

        return user;
    }

    protected virtual Task<Tenant> GetCurrentTenantAsync()
    {
        return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
    }

    protected virtual void CheckErrors(IdentityResult identityResult)
    {
        identityResult.CheckErrors(LocalizationManager);
    }
}
