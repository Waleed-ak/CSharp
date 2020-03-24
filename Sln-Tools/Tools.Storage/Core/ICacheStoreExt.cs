using System;
using System.Collections.Generic;

namespace Tools
{
	internal static class ICacheStoreExt
	{
		#region Private Fields
		private static readonly string Machiane = LocalMachiane();
		private static readonly Dictionary<Type,string> map = new Dictionary<Type,string>();
		private static readonly Random Rand = new Random();
		#endregion Private Fields

		#region Public Methods

		public static bool Exists<T>(this ICacheStore @this) => @this.Exists<T>(@this.Token());

		public static T Get<T>(this ICacheStore @this) => @this.Get<T>(@this.Token());

		public static T GetSet<T>(this ICacheStore @this,Func<T> func) => GetSet<T>(@this,@this.Token(),func,@this.CacheDuration);

		public static T GetSet<T>(this ICacheStore @this,Func<T> func,TimeSpan timeSpan) => GetSet<T>(@this,@this.Token(),func,timeSpan);

		public static T GetSet<T>(this ICacheStore @this,string key,Func<T> func) => GetSet<T>(@this,key,func,@this.CacheDuration);

		public static T GetSet<T>(this ICacheStore @this,string key,Func<T> func,TimeSpan timeSpan)
		{
			var value = @this.Get<T>(key);
			if(Equals(value,default(T)))
			{
				value = func();
				@this.Set(key,value,timeSpan);
			}
			return value;
		}

		public static string NewKey(this ICacheStore _) => $"{DateTime.Now:ddHHmmssff}-{Machiane}-{Rand.Next():X8}";

		public static bool Remove<T>(this ICacheStore @this) => @this.Remove<T>(@this.Token());

		public static void Set<T>(this ICacheStore @this,string key,T value) => @this.Set<T>(key,value,@this.CacheDuration);

		public static void Set<T>(this ICacheStore @this,T value,TimeSpan timeSpan) => @this.Set<T>(@this.Token(),value,timeSpan);

		public static void Set<T>(this ICacheStore @this,T value) => @this.Set<T>(@this.Token(),value,@this.CacheDuration);

		#endregion Public Methods

		#region Internal Methods

		internal static bool CheckKey(this string key) => string.IsNullOrWhiteSpace(key);

		internal static string GetKey<T>(this ICacheStore @this,string key) => $"{@this.AppName}-{GetFriendlyName<T>()}-{key}";

		#endregion Internal Methods

		#region Private Methods

		private static string GetFriendlyName<T>() => typeof(T).GetFriendlyName();

		private static string GetFriendlyName(this Type type)
		{
			if(map.TryGetValue(type,out var friendlyName))
			{
				return friendlyName;
			}

			friendlyName = type.Name;
			if(type.IsGenericType)
			{
				var iBacktick = friendlyName.IndexOf('`');
				if(iBacktick > 0)
				{
					friendlyName = friendlyName.Remove(iBacktick);
				}
				friendlyName += "<";
				var typeParameters = type.GetGenericArguments();
				for(var i = 0;i < typeParameters.Length;++i)
				{
					var typeParamName = typeParameters[i].IsGenericType ? GetFriendlyName(typeParameters[i]) : typeParameters[i].Name;
					friendlyName += (i == 0 ? "" : ",") + typeParamName;
				}
				friendlyName += ">";
			}

			return map[type] = friendlyName;
		}

		private static string LocalMachiane()
		{
			var hash = Environment.MachineName.GetHashCode();
			hash = 0xffff & (hash ^ hash >> 16);
			return $"{hash:X4}";
		}

		#endregion Private Methods
	}
}
