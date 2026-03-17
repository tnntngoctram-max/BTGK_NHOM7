using Abp.AutoMapper;
using BTGK_NHOM7.Sessions.Dto;

namespace BTGK_NHOM7.Web.Views.Shared.Components.TenantChange;

[AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
public class TenantChangeViewModel
{
    public TenantLoginInfoDto Tenant { get; set; }
}
