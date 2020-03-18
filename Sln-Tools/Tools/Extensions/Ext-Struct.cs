using System.Collections.Generic;
using System.Linq;

namespace Tools
{
	public static partial class Ext
	{
		#region Public Methods

		public static bool In<T>(this T @this,IEnumerable<T> list) where T : struct
			=> list.Any(c => Equals(c,@this));

		public static bool In<T>(this T @this,params T[] list)
			=> list.Any(c => Equals(c,@this));

		#endregion Public Methods
	}
}