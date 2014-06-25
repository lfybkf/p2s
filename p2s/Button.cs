using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public class Button: SceneItem
	{
		public static readonly string TYPE = "button";
		//=================
		string stateMashine;
		//=================
		public override bool init(JsonObject jo)
		{
			base.init(jo);
			stateMashine = jo.get("statemachine");
			return true;
		}//function

		internal override XElement toXmlInitializer
		{
			get
			{
				Sprite[] sprites = getChilds().Where(si => si is Sprite).Select(si => (Sprite)si).ToArray();
				XElement Ret = new XElement(Air.INITIALIZER
					, new XAttribute(Air.CLASS, Air.getComp(this))
					, new XAttribute(Air.ID, id + Air.INITIALIZER)
					, new XElement(Air.TEXTURE_IMAGE
						, new XAttribute(Air.DEFAULT_SKIN, sprites[0].id)
					)
				);
				string hoverSkin = (sprites.Count() >= 2) ? sprites[1].id : sprites[0].id;
				string downSkin = (sprites.Count() >= 3) ? sprites[2].id : sprites[0].id;

				Ret.Add(new XElement(Air.TEXTURE_IMAGE, new XAttribute(Air.DOWN_SKIN, downSkin)));
				Ret.Add(new XElement(Air.TEXTURE_IMAGE, new XAttribute(Air.HOVER_SKIN, hoverSkin)));
				return Ret;
			}
		}
	}//class
}//ns
