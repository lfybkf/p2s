using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{

	public class Panel: SceneItem, IContainerOfSceneItems
	{
		public static readonly string TYPE = "sceneObject";
		//=================
		IList<SceneItem> childs = new List<SceneItem>();
		string sizeH;
		string sizeW;
		//=================
		public IEnumerable<SceneItem> getChilds()
		{
			return childs;
		}//function

		public override bool init(JsonObject jo)
		{
 			base.init(jo);
			sizeH = jo.get("properties.Size.h");
			sizeW = jo.get("properties.Size.w");

			JsonArray jaChilds = new JsonArray(jo.get("children"));
			bool isChildsOK = SceneItem.fillChilds(jaChilds, childs);

			return isChildsOK;
		}//function
	}//class
}//ns
