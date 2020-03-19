using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
	public static partial class Ext
	{
		#region Public Methods

		public static bool In<T>(this T @this,IEnumerable<T> list) where T : struct => list.Any(c => Equals(c,@this));

		public static bool In<T>(this T @this,params T[] list) => list.Any(c => Equals(c,@this));

		public static T ToEnum<T>(int @this) where T : struct
		{
			if(!typeof(T).IsEnum)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			return (T)Enum.ToObject(typeof(T),@this);
		}

		public static T ToEnum<T>(string @this) where T : struct
		{
			if(!typeof(T).IsEnum)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			return (T)Enum.Parse(typeof(T),@this);
		}

		public static int ToInt<T>(this T @this) where T : struct
		{
			if(!typeof(T).IsEnum)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			return Convert.ToInt32(@this);
		}

		#endregion Public Methods
	}
}