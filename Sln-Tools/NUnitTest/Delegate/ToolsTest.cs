using NUnit.Framework;

namespace NUnitTest.Delegate
{
	public class ToolsTest
	{
		#region Public Methods

		[Test]
		public void T1()
		{
			var M = Tools.Test1(c => c.Var1);
			var M2 = Tools.Test1(c => c.FunString());
			var M3 = Tools.Test1(c => c.FunString("SSSS"));
			Tools.Test1(c => c.ActString("XYZ"));
		}

		#endregion Public Methods
	}
}
