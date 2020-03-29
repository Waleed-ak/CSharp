using System;

namespace NUnitTest.Delegate
{
	public static class Tools
	{
		#region Public Methods

		public static T Test1<T>(Func<ITest1,T> func) => Run<ITest1,Test1,T>(func);

		public static void Test1(Action<ITest1> func) => Run<ITest1,Test1>(func);


		#endregion Public Methods

		#region Private Methods

		private static T Run<Interface,Class, T>(Func<Interface,T> func) where Class : new()
		{
			var test1 = AOP<Interface>.Create(new Class());

			var ret = func(test1);

			return ret;
		}

		private static void Run<Interface,Class>(Action<Interface> func) where Class :new()
		{
			var test1 = AOP<Interface>.Create(new Class());

			func(test1);
		}

		#endregion Private Methods
	}
}
