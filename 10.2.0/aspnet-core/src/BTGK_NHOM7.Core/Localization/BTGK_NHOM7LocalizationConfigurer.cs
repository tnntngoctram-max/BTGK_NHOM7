using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace BTGK_NHOM7.Localization;

public static class BTGK_NHOM7LocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(BTGK_NHOM7Consts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(BTGK_NHOM7LocalizationConfigurer).GetAssembly(),
                    "BTGK_NHOM7.Localization.SourceFiles"
                )
            )
        );
    }
}
