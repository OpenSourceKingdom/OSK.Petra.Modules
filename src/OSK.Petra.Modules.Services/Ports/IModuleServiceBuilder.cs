using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSK.Hexagonal.MetaData;
using OSK.Petra.DependencyInjection.Ports;
using System;

namespace OSK.Petra.Modules.Services.Ports;

/// <summary>
/// A configurator that combines with the <see cref="IGameServiceProvider"/> to initialize an entire game scene or level for use with a DI container
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
public interface IModuleServiceBuilder
{
    /// <summary>
    /// The configurable services for the scene
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// The related configuration file to use with the scene initialization
    /// </summary>
    IConfiguration Configuration { get; }

    /// <summary>
    /// Registers an action to trigger after a scene has been configured
    /// </summary>
    /// <param name="action"></param>
    void RegisterPostInitializationAction(Action<IServiceProvider> action);

    /// <summary>
    /// Build the service provider for the scene
    /// </summary>
    /// <returns>An <see cref="IGameServiceProvider"/> that can be used with a dependency injector to initialize a scene</returns>
    IGameServiceProvider BuildServiceProvider();
}
