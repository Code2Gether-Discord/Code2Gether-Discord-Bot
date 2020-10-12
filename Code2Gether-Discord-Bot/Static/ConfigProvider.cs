using Code2Gether_Discord_Bot.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Code2Gether_Discord_Bot.Static
{
    public static class ConfigProvider
    {
        private static FileInfo configFile = new FileInfo("config.json");
        public static IConfig GetConfig()
        {
            IConfig config = new Config("c!", "PLACEHOLDER"); ;
            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile.FullName));
            }
            catch (Exception e)
            {
                GenerateNewConfig(config);
            }

            return config;
        }

        private static void GenerateNewConfig(IConfig config)
        {
            var configJson = JsonConvert.SerializeObject(config);
            configFile.Delete();
            File.WriteAllText(configFile.FullName, configJson);
            configFile = new FileInfo(configFile.FullName);
        }
    }
}
