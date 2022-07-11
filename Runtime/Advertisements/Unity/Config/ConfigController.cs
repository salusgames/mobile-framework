using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace SalusGames.MobileFramework.Advertisements.Unity.Config
{
    public static class ConfigController
    {
        private static string _jsonFilePath;
        private static ConfigData _configData;

        static ConfigController()
        {
            _jsonFilePath = Path.Combine(Application.persistentDataPath, "UnityAdsManagerConfig.json");
            CreateFile();
            LoadFile();
        }

        public static bool DisableAds
        {
            get => _configData.DisableAds;
            set => _configData.DisableAds = value;
        }

        private static void Save()
        {
            var jsonSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented};
            var jsonString = JsonConvert.SerializeObject(_configData, jsonSettings);

            var streamWriter = new StreamWriter(_jsonFilePath);
            streamWriter.Write(jsonString);
            streamWriter.Close();

            Debug.Log("Salus Games Unity Ad Manager: Saved config data");
        }

        private static void CreateFile()
        {
            if (!File.Exists(_jsonFilePath))
            {
                Debug.Log("Salus Games Unity Ad Manager: Config data file not found, creating.");
                
                _configData = new ConfigData();
                Save();
            }
        }
        
        private static void LoadFile()
        {
            var streamWriter = new StreamReader(_jsonFilePath);
            var jsonString = streamWriter.ReadToEnd();
            streamWriter.Close();
            _configData = JsonConvert.DeserializeObject<ConfigData>(jsonString);
            Debug.Log("Salus Games Unity Ad Manager: Loaded config data");
        }
    }
}