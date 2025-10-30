using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cryptocop.Services.Helpers
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeJsonToObject<T>(this HttpResponseMessage response, bool flatten = false)
        {
            var json = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(json);
            var data = jsonObject["data"];
            if (flatten)
            {
                data = FlattenJsonObject(data);
            }
            return JsonConvert.DeserializeObject<T>(data.ToString());
        }

        public static async Task<IEnumerable<T>> DeserializeJsonToList<T>(this HttpResponseMessage response, bool flatten = false)
        {
            var json = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(json);
            var data = jsonObject["data"];
            if (flatten)
            {
                data = FlattenJsonArray(data);
            }
            return JsonConvert.DeserializeObject<IEnumerable<T>>(data.ToString());
        }

        private static JToken FlattenJsonObject(JToken jsonToken)
        {
            var newJsonObject = new JObject();

            foreach (var token in jsonToken.OfType<JProperty>())
            {
                token
                    .Descendants()
                    .Where(p => !p.Any())
                    .ToList()
                    .ForEach(j =>
                    {
                        if (j.Parent is JProperty prop && !newJsonObject.ContainsKey(prop.Name))
                        {
                            newJsonObject.Add(prop.Name, prop.Value);
                        }
                    });
            }

            return newJsonObject;
        }

        private static JToken FlattenJsonArray(JToken jsonToken)
        {
            var newJsonArray = new JArray();

            foreach (var token in jsonToken.OfType<JObject>())
            {
                var jObject = new JObject();

                token
                    .Descendants()
                    .Where(p => !p.Any())
                    .ToList()
                    .ForEach(j =>
                    {
                        if (j.Parent is JProperty prop && !jObject.ContainsKey(prop.Name))
                        {
                            jObject.Add(prop.Name, prop.Value);
                        }
                    });

                newJsonArray.Add(jObject);
            }

            return newJsonArray;
        }
    }
}
