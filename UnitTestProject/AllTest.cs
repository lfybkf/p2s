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
			//string jsonString = File.ReadAllText("spriteSheet.json");
			SpriteSheet sheet = new SpriteSheet("spriteSheet.json");
			sheet.load();
			Assert.IsTrue(sheet.size == 1, "size isnt OK");
			Assert.IsTrue(sheet.isContentValid, "content isnt OK");
		}//function
	}//class
}//ns
