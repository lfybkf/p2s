using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public class Sprite: SceneItem
	{
		public static readonly string TYPE = "sprite";
		static readonly string defaultNum = "0";
		//======================
		string num;
		int Num { get { return Int32.Parse(num); } }
		public string sheetName {get; private set;}
		//======================

		public override bool init(JsonObject jo)
		{
			base.init(jo);
			num = jo.get("properties.Frame") ?? defaultNum;
			sheetName = jo.get("properties.SpriteName");
			return true;
		}//function

		public Frame getFrame()
		{
			return getScene().getSpriteSheet(sheetName).getFrame(Num);
		}//function

		public Image getImage()
		{
			Scene scene = getScene();
			SpriteSheet sheet = scene.getSpriteSheet(sheetName);
			return sheet.getImage(Num);
		}//function

	}//class
}//ns
