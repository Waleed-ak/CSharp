using System;

namespace Tools
{
	public interface ICustom<TValue> where TValue : IComparable
	{
		#region Public Properties
		TValue Value { get; set; }
		#endregion Public Properties

		#region Public Methods

		bool Equals(object obj);

		int GetHashCode();

		string ToString();

		#endregion Public Methods
	}
}
