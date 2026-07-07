# OSK.Petra.Modules

A core pillar of a game engine that represents scene or level that can be added or remvoed during runtime. This project contains the broad definitions and data for a simple, standard game module  

# OSK.Petra.Modules.Services

Enhances a standard game module by adding a dependency injection container to the module. This module is configured during a module load or start of a game, using one or more configurators. Additionally, parent modules can be used to provide access to DI services that might not have been directly registered in the child module. For example, a global module may container a global service module that registers globally used services and can then be used in conjunction with a child module to provide the require DI for the game module at runtime. Services will be looked up from the child module first, before proceeding to the parent, the parent's parent, etc. or failing if the dependency is not found anywhere in the DI tree

For dependency fallback, the `ModuleServiceBuilder` can be configured to use the parent module or the child module service provider as the primary provider for dependency resolution. In the event the primary provider does not contain a service dependency, the fallback provider will be used to retrieve it, if possible

Configuration from a JSON or other file can be provided using standard .NET IConfiguration mechanisms, or can be skipped entirely by using the provided `EmptyConfigurationProvider`.

# OSK.Petra.Modules.Services.Configuration.Json

Provides a configuration provider that uses JSON as the file source. By default, this provider will pull using the app's base path and the appsettings for the project, but this can be configured as needed.