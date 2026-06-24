using OSK.Petra.DependencyInjection.Ports;

namespace OSK.Petra.Modules.Services.UnitTests._Helpers;

public class TestableModuleServiceBuilder : ModuleServiceBuilder
{
    public TestableModuleServiceBuilder(IGameServiceProvider? serviceProvider = null)
        : base(serviceProvider)
    {
    }

    public TestableModuleServiceBuilder(Ports.IModuleConfigurationProvider configurationProvider, IGameServiceProvider? serviceProvider = null)
        : base(configurationProvider, serviceProvider)
    {
    }
}