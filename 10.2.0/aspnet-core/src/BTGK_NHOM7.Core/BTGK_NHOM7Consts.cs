using BTGK_NHOM7.Debugging;

namespace BTGK_NHOM7;

public class BTGK_NHOM7Consts
{
    public const string LocalizationSourceName = "BTGK_NHOM7";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "11187d491a04454e93a9168f0a0625e2";
}
