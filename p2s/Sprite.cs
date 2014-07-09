using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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
		public Frame Frame { get { return Scene.getSpriteSheet(sheetName).getFrame(Num); } }
		public Image Image { get { return Frame.Image; } }
		public string sheetName {get; private set;}
		//======================

		public override bool init(JsonObject jo)
		{
			num = jo.get("properties.Frame") ?? defaultNum;
			sheetName = jo.get("properties.SpriteName");
			base.init(jo);
			//init of spritesheet and frame
			Frame.Name = id;
			return true;
		}//function

		public override string[] View
		{
			get
			{
				string[] ss = new string[] { "sheetName={0}".fmt(sheetName), "num={0}".fmt(num.ToString()) };
				return base.View.Concat(ss).ToArray();
			}//get
		}//function

		/// <summary>
		/// sequence here is not good
		/// id depends on sheen
		/// </summary>
		/// <returns></returns>
		protected override string makeId()		{			return sheetName + num;		}//function

		internal override XElement toXmlInitializer
		{
			get
			{
				return new XElement(Air.INITIALIZER
					, new XAttribute(Air.ID, this.id)
					, new XAttribute(Air.CLASS, Air.getComp(this))
					, new XAttribute(Air.STYLE, this.Style)
					, new XElement(Air.TEXTURE_IMAGE
						, new XAttribute(Air.IMAGE, this.Frame.Name)
					));
			}
		}//function
	}//class
}//ns
