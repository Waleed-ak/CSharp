using System;
using System.Reflection;

namespace NUnitTest.Delegate
{
	public class AOP<InterFace>:DispatchProxy
	{
		#region Public Properties
		public string ClassName;
		public dynamic Decorated;
		#endregion Public Properties

		#region Public Methods

		public static InterFace Create<T>(T decorated)
		{
			dynamic proxy = Create<InterFace,AOP<InterFace>>();
			SetParameters();
			return proxy;
			void SetParameters()
			{
				proxy.Decorated = Equals(decorated,default(T)) ? throw new ArgumentNullException(nameof(decorated)) : decorated;
				proxy.ClassName = decorated.GetType().Name;
			}
		}

		#endregion Public Methods

		#region Protected Methods

		protected override object Invoke(MethodInfo targetMethod,object[] args)
		{
			var result = targetMethod.Invoke(Decorated,args);

			return result;
		}

		#endregion Protected Methods
	}
}
