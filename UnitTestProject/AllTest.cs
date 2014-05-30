using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using synesis;

namespace UnitTestProject
{
	[TestClass]
	public class AllTest
	{
		[TestMethod]
		public void testSpriteLoad()
		{
			string jsonString = File.ReadAllText(@"D:\glowing.spr");
			SpriteSheet sheet = new SpriteSheet();
			sheet.load(jsonString);
			Assert.IsTrue(sheet.size == 10, "size isnt OK");
			Assert.IsTrue(sheet.isContentValid, "content isnt OK");
		}//function
	}//class
}//ns
