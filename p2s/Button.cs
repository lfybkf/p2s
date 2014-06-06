using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	class Button: SceneItem
	{
		public static readonly string TYPE = "button";
		//=================
		IList<SceneItem> childs = new List<SceneItem>();
		string stateMashine;
		//=================
		public override bool init(JsonObject jo)
		{
			base.init(jo);
			stateMashine = jo.get("statemachine");

			JsonArray jaChilds = new JsonArray(jo.get("children"));
			bool isChildsOK = SceneItem.fillChilds(jaChilds, childs);

			return isChildsOK;
		}//function
	}//class
}//ns
