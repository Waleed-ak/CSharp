using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Tools
{
	internal static class ConverterWrapper
	{
		#region Private Fields
		private static readonly JsonSerializerSettings _JsonSerializerSettings = new() { NullValueHandling = NullValueHandling.Ignore,DefaultValueHandling = DefaultValueHandling.Ignore };
		#endregion Private Fields

		#region Public Methods

		public static T DeserializeJson<T>(string value)
					=> value == null ? default : JsonConvert.DeserializeObject<T>(value: value);

		public static T DeserializeXml<T>(string value)
		{
			if(value.IsNullOrWhiteSpace())
				return default;
			var result = default(T);
			var serial = new XmlSerializer(typeof(T));
			using(var reader = new StringReader(value))
			{
				result = (T)serial.Deserialize(reader);
			}
			return result;
		}

		public static string SerializeJson<T>(T value)
				=> value is null ? null : JsonConvert.SerializeObject(value: value,formatting: Formatting.Indented,settings: _JsonSerializerSettings);

		public static string SerializeXml<T>(T value,bool removeNameSpace = false,EnEncoding encoding = EnEncoding.UTF8)
		{
			if(value is null)
				return null;
			string ret;
			using(var stream = new MemoryStream())
			{
				var enc = encoding switch
				{
					EnEncoding.ASCII => Encoding.ASCII,
					EnEncoding.Default => Encoding.Default,
					EnEncoding.Unicode => Encoding.Unicode,
					EnEncoding.UTF32 => Encoding.UTF32,
					EnEncoding.UTF7 => Encoding.UTF7,
					EnEncoding.UTF8 => Encoding.UTF8,
					_ => Encoding.UTF8,
				};
				using var xmlWriter = new StreamWriter(stream: stream,encoding: enc);
				XmlSerializerNamespaces xns = null;
				if(removeNameSpace)
				{
					xns = new XmlSerializerNamespaces();
					xns.Add(string.Empty,string.Empty);
				}
				var xmlSerializer = new XmlSerializer(typeof(T));
				xmlSerializer.Serialize(xmlWriter,value,xns);
				stream.Position = 0;
				using var sr = new StreamReader(stream);
				ret = sr.ReadToEnd();
			}
			return ret;
		}

		#endregion Public Methods
	}
}
