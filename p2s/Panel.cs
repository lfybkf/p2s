using System;
using System.Collections.Generic;
using System.Linq;
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
	}//class
}//ns
