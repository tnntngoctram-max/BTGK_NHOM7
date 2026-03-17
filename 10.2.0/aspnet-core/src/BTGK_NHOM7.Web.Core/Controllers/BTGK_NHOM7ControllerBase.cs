using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BTGK_NHOM7.Controllers
{
    public abstract class BTGK_NHOM7ControllerBase : AbpController
    {
        protected BTGK_NHOM7ControllerBase()
        {
            LocalizationSourceName = BTGK_NHOM7Consts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
