﻿using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;

namespace SmartTaskbar.PlatformInvoke
{
    public class UserConfigService : IUserConfigService
    {
        private readonly JsonSerializerOptions _options;
        private readonly string _userConfigPath;

        public UserConfigService()
        {
            _userConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                           Constants.ApplicationName,
                                           "SmartTaskbar.json");

            _options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            _options.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<UserConfiguration> ReadSettingsAsync()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_userConfigPath)!);


            await using var fs = new FileStream(_userConfigPath, FileMode.OpenOrCreate);
            try
            {
                // todo UserConfiguration load default settings.
                return await JsonSerializer.DeserializeAsync<UserConfiguration>(fs, _options)
                       ?? new UserConfiguration();
            }
            catch
            {
                // clear the contents of the file without creating a new file.
                fs.SetLength(0);
                // return default setting if Deserialization process failed.
                return new UserConfiguration();
            }
        }

        public async Task SaveSettingsAsync(UserConfiguration configuration)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_userConfigPath)!);

            await using var fs = new FileStream(_userConfigPath, FileMode.OpenOrCreate);

            // clear the contents of the file without creating a new file.
            fs.SetLength(0);

            await JsonSerializer.SerializeAsync(fs, configuration, _options);
        }
    }
}
