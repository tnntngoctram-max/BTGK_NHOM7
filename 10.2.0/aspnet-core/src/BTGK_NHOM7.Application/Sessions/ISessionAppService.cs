using Abp.Application.Services;
using BTGK_NHOM7.Sessions.Dto;
using System.Threading.Tasks;

namespace BTGK_NHOM7.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
