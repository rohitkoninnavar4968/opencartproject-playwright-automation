using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Opencart_Automation_Project.Config;

public static class ConfigReader
{
    public static TestSettings ReadConfig()
    {
        // Try several likely base directories, pick the first one that contains appsettings.json
        var candidateDirs = new[]
        {
            AppContext.BaseDirectory,
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
            Directory.GetCurrentDirectory()
        };

        string configPath = null;
        foreach (var dir in candidateDirs)
        {
            if (string.IsNullOrWhiteSpace(dir)) continue;
            var p = Path.Combine(dir, "appsettings.json");
            if (File.Exists(p))
            {
                configPath = p;
                break;
            }
        }

        if (configPath == null)
        {
            throw new FileNotFoundException(
                "Could not find appsettings.json. Searched: " + string.Join(", ", candidateDirs.Where(d => !string.IsNullOrEmpty(d)))
            );
        }

        var configFile = File.ReadAllText(configPath);

        var jsonSerializerSettings = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        jsonSerializerSettings.Converters.Add(new JsonStringEnumConverter());

        return JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerializerSettings);
    }
}