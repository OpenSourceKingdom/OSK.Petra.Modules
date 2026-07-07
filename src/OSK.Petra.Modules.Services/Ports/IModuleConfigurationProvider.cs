using Microsoft.Extensions.Configuration;
using OSK.Hexagonal.MetaData;

namespace OSK.Petra.Modules.Services.Ports;

/// <summary>
/// A configuration provider for the Petra ecosystem that provides a <see cref="IConfiguration"/> for the <see cref="IModuleServiceBuilder"/>
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.LibraryProvided, HexagonalIntegrationType.ConsumerOptional, HexagonalIntegrationType.IntegrationOptional)]
public interface IModuleConfigurationProvider
{
    /// <summary>
    /// Gets the configuration object to use for the initialization of the current game
    /// </summary>
    /// <returns>An <see cref="IConfiguration"/> that can setup the scene</returns>
    IConfiguration GetConfiguration();
}
