using Abp.Application.Services;
using BTGK_NHOM7.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace BTGK_NHOM7.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
