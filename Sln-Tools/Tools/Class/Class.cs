namespace Tools.Class
{
	public static class Class<T> where T : class, new()
	{
		#region Public Properties
		public static T New => new T();
		public static T Singleton { get; } = new T();
		#endregion Public Properties
	}
}
