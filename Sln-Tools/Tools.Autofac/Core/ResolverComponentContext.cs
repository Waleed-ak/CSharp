using System;
using Autofac;

namespace Tools
{
	public class ResolverComponentContext:IResolver
	{
		#region Private Fields
		private readonly IComponentContext _IComponentContext;
		#endregion Private Fields

		#region Public Constructors

		public ResolverComponentContext(IComponentContext componentContext)
		{
			_IComponentContext = componentContext;
		}

		#endregion Public Constructors

		#region Public Methods

		public ILifetimeScope CreateScope() => _IComponentContext.Resolve<ILifetimeScope>().BeginLifetimeScope();

		public IComponentContext GetComponentContext() => _IComponentContext;

		public T Resolve<T>() => _IComponentContext.Resolve<T>();

		public object Resolve(Type type) => _IComponentContext.Resolve(type);

		public T ResolveKeyed<T>(object key) => _IComponentContext.ResolveKeyed<T>(key);

		public T ResolveNamed<T>(string name) => _IComponentContext.ResolveNamed<T>(name);

		#endregion Public Methods
	}
}
