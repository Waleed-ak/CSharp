using System;
using System.Runtime.Caching;

namespace Tools
{
	public class InMemoryThroughStore:ICacheStore
	{
		#region Private Fields
		private static readonly ObjectCache _Cache = MemoryCache.Default;
		private static readonly object _locker = new object();
		private readonly ICacheStore _CacheMain;
		private readonly TimeSpan _SlidingExpiration;
		#endregion Private Fields

		#region Public Constructors

		public InMemoryThroughStore(ICacheStore mainCache,int durationInSec = 0)
		{
			_CacheMain = mainCache;
			_SlidingExpiration = TimeSpan.FromSeconds(durationInSec > 0 ? durationInSec : 45);
		}

		#endregion Public Constructors

		#region Public Properties
		public string AppName { get; set; } = "App";

		public TimeSpan CacheDuration { get; set; }

		public Func<string> Token { get; set; }
		#endregion Public Properties

		#region Public Methods

		public bool Exists<T>(string key) => _CacheMain.Exists<T>(key);

		public T Get<T>(string key)
		{
			try
			{
				var ret = GetInMemory<T>(key);
				if(!(ret is null))
				{
					return ret;
				}
				var RretMain = _CacheMain.Get<T>(key);
				if(!(RretMain is null))
				{
					SetInMemory(key,RretMain);
				}

				return RretMain;
			}
			catch(Exception)
			{
				throw;
			}
		}

		public bool Remove<T>(string key)
		{
			_Cache.Remove(this.GetKey<T>(key));
			return _CacheMain.Remove<T>(key);
		}

		public void Set<T>(string key,T value)
		{
			try
			{
				if(key.CheckKey())
					throw new ArgumentException($"Invalid cache {nameof(key)}");
				SetInMemory(key,value);
				_CacheMain.Set(key,value);
			}
			catch(Exception)
			{
				throw;
			}
		}

		public void Set<T>(string key,T value,TimeSpan timeSpan)
		{
			try
			{
				if(key.CheckKey())
					throw new ArgumentException($"Invalid cache {nameof(key)}");
				SetInMemory(key,value,timeSpan);
				_CacheMain.Set(key,value,timeSpan);
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion Public Methods

		#region Private Methods

		private T GetInMemory<T>(string key)
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

		private void SetInMemory<T>(string key,T value,TimeSpan timeSpan)
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
					_Cache.Set(key: this.GetKey<T>(key),value: value,policy: new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now + timeSpan });
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		private void SetInMemory<T>(string key,T value)
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
					_Cache.Set(key: this.GetKey<T>(key),value: value,policy: new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.Add(_SlidingExpiration) });
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion Private Methods
	}
}
