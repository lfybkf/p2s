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
	}//class
}//ns
