using OSK.Petra.DependencyInjection.Ports;
using OSK.Petra.Modules.Services.Ports;

namespace OSK.Petra.Modules.Services.UnitTests._Helpers;

public class TestableModuleServiceBuilder : ModuleServiceBuilder
{
    public TestableModuleServiceBuilder(IGameServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public TestableModuleServiceBuilder(IModuleConfigurationProvider configurationProvider)
        : base(configurationProvider)
    {
    }

    public TestableModuleServiceBuilder(IModuleConfigurationProvider configurationProvider, IGameServiceProvider serviceProvider, bool useAsPrimary = false)
        : base(configurationProvider, serviceProvider, useAsPrimary)
    {
    }
}