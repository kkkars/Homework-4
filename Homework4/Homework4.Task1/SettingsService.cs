using System.IO;
using System.Text.Json;

namespace Homework4.Task1
{
    public class SettingsService
    {
        private readonly string _settingFileName;
        private Settings _settings = null;
        public SettingsService(string settingFileName)
        {
            _settingFileName = settingFileName;
        }

        public Settings GetSettings()
        {
            if (_settings != null) return _settings;
            var json = File.ReadAllText(_settingFileName);
            _settings = JsonSerializer.Deserialize<Settings>(json);
            return _settings;
        }
    }
}
