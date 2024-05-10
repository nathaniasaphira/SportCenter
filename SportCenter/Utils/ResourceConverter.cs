using SportCenter.Properties;

namespace SportCenter.Utils;

public static class ResourceConverter
{
    public static string ConvertResourceToMessage(this string key)
    {
        string? value = Resources.ResourceManager.GetString(key);
        return value ?? key;
    }
}
