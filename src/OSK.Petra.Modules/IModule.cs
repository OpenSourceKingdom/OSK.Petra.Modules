namespace OSK.Petra.Modules;

/// <summary>
/// A simple game module for a game engine
/// </summary>
public interface IModule
{
    /// <summary>
    /// The name of the module
    /// </summary>
    ModuleName ModuleName { get; }
}
