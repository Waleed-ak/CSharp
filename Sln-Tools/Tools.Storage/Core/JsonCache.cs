using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tools
{
  [Serializable]
  public class JsonCache
  {
    #region Private Fields

    private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore,
      DefaultValueHandling = DefaultValueHandling.Ignore,
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    };

    #endregion Private Fields

    #region Public Constructors

    static JsonCache()
    {
      JsonConvert.DefaultSettings = () => new JsonSerializerSettings
      {
        Converters = new List<JsonConverter> { new ConverterStreamToBytes() }
      };
    }

    #endregion Public Constructors
    #region Public Properties
    public string Data { get; set; }
    #endregion Public Properties

    #region Public Methods

    public static T Deserialize<T>(string input) => input == null ? default(T) : JsonConvert.DeserializeObject<T>(input);

    public static T Get<T>(JsonCache obj) => JsonConvert.DeserializeObject<T>(obj?.Data);

    public static JsonCache Instance<T>(T obj) => new JsonCache { Data = JsonConvert.SerializeObject(obj,JsonSettings) };

    public static string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj,JsonSettings);

    #endregion Public Methods
  }
}
