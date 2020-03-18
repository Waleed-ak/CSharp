using Autofac;

namespace Tools
{
	public static class AutofacStatic
	{
		#region Private Fields
		private static IResolver Resolver;
		#endregion Private Fields

		#region Public Methods

		public static T Resolve<T>() => Resolver.Resolve<T>();

		public static T ResolveKeyed<T>(object key) => Resolver.ResolveKeyed<T>(key);

		public static T ResolveNamed<T>(string name) => Resolver.ResolveNamed<T>(name);

		public static void SetResolver(IContainer container) => Resolver = new ResolverContainer(container);

		#endregion Public Methods
	}
}
