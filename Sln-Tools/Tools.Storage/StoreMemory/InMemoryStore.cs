using System;
using System.Runtime.Caching;

namespace Tools
{
	public class InMemoryStore:ICacheStore
	{
		#region Private Fields
		private static readonly ObjectCache _Cache = MemoryCache.Default;
		private static readonly object _locker = new object();
		#endregion Private Fields

		#region Public Properties
		public string AppName { get; set; } = "App";

		public TimeSpan CacheDuration { get; set; }
		public Func<string> Token { get; set; }
		#endregion Public Properties

		#region Public Methods

		public bool Exists<T>(string key) => _Cache[this.GetKey<T>(key)] != null;

		public T Get<T>(string key)
		{
			try
			{
				return (T)_Cache[this.GetKey<T>(key)];
			}
			catch
			{
				return default;
			}
		}

		public bool Remove<T>(string key) => !(_Cache.Remove(this.GetKey<T>(key)) is null);

		public void Set<T>(string key,T value,TimeSpan timeSpan)
		{
			try
			{
				if(key.CheckKey())
					throw new ArgumentException($"Invalid cache {nameof(key)}");
				if(value is null)
				{
					return;
				}
				lock(_locker)
				{
					_Cache.Set(key: this.GetKey<T>(key),value: value,policy: new CacheItemPolicy { SlidingExpiration = timeSpan });
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion Public Methods
	}
}
