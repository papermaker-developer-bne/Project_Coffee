using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Coffee.API.Processor
{
    public class JsonHelper
    {
        private string _jsonName;
        private string _path;
        private IConfiguration Configuration { get; set; }
        public JsonHelper(string jsonName)
        {
            _jsonName = jsonName;
            if (!jsonName.EndsWith(".json"))
                _path = $"{jsonName}.json";
            else
                _path = jsonName;
            //ReloadOnChange = true. Reload when JSON file is modified
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = _path, ReloadOnChange = true, Optional = true })
            .Build();
        }

        /// <summary>
        /// Read JSON and return objects
        /// </summary>
        /// <returns></returns>
        public T Read<T>() => Read<T>("");

        /// <summary>
        /// Read JSON and return objects based on section
        /// </summary>
        /// <returns></returns>
        public T Read<T>(string section)
        {
            try
            {
                using (var file = new StreamReader(_path))
                using (var reader = new JsonTextReader(file))
                {
                    var jObj = (JArray)JToken.ReadFrom(reader);
                    if (!string.IsNullOrWhiteSpace(section))
                    {
                        var secJt = jObj[section];
                        if (secJt != null)
                        {
                            return JsonConvert.DeserializeObject<T>(secJt.ToString());
                        }
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<T>(jObj.ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return default(T);
        }

        /// <summary>
        /// Read from JSON and return list
        /// </summary>
        /// <returns></returns>
        public List<T> ReadList<T>() => ReadList<T>("");

        /// <summary>
        /// Read from JSON and return list based on section
        /// </summary>
        /// <returns></returns>
        public List<T> ReadList<T>(string section)
        {
            try
            {
                using (var file = new StreamReader(_path))
                using (var reader = new JsonTextReader(file))
                {
                    var jObj = (JObject)JToken.ReadFrom(reader);
                    if (!string.IsNullOrWhiteSpace(section))
                    {
                        var secJt = jObj[section];
                        if (secJt != null)
                        {
                            return JsonConvert.DeserializeObject<List<T>>(secJt.ToString());
                        }
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<T>>(jObj.ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return default(List<T>);
        }

    }
}
