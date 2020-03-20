using System;
using Newtonsoft.Json;

namespace Tools
{
	public class CustomConverter<TCustom, TValue>:JsonConverter<TCustom>
		where TCustom : Custom<TCustom,TValue>, new()
		where TValue : IComparable
	{
		#region Public Methods



		#endregion Public Methods
		public override TCustom ReadJson(JsonReader reader,Type objectType,TCustom existingValue,bool hasExistingValue,JsonSerializer serializer)
		{
			var val = (TValue)Convert.ChangeType(reader.Value,typeof(TValue));
			return new TCustom() { Value = val };
		}

		public override void WriteJson(JsonWriter writer,TCustom value,JsonSerializer serializer) => writer.WriteValue(value.Value);
	}
}
