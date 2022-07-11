using System;
using System.Collections.Generic;

namespace Tools
{
	public class Custom<TCustom, TValue>:ICustom<TValue> where TCustom : Custom<TCustom,TValue>
		where TValue : IComparable
	{
		#region Public Constructors

		public Custom(TValue value = default) => Value = value;

		#endregion Public Constructors

		#region Public Properties
		public TValue Value { get; set; }
		#endregion Public Properties

		#region Public Methods

		public static implicit operator TValue(Custom<TCustom,TValue> custom) => custom.Value;

		public static TCustom operator -(Custom<TCustom,TValue> l,TCustom r)
			=> (dynamic)l.Value - r.Value;

		public static TCustom operator -(Custom<TCustom,TValue> l,TValue r)
			=> (dynamic)l.Value - r;

		public static bool operator !=(Custom<TCustom,TValue> l,TCustom r)
			=> !(l == r);

		public static bool operator !=(Custom<TCustom,TValue> l,TValue r)
			=> !(l == r);

		public static TCustom operator +(Custom<TCustom,TValue> l,TCustom r)
			=> (dynamic)l.Value + r.Value;

		public static TCustom operator +(Custom<TCustom,TValue> l,TValue r)
			=> (dynamic)l.Value + r;

		public static bool operator <(Custom<TCustom,TValue> l,TCustom r)
			=> l.Value.CompareTo(r.Value) < 0;

		public static bool operator <(Custom<TCustom,TValue> l,TValue r)
			=> Comparer<TValue>.Default.Compare(l.Value,r) < 0;

		public static bool operator <=(Custom<TCustom,TValue> l,TCustom r)
			=> !(l > r);

		public static bool operator <=(Custom<TCustom,TValue> l,TValue r)
			=> !(l > r);

		public static bool operator ==(Custom<TCustom,TValue> l,TCustom r)
			=> Comparer<TValue>.Default.Compare(l.Value,r.Value) == 0;

		public static bool operator ==(Custom<TCustom,TValue> l,TValue r)
			=> Comparer<TValue>.Default.Compare(l.Value,r) == 0;

		public static bool operator >(Custom<TCustom,TValue> l,TCustom r)
			=> Comparer<TValue>.Default.Compare(l.Value,r.Value) > 0;

		public static bool operator >(Custom<TCustom,TValue> l,TValue r)
			=> Comparer<TValue>.Default.Compare(l.Value,r) > 0;

		public static bool operator >=(Custom<TCustom,TValue> l,TCustom r) => !(l < r);

		public static bool operator >=(Custom<TCustom,TValue> l,TValue r) => !(l < r);

		public override bool Equals(object obj)
		{
			if(obj is null)
				return false;
			if(ReferenceEquals(this,obj))
				return true;
			if(obj.GetType() != GetType())
				return false;
			return Equals((Custom<TCustom,TValue>)obj);
		}

		public override int GetHashCode() => Value.GetHashCode();

		public override string ToString() => Value.ToString();

		#endregion Public Methods

		#region Protected Methods

		protected bool Equals(Custom<TCustom,TValue> other) => EqualityComparer<TValue>.Default.Equals(Value,other.Value);

		#endregion Protected Methods
	}
}
