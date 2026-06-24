using Microsoft.Extensions.Configuration;
using OSK.Petra.Modules.Services.Ports;
using System;

namespace OSK.Petra.Modules.Services.Configuration.Json;

/// <summary>
/// An <see cref="IModuleConfigurationProvider"/> that gets configuration from a Json file in the project
/// </summary>
public class JsonConfigurationProvider : IModuleConfigurationProvider
{
    #region Variables

    private readonly IConfiguration _configuration;

    #endregion

    #region Constructors

    /// <summary>
    /// Create a json configuration provider that uses default settings for the base path and app settings file name
    /// </summary>
    /// <param name="reloadOnChange">Whether the configuration should be updated if the file changes</param>
    public JsonConfigurationProvider(bool reloadOnChange = false)
        : this(AppContext.BaseDirectory, "appsettings.json", reloadOnChange)
    {
    }

    /// <summary>
    /// Creates a json configuration provider that uses default settings for the base path and a custom app settings file name
    /// </summary>
    /// <param name="settingsName">The appsettings file name to use</param>
    /// <param name="reloadOnChange">Whether the configuration should be updated if the file changes</param>
    public JsonConfigurationProvider(string settingsName, bool reloadOnChange = false)
        : this(AppContext.BaseDirectory, settingsName, reloadOnChange)
    {
    }

    /// <summary>
    /// Creates a json configuration using custom settings for both the base path and app settings file name
    /// </summary>
    /// <param name="basePath">The base path that is used when getting the app settings</param>
    /// <param name="settingsName">The appsettings file name to use</param>
    /// <param name="reloadOnChange">Whether the configuration should be updated if the file changes</param>
    /// <exception cref="ArgumentException"></exception>
    public JsonConfigurationProvider(string basePath, string settingsName, bool reloadOnChange)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            throw new ArgumentException($"{nameof(basePath)} can not be empty.");
        }
        if (string.IsNullOrWhiteSpace(settingsName))
        {
            throw new ArgumentException($"{nameof(settingsName)} can not be empty.");
        }

        _configuration = CreateConfigurationFromJson(basePath, settingsName, reloadOnChange);
    }

    #endregion

    #region IModuleConfigurationProvider

    /// <inheritdoc/>
    public IConfiguration GetConfiguration()
        => _configuration;

    #endregion

    #region Helpers

    private IConfiguration CreateConfigurationFromJson(string basePath, string settingsName, bool reloadOnChange)
        => new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(settingsName, optional: false, reloadOnChange: reloadOnChange)
                .Build();

    #endregion
}
