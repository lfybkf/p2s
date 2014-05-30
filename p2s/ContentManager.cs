using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	public static class ContentManager
	{
		public static string BasePath = Environment.CurrentDirectory;
		static SpriteSheet currentSpriteSheet = null;
		static Bitmap imageSpriteSheet = null;
		//====================

		//====================
		public static IEnumerable<string> getSpriteSheetFiles()
		{
			return Directory.GetFiles(BasePath, SpriteSheet.fileMask);
		}//function

		public static SpriteSheet getSpriteSheet(string file)
		{
			if (File.Exists(file) == false)
				file = Path.Combine(BasePath, file);

			string content = File.ReadAllText(file);
			SpriteSheet Ret = new SpriteSheet();
			Ret.load(content);
			return Ret;
		}//function

		public static Image getImage(Sprite sprite)
		{
			Image Ret = null;
		
			//create atlas if need
			if (sprite.sheet != currentSpriteSheet)
			{
				currentSpriteSheet = sprite.sheet;
				imageSpriteSheet = new Bitmap(Path.Combine(BasePath, currentSpriteSheet.atlasName));
			}//if

			//crop sprite
			System.Drawing.Imaging.PixelFormat format = imageSpriteSheet.PixelFormat;
			Ret = imageSpriteSheet.Clone(sprite.rectangle, format);

			return Ret;
		}//function
	}//class
}//ns
