using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public class SpriteSheet
	{
		public static readonly string spriteType = "SpriteFrameSheet";
		public static readonly string fileMask = "*.spr";
		//=================
		public string atlasName { get; private set;}
		public IList<Sprite> content = new List<Sprite>();
		public int size { get { return content.Count(); } }
		//=================

		public bool load(string jsonString)
		{
			JsonObject jo = new JsonObject(jsonString);
			string type = jo.get("header.type");
			if (type != spriteType)
				return false;

			atlasName = jo.get("resource.meta.image");
			JsonArray jaFrames = new JsonArray(jo.get("resource.frames"));
			Sprite spr;
			for (int i = 0; i < jaFrames.Count; i++)
			{
				spr = new Sprite();
				spr.load(jaFrames.get(i));
				spr.sheet = this;
				content.Add(spr);
			}//for

			return true;
		}//function

		public bool isContentValid
		{
			get 
			{
				var listOfInvalid = content.Where(sprite => sprite.isValid == false);
				return listOfInvalid.Any() == false;
			}
		}//function
	}//class
}//ns
