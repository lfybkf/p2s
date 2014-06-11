using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public class Checkbox: SceneItem
	{
		public static readonly string TYPE = "checkbox";
		//=================
		IList<SceneItem> childs = new List<SceneItem>();
		string stateMashine;
		//=================
		public override bool init(JsonObject jo)
		{
			base.init(jo);
			stateMashine = jo.get("statemachine");
			return true;
		}//function

	}//class
}//ns
