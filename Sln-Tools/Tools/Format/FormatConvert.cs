namespace Tools
{
	public static class FormatConvert
	{
		#region Public Methods

		public static T DeserializeJson<T>(string value)
			=> ConverterWrapper.DeserializeJson<T>(value);

		public static T DeserializeXml<T>(string value)
			=> ConverterWrapper.DeserializeXml<T>(value);

		public static T ObjectMapper<T>(this object value)
			=> ConverterWrapper.DeserializeJson<T>(SerializeJson(value));

		public static string SerializeJson<T>(this T value)
			=> ConverterWrapper.SerializeJson(value);

		public static string SerializeXml<T>(this T value,bool removeNameSpace = false,EnEncoding encoding = EnEncoding.UTF8)
			=> ConverterWrapper.SerializeXml(value,removeNameSpace,encoding);

		#endregion Public Methods
	}
}
