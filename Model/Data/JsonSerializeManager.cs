using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Model.Data
{
    public class JsonSerializeManager<T> : SerializeManager<T> where T : class
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializeManager(string folderPath, string fileName) : base("JsonManager", folderPath, fileName, "json")
        {
            _settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
        }

        public override void Serialize(IEnumerable<T> items, string path)
        {
            string json = JsonConvert.SerializeObject(items, _settings);
            File.WriteAllText(path, json);
        }

        public override IEnumerable<T> Deserialize(string path)
        {
            if (!File.Exists(path))
                return new List<T>();
            
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<T>>(json, _settings) ?? new List<T>();
        }
    }
}