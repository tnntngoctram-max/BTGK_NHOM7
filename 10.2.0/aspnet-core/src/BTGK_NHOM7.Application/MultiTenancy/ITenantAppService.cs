using Abp.Application.Services;
using BTGK_NHOM7.MultiTenancy.Dto;

namespace BTGK_NHOM7.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

