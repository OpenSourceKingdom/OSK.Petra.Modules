namespace OSK.Petra.Modules.Services.Configuration.Json.UnitTests._Helpers;

public class TestableJsonConfigurationProvider : JsonConfigurationProvider
{
    public TestableJsonConfigurationProvider(string basePath, string settingsName, bool reloadOnChange)
        : base(basePath, settingsName, reloadOnChange)
    {
    }
}
