using System;
using Autofac;

namespace Tools
{
	public interface IResolver
	{
		#region Public Methods

		ILifetimeScope CreateScope();

		IComponentContext GetComponentContext();

		T Resolve<T>();

		object Resolve(Type type);

		T ResolveKeyed<T>(object key);

		T ResolveNamed<T>(string name);

		#endregion Public Methods
	}
}
