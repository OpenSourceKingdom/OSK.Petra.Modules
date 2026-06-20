using Microsoft.Extensions.DependencyInjection;
using System;
using OSK.Petra.Modules.Services.Ports;

namespace OSK.Petra.Modules.Services;

/// <summary>
/// A set of <see cref="IModuleServiceBuilder"/> extensions to enrich its usage
/// </summary>
public static class ModuleServiceBuilderExtensions
{
    /// <summary>
    /// Add a custom action to run after initializing a module that utilizes the built <see cref="IServiceProvider"/>
    /// </summary>
    /// <param name="builder">The builder for the module services</param>
    /// <param name="action">The action to run</param>
    /// <returns>The builder for chaining</returns>
    /// <exception cref="ArgumentNullException">if the builder or action is null</exception>
    public static IModuleServiceBuilder AddPostInitializationAction(this IModuleServiceBuilder builder, Action<IServiceProvider> action)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        builder.RegisterPostInitializationAction(action);

        return builder;
    }

    /// <summary>
    /// Add a custom action to run after initializing a module that utilizes the built <see cref="IServiceProvider"/> to get the service for the action
    /// </summary>
    /// <param name="builder">The builder for the module services</param>
    /// <param name="action">The action to run</param>
    /// <returns>The builder for chaining</returns>
    /// <exception cref="ArgumentNullException">if the builder or action is null</exception>
    public static IModuleServiceBuilder AddPostInitializationAction<TService>(this IModuleServiceBuilder builder, Action<TService> action)
        where TService: notnull
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        builder.RegisterPostInitializationAction(serviceProvider =>
        {
            var service = serviceProvider.GetRequiredService<TService>();
            action(service);
        });
        
        return builder;
    }
}
