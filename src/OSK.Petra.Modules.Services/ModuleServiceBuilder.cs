using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSK.Petra.DependencyInjection;
using OSK.Petra.DependencyInjection.Ports;
using OSK.Petra.Modules.Services.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Modules.Services;

/// <summary>
///  A base class for game service configurators
/// </summary>
public abstract class ModuleServiceBuilder : IModuleServiceBuilder
{
    #region Variables

    private IGameServiceProvider? _serviceProvider;

    private readonly List<Action<IServiceProvider>> _postInitializationActions = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a <see cref="ModuleServiceBuilder"/> using a default configuration provider and without a game service provider
    /// </summary>
    public ModuleServiceBuilder()
        : this(new EmptyConfigurationProvider().GetConfiguration(), null, false)
    {
    }

    /// <summary>
    /// Creates a <see cref="ModuleServiceBuilder"/> using a default configuration provider and a provider game service provider to infuse the services
    /// </summary>
    /// <param name="serviceProvider">The service provider to infuse the services with</param>
    /// <param name="useAsPrimaryProvider">Whether the provider should be considered the source of dependency resolution or as a fallback. By default, it is considered a fallback, but setting this to true will specify it to be the source of resolution</param>
    public ModuleServiceBuilder(IGameServiceProvider serviceProvider, bool useAsPrimaryProvider = false)
        : this(new EmptyConfigurationProvider().GetConfiguration(), serviceProvider, useAsPrimaryProvider)
    {

    }

    /// <summary>
    /// Creates a <see cref="ModuleServiceBuilder"/> using a specified configuration provider and without a game service provider
    /// </summary>
    /// <param name="configurationProvider">The configuration provider to use to retrieve the app configuration with the scene initialization</param>
    public ModuleServiceBuilder(IModuleConfigurationProvider configurationProvider)
        : this(configurationProvider?.GetConfiguration(), null, false)
    {
    }

    /// <summary>
    /// Creates a <see cref="ModuleServiceBuilder"/> using a default configuration provider and a provider game service provider to infuse the services
    /// </summary>
    /// <param name="configurationProvider">The configuration provider to use to retrieve the app configuration with the scene initialization</param>
    /// <param name="serviceProvider">The service provider to infuse the services with</param>
    /// <param name="useAsPrimaryProvider">Whether the provider should be considered the source of dependency resolution or as a fallback. By default, it is considered a fallback, but setting this to true will specify it to be the source of resolution</param>
    public ModuleServiceBuilder(IModuleConfigurationProvider configurationProvider, IGameServiceProvider serviceProvider, bool useAsPrimaryProvider = false)
        : this (configurationProvider?.GetConfiguration(), serviceProvider, useAsPrimaryProvider)
    {

    }

    private ModuleServiceBuilder(IConfiguration? configuration, IGameServiceProvider? serviceProvider, bool useAsPrimaryProvider)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        Services = new ServiceCollection();
        if (serviceProvider is not null && useAsPrimaryProvider)
        {
            InfuseProviderIntoServices(serviceProvider, Services);
        }
        else
        {
            _serviceProvider = serviceProvider;
        }
    }

    #endregion

    #region IModuleServiceBuilder

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
        if (_serviceProvider is not null)
        {
            InfuseProviderIntoServices(_serviceProvider, Services);
        }

        var gameServiceProvider = Services.BuildServiceProvider().GetRequiredService<IGameServiceProvider>();
        return gameServiceProvider;
    }

    #endregion

    #region Helpers

    private void InfuseProviderIntoServices(IGameServiceProvider serviceProvider, IServiceCollection services)
    {
        var scopedServiceDescriptors = serviceProvider.GetServiceDescriptors().Select(descriptor =>
            descriptor.ServiceType.IsGenericTypeDefinition
                ? descriptor.ImplementationType is null ? null : new ServiceDescriptor(descriptor.ServiceType, descriptor.ImplementationType, descriptor.Lifetime)
                : new ServiceDescriptor(descriptor.ServiceType, _ =>
                {
                    var service = serviceProvider.GetService(descriptor.ServiceType);
                    return service;
                }, descriptor.Lifetime)
            )
            .Where(descriptor => descriptor is not null)
            .Select(descriptor => descriptor!);

        foreach (var descriptor in scopedServiceDescriptors)
        {
            services.TryAdd(descriptor);
        }
    }

    #endregion
}
