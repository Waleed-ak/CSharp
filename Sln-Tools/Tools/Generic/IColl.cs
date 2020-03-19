using System;
using System.Collections.Generic;

namespace Tools
{
	public interface IColl<T>:IList<T>
	{
		#region Public Methods

		IEnumerable<T> AsReadOnly();

		IColl<T> Clone();

		bool Exists(Func<T,bool> match);

		List<T> FindAll(Func<T,bool> match);

		T FirstOrDefault(Func<T,bool> match);

		void ForEach(Action<T> action);

		void FromJson(string val);

		void FromXml(string val);

		string ToJson();

		string ToXml(bool removeNameSpace = false,EnEncoding encoding = EnEncoding.UTF8);

		#endregion Public Methods
	}
}
