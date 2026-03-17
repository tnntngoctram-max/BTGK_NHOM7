using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace BTGK_NHOM7.Web.Views;

public abstract class BTGK_NHOM7RazorPage<TModel> : AbpRazorPage<TModel>
{
    [RazorInject]
    public IAbpSession AbpSession { get; set; }

    protected BTGK_NHOM7RazorPage()
    {
        LocalizationSourceName = BTGK_NHOM7Consts.LocalizationSourceName;
    }
}
