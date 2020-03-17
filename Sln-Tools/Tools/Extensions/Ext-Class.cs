using System;
using System.Collections.Generic;

namespace Tools
{
	public static partial class Ext
	{
		#region Public Methods

		public static T With<T>(this T @this,Action<T> action)
			where T : class
		{
			action(@this);
			return @this;
		}

		public static T[] WithItem<T>(this T[] @this,Action<T> action) where T : class => @this.WithItem<T[],T>(action);

		public static List<T> WithItem<T>(this List<T> @this,Action<T> action) where T : class => @this.WithItem<List<T>,T>(action);

		public static IEnumerable<T> WithItem<T>(this IEnumerable<T> @this,Action<T> action) where T : class => @this.WithItem<IEnumerable<T>,T>(action);

		#endregion Public Methods

		#region Private Methods

		private static M WithItem<M, T>(this M @this,Action<T> action)
					where M : IEnumerable<T>
			where T : class
		{
			foreach(var item in @this)
			{
				action(item);
			}

			return @this;
		}

		#endregion Private Methods
	}
}
