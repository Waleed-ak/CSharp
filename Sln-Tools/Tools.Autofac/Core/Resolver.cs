using Autofac;

namespace Tools
{
	public static class Resolver
	{


		#region Private Fields
		private static IResolver _Resolver;
		#endregion Private Fields

		#region Public Methods
		/// <summary>
		/// this method will be called After
		/// var builder = new ContainerBuilder();
		/// </summary>
		/// <param name="builder"></param>
		public static void RegisterResolver(ContainerBuilder builder)
			=> builder.RegisterType<ResolverComponentContext>().As<IResolver>().SingleInstance();

		public static T Resolve<T>() => _Resolver.Resolve<T>();

		public static T ResolveKeyed<T>(object key) => _Resolver.ResolveKeyed<T>(key);

		public static T ResolveNamed<T>(string name) => _Resolver.ResolveNamed<T>(name);

		/// <summary>
		/// this method will be called After
		/// var container = builder.Build();
		/// </summary>
		/// <param name="container"></param>
		public static void SetResolver(IContainer container) => _Resolver = new ResolverContainer(container);

		/// <summary>
		/// this method is useful for mocking.
		/// </summary>
		/// <param name="resolver"></param>
		public static void SetResolverTest(IResolver resolver) => _Resolver = resolver;
		#endregion Public Methods
	}
}
