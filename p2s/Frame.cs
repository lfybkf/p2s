using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public class Frame
	{
		static readonly string fmtMain = "({0}) x={1} y={2} h={3} w={4} name={5}";
		//===================
		string x, y, h, w;
		string name;
		public string Name { get {return name;} set { name = (name == string.Empty) ? value : name; } }
		public int num = -1;
		public int X { get { return Int32.Parse(x); } }
		public int Y { get { return Int32.Parse(y); } }
		public int H { get { return Int32.Parse(h); } }
		public int W { get { return Int32.Parse(w); } }
		public Rectangle rectangle { get { return new Rectangle(X, Y, W, H); } }
		public Image Image { get { return sheet.getImage(num);  } }

		internal SpriteSheet sheet;
		//===================
		public override string ToString()
		{
				return string.Format(fmtMain, num, x, y, h, w, name ?? string.Empty);
		}//function

		public bool isValid
		{
			get
			{
				bool b = string.IsNullOrWhiteSpace(x) == false
					&& string.IsNullOrWhiteSpace(y) == false
					&& string.IsNullOrWhiteSpace(w) == false
					&& string.IsNullOrWhiteSpace(h) == false;
				return b;
			}
		}//function

		public string[] View
		{
			get
			{
				string[] ss = new string[] { "name={0}".fmt(name) };
				return ss;
			}//get
		}//function

		public void load(JsonObject jo)
		{
			name = jo.get("filename") ?? string.Empty;
			x = jo.get("frame.x");
			y = jo.get("frame.y");
			h = jo.get("frame.h");
			w = jo.get("frame.w");

			if (name != string.Empty)
				name = name.onlySymbols();
		}//function


		public void load(XElement elem)
		{
			name = elem.Attribute("name").Value;
			x = elem.Attribute("x").Value;
			y = elem.Attribute("y").Value;
			h = elem.Attribute("height").Value;
			w = elem.Attribute("width").Value;
		}//function

		internal XElement toXmlAtlas()
		{
			return new XElement("SubTexture"
				, new XAttribute("name", name), new XAttribute("x", x), new XAttribute("y", y), new XAttribute("width", w), new XAttribute("height", h)
				);
		}//function

		internal XElement toXmlScale()
		{
			return new XElement("Texture"	, new XAttribute("name", name));
		}//function
	}//class
}//ns
