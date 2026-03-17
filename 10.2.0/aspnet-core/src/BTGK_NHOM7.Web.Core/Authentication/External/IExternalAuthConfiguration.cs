using System.Collections.Generic;

namespace BTGK_NHOM7.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
