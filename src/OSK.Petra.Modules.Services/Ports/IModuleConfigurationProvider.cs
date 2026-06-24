using Microsoft.Extensions.Configuration;

namespace OSK.Petra.Modules.Services.Ports;

/// <summary>
/// A configuration provider for the Petra ecosystem that provides a <see cref="IConfiguration"/> for the <see cref="IModuleServiceBuilder"/>
/// </summary>
public interface IModuleConfigurationProvider
{
    /// <summary>
    /// Gets the configuration object to use for the initialization of the current game
    /// </summary>
    /// <returns>An <see cref="IConfiguration"/> that can setup the scene</returns>
    IConfiguration GetConfiguration();
}
