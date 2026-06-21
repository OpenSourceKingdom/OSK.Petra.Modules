using Microsoft.Extensions.Configuration;

namespace OSK.Petra.Modules.Services;

/// <summary>
/// Represents a configuration with no settings or values applied. 
/// </summary>
public class EmptyConfigurationProvider : Ports.IConfigurationProvider
{
    #region IGameConfigurationProvider

    /// <inheritdoc/>
    public IConfiguration GetConfiguration()
        => new ConfigurationBuilder().Build();

    #endregion
}
