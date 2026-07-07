using OSK.Hexagonal.MetaData;

namespace OSK.Petra.Modules.Services.Ports;

/// <summary>
/// An object that is able to configure and modify a module's service as it is being built
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.ConsumerOptional)]
public interface IModuleServiceConfigurator<TBuilder>
    where TBuilder: IModuleServiceBuilder
{
    /// <summary>
    /// Configures the service builder to include any needed dependencies and services for the module
    /// </summary>
    /// <param name="serviceBuilder">The builder that will initialize the module's services</param>
    void Configure(TBuilder serviceBuilder);
}
