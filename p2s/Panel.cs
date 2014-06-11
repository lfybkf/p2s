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
		string sizeH;
		string sizeW;
		//=================
		public override bool init(JsonObject jo)
		{
 			base.init(jo);
			sizeH = jo.get("properties.Size.h");
			sizeW = jo.get("properties.Size.w");
			return true;
		}//function
	}//class
}//ns
