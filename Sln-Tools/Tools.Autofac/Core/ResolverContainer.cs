using System;
using Autofac;

namespace Tools
{
	public class ResolverContainer:IResolver
	{
		#region Private Fields
		private readonly IContainer Container;
		#endregion Private Fields

		#region Public Constructors

		public ResolverContainer(IContainer container) => Container = container;

		#endregion Public Constructors

		#region Public Methods

		public ILifetimeScope CreateScope() => Container.BeginLifetimeScope();

		public IComponentContext GetComponentContext() => Container;

		public T Resolve<T>() => Container.Resolve<T>();

		public object Resolve(Type type) => Container.Resolve(type);

		public T ResolveKeyed<T>(object key) => Container.ResolveKeyed<T>(key);

		public T ResolveNamed<T>(string name) => Container.ResolveNamed<T>(name);

		#endregion Public Methods
	}
}
