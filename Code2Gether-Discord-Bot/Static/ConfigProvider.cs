using Code2Gether_Discord_Bot.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Code2Gether_Discord_Bot.Static
{
    public static class ConfigProvider
    {
        private static FileInfo _configFile = new FileInfo("config.json");
        public static IConfig GetConfig()
        {
            // Default values - Do not change
            IConfig config = new Config("c!", "PLACEHOLDER", true, "https://localhost:5001", "PLACEHOLDER", "Code2Gether-Discord");

            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configFile.FullName));
            }
            catch (Exception)
            {
                GenerateNewConfig(config);
            }

            return config;
        }

        private static void GenerateNewConfig(IConfig config)
        {
            var configJson = JsonConvert.SerializeObject(config);
            _configFile.Delete();
            File.WriteAllText(_configFile.FullName, configJson);
            _configFile = new FileInfo(_configFile.FullName);
        }
    }
}
