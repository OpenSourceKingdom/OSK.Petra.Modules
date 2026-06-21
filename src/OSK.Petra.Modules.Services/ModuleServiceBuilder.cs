using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSK.Petra.DependencyInjection;
using OSK.Petra.DependencyInjection.Ports;
using System;
using System.Collections.Generic;
using OSK.Petra.Modules.Services.Ports;

namespace OSK.Petra.Modules.Services;

/// <summary>
///  A base class for game service configurators
/// </summary>
public abstract class ModuleServiceBuilder : IModuleServiceBuilder
{
    #region Variables

    private readonly List<Action<IServiceProvider>> _postInitializationActions = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a <see cref="ModuleServiceBuilder"/> using an <see cref="EmptyConfigurationProvider"/> and an optional <see cref="IGameServiceProvider"/> as an initializer
    /// </summary>
    /// <param name="serviceProvider">An initializing service provider</param>
    public ModuleServiceBuilder(IGameServiceProvider? serviceProvider = null)
        : this(new EmptyConfigurationProvider(), serviceProvider)
    {
    }

    /// <summary>
    /// Creates a <see cref="ModuleServiceBuilder"/> with a given <see cref="Microsoft.Extensions.Configuration.IConfigurationProvider"/> and using an optional <see cref="IGameServiceProvider"/> as an initializer
    /// </summary>
    /// <param name="configurationProvider">The configuration provider to use to retrieve the app configuration with the scene initialization</param>
    /// <param name="serviceProvider">An initializing service provider</param>
    public ModuleServiceBuilder(Ports.IConfigurationProvider configurationProvider, IGameServiceProvider? serviceProvider = null)
    {
        if (configurationProvider is null)
        {
            throw new ArgumentNullException(nameof(configurationProvider));
        }

        Configuration = configurationProvider.GetConfiguration();

        Services = serviceProvider?.CreateScopedServices() ?? new ServiceCollection();
    }

    #endregion

    #region IGameServiceConfigurator

    /// <inheritdoc/>
    public IReadOnlyCollection<Action<IServiceProvider>> PostInitializationActions => _postInitializationActions;

    /// <inheritdoc/>
    public IServiceCollection Services { get; }

    /// <inheritdoc/>
    public IConfiguration Configuration { get; }

    /// <inheritdoc/>
    public void RegisterPostInitializationAction(Action<IServiceProvider> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        _postInitializationActions.Add(action);
    }

    /// <inheritdoc/>
    public IGameServiceProvider BuildServiceProvider()
    {
        Services.AddGameServiceProvider();
        var gameServiceProvider = Services.BuildServiceProvider().GetRequiredService<IGameServiceProvider>();
        return gameServiceProvider;
    }

    #endregion
}
