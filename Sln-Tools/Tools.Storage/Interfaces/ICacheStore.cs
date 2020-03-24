using System;

namespace Tools
{
	public interface ICacheStore
	{
		#region Public Properties
		string AppName { get; set; }
		TimeSpan CacheDuration { set; get; }
		Func<string> Token { get; set; }
		#endregion Public Properties

		#region Public Methods

		bool Exists<T>(string key);

		T Get<T>(string key);

		bool Remove<T>(string key);

		void Set<T>(string key,T value,TimeSpan timeSpan);

		#endregion Public Methods
	}
}
