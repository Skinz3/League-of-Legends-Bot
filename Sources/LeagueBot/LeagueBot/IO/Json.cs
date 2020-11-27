using Newtonsoft.Json;

namespace LeagueBot.IO
{
    public class Json
    {
        public static T Deserialize<T>(string content) where T : class
        {
            return (T)JsonConvert.DeserializeObject<T>(content);
        }

        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}