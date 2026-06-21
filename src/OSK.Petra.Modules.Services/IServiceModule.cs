using OSK.Petra.DependencyInjection.Ports;

namespace OSK.Petra.Modules.Services;

/// <summary>
/// A game module that utilizes a service provider
/// </summary>
public interface IServiceModule: IModule
{
    /// <summary>
    /// The module service provider
    /// </summary>
    IGameServiceProvider Services { get; }

    /// <summary>
    /// Initializes the module using the provided <see cref="IGameServiceProvider"/>
    /// </summary>
    /// <param name="serviceProvider">The servuces to initialize the module with</param>
    void Initialize(IGameServiceProvider serviceProvider);
}
