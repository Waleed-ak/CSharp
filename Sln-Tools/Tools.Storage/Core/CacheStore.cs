using System;

namespace Tools
{
	public class CacheStore<M>:ICacheStore<M>
	{
		#region Public Constructors

		public CacheStore(ICacheStore cacheStore) => Store = cacheStore;

		#endregion Public Constructors

		#region Public Properties
		public string AppName { get; set; }
		public TimeSpan CacheDuration { get; set; }
		public ICacheStore Store { get; }

		public Func<string> Token { get; set; }
		#endregion Public Properties

		#region Public Methods

		public bool Exists(string key) => Store.Exists<M>(key);

		public bool Exists<T>(string key) => Store.Exists<T>(key);

		public M Get(string key) => Store.Get<M>(key);

		public T Get<T>(string key) => Store.Get<T>(key);

		public bool Remove(string key) => Store.Remove<M>(key);

		public bool Remove<T>(string key) => Store.Remove<T>(key);

		public void Set(string key,M value,TimeSpan timeSpan) => Store.Set(key,value,timeSpan);

		public void Set<T>(string key,T value,TimeSpan timeSpan) => Store.Set(key,value,timeSpan);

		#endregion Public Methods
	}
}
