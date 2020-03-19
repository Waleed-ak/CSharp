using System;
using NUnit.Framework;
using Tools;

namespace NUnitTest.Tools
{
	public class GenericTestExample
	{
		#region Public Properties
		public int IntVal { get; set; }
		public string StrVal { get; set; }
		#endregion Public Properties
	}

	internal class GenericTest
	{
		#region Public Methods

		[SetUp]
		public void _Setup()
		{
		}

		[TearDown]
		public void _TearDown()
		{
		}

		[Test]
		public void Generic()
		{
			var val = new Coll<GenericTestExample>
			{
				new GenericTestExample { IntVal = 1,StrVal = "Val1" },
				new GenericTestExample { IntVal = 2,StrVal = "Val2" },
				new GenericTestExample { IntVal = 3,StrVal = "Val3" },
				new GenericTestExample { IntVal = 4,StrVal = "Val4" },
				new GenericTestExample { IntVal = 5,StrVal = "Val5" },
			};
			Console.WriteLine(val.ToJson());
			Console.WriteLine(val.ToXml());
			var val2 = val.Clone();
			Assert.AreEqual(val.GetType(),val2.GetType());
			Assert.AreEqual(val.Count,val2.Count);
			Assert.AreNotSame(val,val2);
			Assert.AreNotSame(val[0],val2[0]);
			Assert.AreNotSame(val[0].IntVal,val2[0].IntVal);
		}

		#endregion Public Methods
	}
}
