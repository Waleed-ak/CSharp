using System;

namespace Tools
{
	public interface ICacheStore<M>:ICacheStore
	{
		#region Public Properties
		ICacheStore Store { get; }
		#endregion Public Properties

		#region Public Methods

		bool Exists(string key);

		M Get(string key);

		bool Remove(string key);

		void Set(string key,M value,TimeSpan timeSpan);

		#endregion Public Methods
	}
}
