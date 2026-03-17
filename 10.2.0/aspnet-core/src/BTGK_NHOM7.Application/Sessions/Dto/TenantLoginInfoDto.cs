using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BTGK_NHOM7.MultiTenancy;

namespace BTGK_NHOM7.Sessions.Dto;

[AutoMapFrom(typeof(Tenant))]
public class TenantLoginInfoDto : EntityDto
{
    public string TenancyName { get; set; }

    public string Name { get; set; }
}
