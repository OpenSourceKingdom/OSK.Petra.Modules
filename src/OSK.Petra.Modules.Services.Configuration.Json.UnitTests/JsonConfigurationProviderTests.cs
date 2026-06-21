using OSK.Petra.Modules.Services.Configuration.Json.UnitTests._Helpers;

namespace OSK.Petra.Modules.Services.Configuration.Json.UnitTests;

public class JsonConfigurationProviderTests
{
    #region Variables

    private readonly string _tempDir;
    private readonly string _validJsonFile;
    private readonly string _emptyJsonFile;

    #endregion

    #region Constructors

    public JsonConfigurationProviderTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"JsonConfigTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(_tempDir);

        _validJsonFile = Path.Combine(_tempDir, "appsettings.json");
        File.WriteAllText(_validJsonFile, """{"TestKey": "TestValue", "Nested": {"Setting": 42}}""");

        _emptyJsonFile = Path.Combine(_tempDir, "empty.json");
        File.WriteAllText(_emptyJsonFile, "{}");
    }

    ~JsonConfigurationProviderTests()
    {
        try
        {
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }
        catch 
        { 
        }
    }

    #endregion

    #region Constructor_DefaultOverload

    [Fact]
    public void Constructor_DefaultOverload_UsesBaseDirectoryAndDefaultFilename_ThrowsFileNotFoundException()
    {
        // Arrange/Act/Assert
        Assert.Throws<FileNotFoundException>(() => new JsonConfigurationProvider());
    }

    #endregion

    #region Constructor_SettingsNameOverload

    [Fact]
    public void Constructor_WithSettingsName_UsesCustomFilename_ThrowsFileNotFoundException()
    {
        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => new JsonConfigurationProvider("nonexistent.json"));
    }

    #endregion

    #region Constructor_FullOverload_BasePathValidation

    [Fact]
    public void Constructor_WithEmptyBasePath_ThrowsArgumentException()
    {
        // Arrange

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new JsonConfigurationProvider("", "appsettings.json", false));
        Assert.Contains("basePath", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Constructor_WithWhitespaceBasePath_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new JsonConfigurationProvider("   ", "appsettings.json", false));
        Assert.Contains("basePath", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region Constructor_FullOverload_SettingsNameValidation

    [Fact]
    public void Constructor_WithEmptySettingsName_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new JsonConfigurationProvider(_tempDir, "", false));
        Assert.Contains("settingsName", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Constructor_WithWhitespaceSettingsName_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new JsonConfigurationProvider(_tempDir, "   ", false));
        Assert.Contains("settingsName", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region Constructor_FullOverload_ValidPaths

    [Fact]
    public void Constructor_WithValidPaths_CreatesProvider()
    {
        // Arrange/Act
        var provider = new TestableJsonConfigurationProvider(_tempDir, _validJsonFile, false);

        // Assert
        Assert.NotNull(provider);
    }

    #endregion

    #region GetConfiguration

    [Fact]
    public void GetConfiguration_ReturnsNonNullConfiguration()
    {
        // Arrange
        var provider = new TestableJsonConfigurationProvider(_tempDir, _validJsonFile, false);

        // Act
        var configuration = provider.GetConfiguration();

        // Assert
        Assert.NotNull(configuration);
    }

    [Fact]
    public void GetConfiguration_ReturnsConfigurationWithData()
    {
        // Arrange
        var provider = new TestableJsonConfigurationProvider(_tempDir, _validJsonFile, false);

        // Act
        var configuration = provider.GetConfiguration();

        // Assert
        Assert.Equal("TestValue", configuration["TestKey"]);
        Assert.Equal("42", configuration["Nested:Setting"]);
    }

    [Fact]
    public void GetConfiguration_ReturnsSameInstanceOnMultipleCalls()
    {
        // Arrange
        var provider = new TestableJsonConfigurationProvider(_tempDir, _validJsonFile, false);

        // Act
        var config1 = provider.GetConfiguration();
        var config2 = provider.GetConfiguration();

        // Assert
        Assert.Same(config1, config2);
    }

    #endregion
}
