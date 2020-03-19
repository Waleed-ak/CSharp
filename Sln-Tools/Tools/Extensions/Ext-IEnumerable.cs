using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace Tools
{
	public static partial class Ext
	{
		#region Public Methods

		public static M Cast<T, M>(this IEnumerable<T> input) where M : System.Collections.Generic.ICollection<T>, new()
		{
			var ret = new M();
			foreach(var item in input ?? Enumerable.Empty<T>())
			{
				ret.Add(item);
			}
			return ret;
		}

		public static IEnumerable<T> Dump<T>(this IEnumerable<T> items)
		{
			foreach(var item in items)
			{
				Debug.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
				yield return item;
			}
		}

		public static T FirstOrNew<T>(this IEnumerable<T> items) where T : new() => items.FirstOrDefault() ?? new T();

		public static void ForEach<T>(this IEnumerable<T> items,Action<T> action)
		{
			foreach(var item in items.ToArray())
			{
				action(item);
			}
		}

		#endregion Public Methods
	}
}
