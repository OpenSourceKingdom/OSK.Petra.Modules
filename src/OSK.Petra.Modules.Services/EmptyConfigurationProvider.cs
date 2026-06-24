using Microsoft.Extensions.Configuration;
using OSK.Petra.Modules.Services.Ports;

namespace OSK.Petra.Modules.Services;

/// <summary>
/// Represents a configuration with no settings or values applied. 
/// </summary>
public class EmptyConfigurationProvider : IModuleConfigurationProvider
{
    #region IModuleConfigurationProvider

    /// <inheritdoc/>
    public IConfiguration GetConfiguration()
        => new ConfigurationBuilder().Build();

    #endregion
}
