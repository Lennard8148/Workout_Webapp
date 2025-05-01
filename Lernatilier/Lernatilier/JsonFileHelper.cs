using System.Text.Json;

namespace FitnessTrackerAPI.Helpers
{
    public static class JsonFileHelper
    {
        public static List<T> Load<T>(string path)
        {
            if (!File.Exists(path))
                return new List<T>();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void Save<T>(string path, List<T> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}
