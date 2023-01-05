using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.IO;

namespace Battleship2000.Logic
{
    internal static class Configuration
    {
        private static readonly string configPath = Path.Combine(Path.GetDirectoryName(typeof(Configuration).Assembly.Location), "config.json");
        public static void Load()
        {
            if (!File.Exists(configPath))
            {
                Log.Information($"[Configuration] No config found, creating new with default values");
                Save();
                return;
            }

            string json = null;

            using (FileStream fs = File.Open(configPath, new FileStreamOptions() { BufferSize = 128, Share = FileShare.Read, Mode = FileMode.Open }))
            {
                using (StreamReader r = new(fs))
                {
                    json = r.ReadToEnd();
                }
            }

            if (!IsValidJson(json))
            {
                Log.Warning($"[Configuration] Invalid config file found");
                Save();
                return;
            }

            ObjectStorage.Config = JsonConvert.DeserializeObject<Models.Configuration>(json);

            Log.Information($"[Configuration] Config loaded");
        }

        public static void Save()
        {
            if (!File.Exists(configPath))
            {
                TouchFile(configPath);
            }

            using (FileStream fs = File.Open(configPath, new FileStreamOptions() { BufferSize = 128, Share = FileShare.ReadWrite, Mode = FileMode.Truncate, Access = FileAccess.ReadWrite }))
            {
                using (StreamWriter w = new(fs))
                {
                    w.Write(JsonConvert.SerializeObject(ObjectStorage.Config, Formatting.Indented));
                }
            }

            Log.Information($"[Configuration] Config saved");
        }

        private static void TouchFile(string filepath)
        {
            File.Create(filepath).Dispose();
        }

        private static bool IsValidJson(string json)
        {
            try
            {
                JObject.Parse(json);
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
    }
}
