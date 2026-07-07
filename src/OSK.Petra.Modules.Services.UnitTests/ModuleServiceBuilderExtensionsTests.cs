using Moq;
using OSK.Petra.Modules.Services.Ports;
using OSK.Petra.Modules.Services.UnitTests._Helpers;

namespace OSK.Petra.Modules.Services.UnitTests;

public class ModuleServiceBuilderExtensionsTests
{
    #region Variables

    private readonly Mock<IModuleServiceBuilder> _mockBuilder;

    #endregion

    #region Constructors

    public ModuleServiceBuilderExtensionsTests()
    {
        _mockBuilder = new Mock<IModuleServiceBuilder>();
    }

    #endregion

    #region AddPostInitializationAction

    [Fact]
    public void AddPostInitializationAction_WithNullBuilder_ThrowsArgumentNullException()
    {
        // Arrange
        IModuleServiceBuilder? nullBuilder = null;
        var action = new Action<IServiceProvider>(_ => { });

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ModuleServiceBuilderExtensions.AddPostInitializationAction(nullBuilder!, action));
    }

    [Fact]
    public void AddPostInitializationAction_WithNullAction_ThrowsArgumentNullException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _mockBuilder.Object.AddPostInitializationAction(null!));
    }

    [Fact]
    public void AddPostInitializationAction_WithValidBuilderAndAction_CallsRegisterAndReturnsBuilder()
    {
        // Arrange
        var action = new Action<IServiceProvider>(_ => { });

        // Act
        var result = _mockBuilder.Object.AddPostInitializationAction(action);

        // Assert
        _mockBuilder.Verify(b => b.RegisterPostInitializationAction(action), Times.Once);
        Assert.Same(_mockBuilder.Object, result);
    }

    #endregion

    #region AddPostInitializationAction_Generic

    [Fact]
    public void AddPostInitializationAction_Generic_WithNullBuilder_ThrowsArgumentNullException()
    {
        // Arrange
        IModuleServiceBuilder? nullBuilder = null;
        var action = new Action<ITestMarker>(_ => { });

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ModuleServiceBuilderExtensions.AddPostInitializationAction<ITestMarker>(nullBuilder!, action));
    }

    [Fact]
    public void AddPostInitializationAction_Generic_WithNullAction_ThrowsArgumentNullException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _mockBuilder.Object.AddPostInitializationAction<ITestMarker>(null!));
    }

    [Fact]
    public void AddPostInitializationAction_Generic_WithValidWrapsActionInServiceProviderCallback()
    {
        // Arrange
        var action = new Action<ITestMarker>(_ => { });

        // Act
        var result = _mockBuilder.Object.AddPostInitializationAction(action);

        // Assert
        _mockBuilder.Verify(b => b.RegisterPostInitializationAction(It.IsAny<Action<IServiceProvider>>()), Times.Once);
        Assert.Same(_mockBuilder.Object, result);
    }

    [Fact]
    public void AddPostInitializationAction_Generic_ReturnsBuilderForChaining()
    {
        // Arrange
        var action = new Action<ITestMarker>(_ => { });

        // Act
        var result = _mockBuilder.Object.AddPostInitializationAction(action);

        // Assert
        Assert.Same(_mockBuilder.Object, result);
    }

    #endregion
}
