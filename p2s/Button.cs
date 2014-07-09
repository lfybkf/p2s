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
		string stateMachine;
		//=================
		public override bool init(JsonObject jo)
		{
			base.init(jo);
			stateMachine = jo.get("statemachine");
			return true;
		}//function

		public override string[] View
		{
			get
			{
				string[] ss = new string[] { "statemachine={0}".fmt(stateMachine) };
				return base.View.Concat(ss).ToArray();
			}//get
		}//function

		internal override XElement toXmlInitializer
		{
			get
			{
				Sprite[] sprites = this.getChildsAll().OfType<Sprite>().ToArray();
				XElement Ret = new XElement(Air.INITIALIZER
					, new XAttribute(Air.ID, this.id)
					, new XAttribute(Air.CLASS, Air.getComp(this))
					, new XAttribute(Air.STYLE, this.Style)
					, new XElement(Air.TEXTURE_IMAGE, new XAttribute(Air.DEFAULT_SKIN, sprites[0].Frame.Name)
					)
				);
				string hoverSkin = (sprites.Length >= 2) ? sprites[1].Frame.Name : sprites[0].Frame.Name;
				string downSkin = (sprites.Length >= 3) ? sprites[2].Frame.Name : sprites[0].Frame.Name;
				
				if (sprites.Count() <= 1) Logger.def.warn("Button {0} hasnt all skins".fmt(id));

				Ret.Add(new XElement(Air.TEXTURE_IMAGE, new XAttribute(Air.DOWN_SKIN, downSkin)));
				Ret.Add(new XElement(Air.TEXTURE_IMAGE, new XAttribute(Air.HOVER_SKIN, hoverSkin)));
				return Ret;
			}//get
		}//function
	}//class
}//ns
