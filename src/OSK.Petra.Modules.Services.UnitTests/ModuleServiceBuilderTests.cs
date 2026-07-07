using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OSK.Extensions.Object.DeepEquals;
using OSK.Petra.DependencyInjection.Ports;
using OSK.Petra.Modules.Services.UnitTests._Helpers;

namespace OSK.Petra.Modules.Services.UnitTests;

public class ModuleServiceBuilderTests
{
    #region Variables

    private readonly Mock<IGameServiceProvider> _mockServiceProvider;
    private readonly Mock<Ports.IModuleConfigurationProvider> _mockConfigurationProvider;
    private readonly IConfiguration _mockConfiguration;

    #endregion

    #region Constructors

    public ModuleServiceBuilderTests()
    {
        _mockServiceProvider = new Mock<IGameServiceProvider>();
        _mockConfigurationProvider = new Mock<Ports.IModuleConfigurationProvider>();
        _mockConfiguration = new ConfigurationBuilder().Build();

        _mockConfigurationProvider.Setup(p => p.GetConfiguration())
            .Returns(_mockConfiguration);
    }

    #endregion

    #region Constructor_WithServiceProviderOnly

    [Fact]
    public void Constructor_WithServiceProviderOnly_CreatesBuilderWithEmptyConfig()
    {
        // Arrange
        _mockServiceProvider.Setup(m => m.GetServiceDescriptors())
            .Returns([]);

        // Act
        var builder = new TestableModuleServiceBuilder(_mockServiceProvider.Object);

        // Assert.
        Assert.NotNull(builder.Services);
        Assert.Empty(builder.Services);

        Assert.NotNull(builder.Configuration);
        _mockConfiguration.DeepEquals(builder.Configuration);
    }

    [Fact]
    public void Constructor_WithServiceProviderOnly_PassesServiceProviderToBase()
    {
        // Arrange
        var expectedServices = new ServiceCollection();
        _mockServiceProvider.Setup(sp => sp.GetServiceDescriptors())
            .Returns([.. expectedServices]);

        // Act
        var builder = new TestableModuleServiceBuilder(_mockServiceProvider.Object);

        // Assert
        Assert.Equal(expectedServices, builder.Services);
    }

    #endregion

    #region Constructor_WithConfigurationProvider

    [Fact]
    public void Constructor_WithNullConfigurationProvider_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new TestableModuleServiceBuilder(null!, _mockServiceProvider.Object));
    }

    [Fact]
    public void Constructor_WithValidConfigurationProvider_SetsConfiguration()
    {
        // Arrange
        _mockServiceProvider.Setup(sp => sp.GetServiceDescriptors())
            .Returns([]);

        // Act
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object, _mockServiceProvider.Object);

        // Assert
        Assert.NotNull(builder.Configuration);
    }

    [Fact]
    public void Constructor_WithNullServiceProvider_CreatesNewServiceCollection()
    {
        // Act
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object, null!);

        // Assert
        Assert.NotNull(builder.Services);
        Assert.Empty(builder.Services);
    }

    [Fact]
    public void Constructor_WithValidServiceProvider_UsesProvidedScopedServices()
    {
        // Arrange
        var expectedServices = new ServiceCollection();
        expectedServices.AddTransient<IServiceCollection, ServiceCollection>();

        _mockServiceProvider.Setup(sp => sp.GetServiceDescriptors())
            .Returns([.. expectedServices]);

        // Act
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object, _mockServiceProvider.Object, useAsPrimary: true);

        // Assert
        Assert.Equal(expectedServices.Count, builder.Services.Count);
    }

    #endregion

    #region RegisterPostInitializationAction

    [Fact]
    public void RegisterPostInitializationAction_WithNullAction_ThrowsArgumentNullException()
    {
        // Arrange
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => builder.RegisterPostInitializationAction(null!));
    }

    [Fact]
    public void RegisterPostInitializationAction_WithValidAction_AddsToCollection()
    {
        // Arrange
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object);
        var action = new Action<IServiceProvider>(_ => { });

        // Act
        builder.RegisterPostInitializationAction(action);

        // Assert
        Assert.Contains(action, builder.PostInitializationActions);
    }

    [Fact]
    public void RegisterPostInitializationAction_MultipleActions_AccumulatesAll()
    {
        // Arrange
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object);
        var action1 = new Action<IServiceProvider>(_ => { });
        var action2 = new Action<IServiceProvider>(_ => { });

        // Act
        builder.RegisterPostInitializationAction(action1);
        builder.RegisterPostInitializationAction(action2);

        // Assert
        Assert.Equal(2, builder.PostInitializationActions.Count);
    }

    #endregion

    #region BuildServiceProvider

    [Fact]
    public void BuildServiceProvider_ReturnsIGameServiceProvider()
    {
        // Arrange
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object);

        // Act
        var provider = builder.BuildServiceProvider();

        // Assert
        Assert.NotNull(provider);
    }

    #endregion

    #region Configuration

    [Fact]
    public void Configuration_ReturnsNonNullValue()
    {
        // Arrange
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object);

        // Act
        var configuration = builder.Configuration;

        // Assert
        Assert.NotNull(configuration);
        Assert.Equal(_mockConfiguration, configuration);
    }

    #endregion

    #region Services

    [Fact]
    public void Services_ReturnsIServiceCollection()
    {
        // Arrange
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object);

        // Act
        var services = builder.Services;

        // Assert
        Assert.NotNull(services);
        Assert.Empty(services);
    }

    [Fact]
    public void Services_AllowsAddingServices()
    {
        // Arrange
        var builder = new TestableModuleServiceBuilder(_mockConfigurationProvider.Object);

        // Act
        builder.Services.AddSingleton<ITestMarker, TestMarkerImplementation>();

        // Assert
        Assert.Single(builder.Services);
    }

    #endregion
}
