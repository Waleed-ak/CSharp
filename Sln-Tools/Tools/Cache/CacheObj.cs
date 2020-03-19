using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Tools
{
	public static class CacheObj
	{
		#region Private Fields
		private static readonly MemoryCache Mem = new MemoryCache("CacheObj");
		#endregion Private Fields

		#region Public Methods

		public static IEnumerable<KeyValuePair<string,object>> AsEnumerable() => Mem.AsEnumerable();

		public static void Clear(string key = null)
		{
			if(key is null)
			{
				foreach(var item in GetKeys())
				{
					Mem.Remove(item);
				}
			}
			else
			{
				Mem.Remove(key);
			}
		}

		public static void ClearCascade(string key)
		{
			foreach(var item in GetKeys())
			{
				if(item?.StartsWith(key) == true)
				{
					Mem.Remove(item);
				}
			}
		}

		public static object Get(string key) => Mem.Get(key);

		public static IEnumerator<KeyValuePair<string,object>> GetEnumerator() => GetValues().GetEnumerator();

		public static string[] GetKeys() => Mem.Select(kvp => kvp.Key).ToArray();

		public static IDictionary<string,object> GetValues(IEnumerable<string> keys = null) => keys is null ? AsEnumerable().ToDictionary(keySelector: c => c.Key,elementSelector: c => c.Value) : Mem.GetValues(keys);

		public static bool InCache(string key) => Mem.Contains(key);

		public static TValue SetGet<TValue>(string key,Func<TValue> func)
		{
			var obj = Mem.Get(key);
			if(obj is null)
			{
				Mem.Set(key,obj = func(),DateTimeOffset.UtcNow.AddDays(10));
			}
			return (TValue)obj;
		}

		public static TValue SetGetAbsolute<TValue>(string key,Func<TValue> func,TimeSpan timeSpan)
		{
			var obj = Mem.Get(key);
			if(obj is null)
			{
				Mem.Set(key,obj = func(),DateTimeOffset.UtcNow + timeSpan);
			}
			return (TValue)obj;
		}

		public static TValue SetGetSliding<TValue>(string key,Func<TValue> func,TimeSpan timeSpan)
		{
			var obj = Mem.Get(key);
			if(obj is null)
			{
				Mem.Set(key,obj = func(),new CacheItemPolicy { SlidingExpiration = timeSpan });
			}
			return (TValue)obj;
		}

		#endregion Public Methods
	}
}
