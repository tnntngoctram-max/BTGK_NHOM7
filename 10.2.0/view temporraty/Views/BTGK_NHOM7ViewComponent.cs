using Abp.AspNetCore.Mvc.ViewComponents;

namespace BTGK_NHOM7.Web.Views;

public abstract class BTGK_NHOM7ViewComponent : AbpViewComponent
{
    protected BTGK_NHOM7ViewComponent()
    {
        LocalizationSourceName = BTGK_NHOM7Consts.LocalizationSourceName;
    }
}
