using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using synesis;

namespace UnitTestProject
{
	[TestClass]
	public class AllTest
	{
		static string fileScene = @"D:\GamesContent\baccarat\scene.object";

		[TestMethod]
		public void testSpriteLoad()
		{
			SpriteSheet sheet = new SpriteSheet("spriteSheet.json");
			sheet.load();
			Assert.IsTrue(sheet.size == 1, "size isnt OK");
			Assert.IsTrue(sheet.isContentValid, "content isnt OK");
		}//function

		[TestMethod]
		public void testSceneXml()
		{
			Scene scene = new Scene(fileScene);
			scene.load();
			scene.SaveDir = @"D:\GamesContent\baccarat_xml\";
			//scene.saveToXmlAtlas();
			scene.saveToXmlLayout();
		}//function
	}//class
}//ns
