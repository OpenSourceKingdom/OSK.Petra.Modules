namespace OSK.Petra.Modules.Services.UnitTests;

public class EmptyConfigurationProviderTests
{
    #region Variables

    private readonly EmptyConfigurationProvider _provider;

    #endregion

    #region Constructors

    public EmptyConfigurationProviderTests()
    {
        _provider = new EmptyConfigurationProvider();
    }

    #endregion

    #region GetConfiguration

    [Fact]
    public void GetConfiguration_ReturnsEmptyConfiguration()
    {
        // Act
        var configuration = _provider.GetConfiguration();

        // Assert
        Assert.NotNull(configuration);
        Assert.Null(configuration["NonExistentKey"]);
    }

    [Fact]
    public void GetConfiguration_GetSection_ReturnsEmptyValueWithoutCrashing()
    {
        // Act
        var configuration = _provider.GetConfiguration();
        var section = configuration.GetSection("NonExistentSection");

        // Assert
        Assert.NotNull(section);
        Assert.Null(section.Value);
        Assert.Equal("NonExistentSection", section.Key);
    }

    #endregion
}
