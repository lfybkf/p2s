using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	class Text : SceneItem
	{
		public static readonly string TYPE = "ttfTextEdit";
		public static readonly string TYPE2 = "text";
		//================
		string fontName;
		string text;
		string alignH;
		string alignV;
		//================
		public override bool init(ComputerBeacon.Json.JsonObject jo)
		{
			base.init(jo);
			fontName = jo.get("properties.FontName");
			text = jo.get("properties.Text");
			alignH = jo.get("properties.HAlignment");
			alignV = jo.get("properties.VAlignment");
			return true;
		}//function

		public override string[] View
		{
			get
			{
				string[] ss = new string[] { "text={0}".fmt(text), "fontName={0}".fmt(fontName) };
				return base.View.Concat(ss).ToArray();
			}//get
		}//function
	}//class
}//ns
