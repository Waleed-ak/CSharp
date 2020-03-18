using System.Collections.Generic;
using System.Linq;

namespace Tools
{
	public static partial class Ext
	{
		#region Public Methods

		public static string Clean(this string @this) => @this?.Trim() ?? "";

		public static bool EqualsIgnoreCase(this string @this,string other) => string.Equals(@this?.Trim(),other?.Trim(),System.StringComparison.OrdinalIgnoreCase);

		public static bool In(this string @this,IEnumerable<string> list)
							=> list.Any(c => string.Equals(c?.Trim(),@this?.Trim(),System.StringComparison.OrdinalIgnoreCase));

		public static bool In(this string @this,params string[] list)
			=> list.Any(c => string.Equals(c?.Trim(),@this?.Trim(),System.StringComparison.OrdinalIgnoreCase));

		public static bool IsNullOrWhiteSpace(this string @this) => string.IsNullOrWhiteSpace(@this);

		#endregion Public Methods
	}
}
