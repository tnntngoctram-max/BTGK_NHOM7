using Abp.Authorization;
using Abp.Runtime.Session;
using BTGK_NHOM7.Configuration.Dto;
using System.Threading.Tasks;

namespace BTGK_NHOM7.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : BTGK_NHOM7AppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
