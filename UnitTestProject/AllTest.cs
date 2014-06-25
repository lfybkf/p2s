using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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
			scene.Name = "bacara";
			scene.load();
			scene.SaveDir = @"D:\GamesContent\baccarat_xml\";
			scene.saveToXmlTheme();
			scene.saveToXmlLayout();
			scene.saveToXmlInitializer();
		}//function

		[TestMethod]
		public void testSpriteLoadXml()
		{
			IEnumerable<SpriteSheet> sheets = SpriteSheet.getFromTheme(@"D:\GamesContent\baccarat_xml\theme\bacara\drawable-mdpi\");
			Assert.IsTrue(sheets.Count() == 5, "size isnt OK");
		}//function
	}//class
}//ns
