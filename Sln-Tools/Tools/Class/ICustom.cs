using System;

namespace Tools
{
  public interface ICustom<TValue> where TValue : IComparable
  {
	TValue Value { get; set; }

	bool Equals(object obj);
	int GetHashCode();
	string ToString();
  }
}