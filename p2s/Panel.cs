using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{

	public class Panel: SceneItem
	{
		public static readonly string TYPE = "sceneObject";
		//=================
		string h;
		string w;
		//=================
		public override bool init(JsonObject jo)
		{
 			base.init(jo);
			h = jo.get("properties.Size.h");
			w = jo.get("properties.Size.w");
			return true;
		}//function

		public override string[] View
		{
			get
			{
				string[] ss = new string[] { "h={0}".fmt(h), "w={0}".fmt(w) };
				return base.View.Concat(ss).ToArray();
			}//get
		}//function

		internal override XElement toXmlInitializer
		{
			get
			{
				Sprite[] sprites = getChilds().Where(si => si is Sprite).Select(si => (Sprite)si).ToArray();
				XElement Ret = new XElement(Air.INITIALIZER
					, new XAttribute(Air.ID, this.id)
					, new XAttribute(Air.CLASS, Air.getComp(this))
					, new XAttribute(Air.STYLE, this.Style)
				);
				return Ret;
			}
		}//function

		internal override XElement toXmlComponent
		{
			get
			{
				XElement Ret = base.toXmlComponent;
				IEnumerable<SceneItem> childs = getChilds();
				Ret.Add(
					new XElement(Air.CONSTANTS
						, childs.Select(sitem => sitem.toXmlConstant))
					, new XElement(Air.CHILDS
						, childs.Select(sitem => sitem.toXmlChild))
					, new XElement(Air.LAYOUT
						, childs.Select(sitem => sitem.toXmlLayout))
					);
				return Ret;
			}
		}

	}//class
}//ns
