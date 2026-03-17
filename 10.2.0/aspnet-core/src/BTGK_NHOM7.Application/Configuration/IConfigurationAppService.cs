using BTGK_NHOM7.Configuration.Dto;
using System.Threading.Tasks;

namespace BTGK_NHOM7.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
