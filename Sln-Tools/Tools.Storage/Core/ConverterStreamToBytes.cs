using System;
using System.IO;
using Newtonsoft.Json;

namespace Tools
{
  public class ConverterStreamToBytes:JsonConverter
  {
    #region Public Methods

    public override bool CanConvert(Type objectType) => typeof(Stream).IsAssignableFrom(objectType);

    public override object ReadJson(JsonReader reader,Type objectType,object existingValue,JsonSerializer serializer)
    {
      var bytes = serializer.Deserialize<byte[]>(reader);
      return bytes != null ? new MemoryStream(bytes) : new MemoryStream();
    }

    public override void WriteJson(JsonWriter writer,object value,JsonSerializer serializer)
    {
      using(var mem = new MemoryStream())
      {
        var val = ((Stream)value);
        val.Position = 0;
        val.CopyTo(mem);
        serializer.Serialize(writer,mem.ToArray());
      }
    }

    #endregion Public Methods
  }
}
