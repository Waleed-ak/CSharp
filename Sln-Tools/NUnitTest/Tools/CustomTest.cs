using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Tools;

namespace NUnitTest.Tools
{
	[JsonConverter(typeof(CustomConverter<Price,decimal>))]
	public class Price:Custom<Price,decimal>
	{
		public Price(decimal value = 0) : base(value)
		{
		}
		public Price()
		{

		}
		#region Public Constructors


		#endregion Public Constructors

		#region Public Methods

		public static implicit operator Price(decimal value) => new Price(value);

		#endregion Public Methods
	}

	class CustomTest
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
		public void PriceTest()
		{
			var val = new Price(2.4M);
			Assert.IsTrue(val == 2.4M);
			Assert.IsTrue(val != 2.3M);
			Assert.IsTrue(val > 2.3M);
			Assert.IsTrue(val >= 2.3M);
			var val2 = (decimal)val;
			Assert.IsTrue(val2 == 2.4M);
			var val3 = (Price)2.4M;
			Assert.IsTrue(val2 == val3);
		}

		[Test]
		public void PriceJson()
		{
			var val = new Price(2.4M);
			var v2 = FormatConvert.DeserializeJson<Price>(val.SerializeJson());
			Assert.AreEqual(val.Value,v2.Value);
			Console.WriteLine(val.SerializeJson());
			Console.WriteLine(val.SerializeXml(true));
		}

		#endregion Public Methods
	}
}
