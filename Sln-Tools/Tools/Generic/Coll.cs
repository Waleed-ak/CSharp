using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
	public class Coll<T>:IColl<T>
	{
		#region Private Fields
		private List<T> Items = new List<T>();
		#endregion Private Fields

		#region Public Constructors

		public Coll()
		{
		}

		public Coll(IEnumerable<T> collection) => Items.AddRange(collection);

		#endregion Public Constructors

		#region Public Properties
		public int Count => Items.Count;

		public bool IsReadOnly => false;
		#endregion Public Properties

		#region Public Indexers
		public T this[int index] { get => Items[index]; set => Items[index] = value; }
		#endregion Public Indexers

		#region Public Methods

		public void Add(T item) => Items.Add(item);

		public void AddRange(IEnumerable<T> items) => Items.AddRange(items);

		public IEnumerable<T> AsReadOnly() => Items.AsReadOnly();

		public void Clear() => Items.Clear();

		public IColl<T> Clone() => FormatConvert.DeserializeJson<Coll<T>>(this.SerializeJson());

		public bool Contains(T item) => Items.Contains(item);

		public void CopyTo(T[] array,int arrayIndex) => Items.CopyTo(array,arrayIndex);

		public bool Exists(Func<T,bool> match) => Items.Any(match);

		public List<T> FindAll(Func<T,bool> match) => Items.FindAll(c => match(c));

		public T FirstOrDefault(Func<T,bool> match) => Items.Find(c => match(c));

		public void ForEach(Action<T> action) => Items.ForEach(action);

		public void FromJson(string val) => Items = FormatConvert.DeserializeJson<List<T>>(val);

		public void FromXml(string val) => Items = FormatConvert.DeserializeXml<List<T>>(val);

		public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

		public int IndexOf(T item) => Items.IndexOf(item);

		public void Insert(int index,T item) => Items.Insert(index,item);

		public void Remove(T item) => Items.Remove(item);

		bool ICollection<T>.Remove(T item) => Items.Remove(item);

		public void RemoveAt(int index) => Items.RemoveAt(index);

		public string ToJson() => Items.SerializeJson();

		public string ToXml(bool removeNameSpace = false,EnEncoding encoding = EnEncoding.UTF8) => Items.SerializeXml(removeNameSpace,encoding);

		#endregion Public Methods
	}
}
