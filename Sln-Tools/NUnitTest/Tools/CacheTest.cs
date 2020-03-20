using System;
using System.Threading;
using NUnit.Framework;
using Tools;

namespace NUnitTest.Tools
{
	internal class CacheTest
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
		public void SetGet()
		{
			var val = CacheObj.SetGet("Key",() => "Value");
			Assert.AreEqual(val,"Value");
			val = (string)CacheObj.Get("Key");
			Assert.AreEqual(val,"Value");
		}

		[Test]
		public void SetGetAbsolute()
		{
			var val = CacheObj.SetGetAbsolute("Key",() => "Value",TimeSpan.FromSeconds(2));

			Thread.Sleep(1000);
			val = (string)CacheObj.Get("Key");
			Assert.AreEqual(val,"Value");

			Thread.Sleep(1001);
			val = (string)CacheObj.Get("Key");
			Assert.AreNotEqual(val,"Value");
		}

		[Test]
		public void SetGetSliding()
		{
			var val = CacheObj.SetGetSliding("Key",() => "Value",TimeSpan.FromSeconds(2));

			Thread.Sleep(1000);
			val = (string)CacheObj.Get("Key");
			Assert.AreEqual(val,"Value");

			Thread.Sleep(1000);
			val = (string)CacheObj.Get("Key");
			Assert.AreEqual(val,"Value");

			Thread.Sleep(1000);
			val = (string)CacheObj.Get("Key");
			Assert.AreEqual(val,"Value");

			Thread.Sleep(1000);
			val = (string)CacheObj.Get("Key");
			Assert.AreEqual(val,"Value");
		}

		#endregion Public Methods
	}
}
