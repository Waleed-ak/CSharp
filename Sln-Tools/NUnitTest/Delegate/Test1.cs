using System;

namespace NUnitTest.Delegate
{
	public interface ITest1
	{
		string Var1 { get; set; }

		void ActString(string x);
		string FunString();
		string FunString(string x);
	}

	public class Test1:ITest1
	{
		#region Public Properties
		public string Var1 { get; set; } = "This is Var1";
		#endregion Public Properties

		#region Public Methods

		public void ActString(string x) => Console.WriteLine($"This is Var1 {x}");

		public string FunString() => "This is Var1";

		public string FunString(string x) => $"This is Var1 {x}";

		#endregion Public Methods
	}
}
