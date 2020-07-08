using System;

namespace Tools
{
  public static class Cls<T>
	{
		#region Private Fields
		private static Func<T> FuncObj;
		#endregion Private Fields

		#region Public Constructors

		static Cls()
		{
			IsInterface = Type.IsInterface;
			Name = Type.Name;
		}

		#endregion Public Constructors

		#region Public Properties
		public static bool IsInterface { get; }
		public static string Name { get; }
		public static T Resolve => (FuncObj ?? throw new TypeAccessException($"Please Register the Type {Name}"))();
		public static Type Type { get; } = typeof(T);
		#endregion Public Properties

		#region Public Methods

		public static void Register(Func<T> instanceCreator)
		{
			if(FuncObj == null)
			{
				FuncObj = instanceCreator;
				return;
			}
			throw new Exception($"The Type {Name} is already registered");
		}

		public static void RegisterSingleton(T instance) => Register(() => instance);

		public static void RegisterSingleton(Func<T> instanceCreator)
		{
			var lazy = new Lazy<T>(instanceCreator);
			Register(() => lazy.Value);
		}

		#endregion Public Methods
	}
}
