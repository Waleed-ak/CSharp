using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tools
{
	public interface ICollection<T>:IList<T>
	{
		#region Public Methods

		ReadOnlyCollection<T> AsReadOnly();

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
