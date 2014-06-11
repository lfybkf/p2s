using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	/// <summary>
	/// abstract item of scene
	/// may be Panel, Button, Text, Sprite
	/// may have children
	/// </summary>
	public abstract class SceneItem: IContainerOfSceneItems
	{
		protected static readonly string defaultId = "NO ID";
		static readonly string defaultX = "0";
		static readonly string defaultY = "0";
		//==================
		protected string type;
		protected string id;
		protected string drawOrder;
		protected string positionX;
		protected string positionY;
		IList<SceneItem> childs = new List<SceneItem>();
		//public bool hasChilds { get { return childs.Any(); } }
		//==================
		public Scene getScene()
		{ 
			//we dont like cyclic references
			return Scene.takeScene(this);
		}//function

		public virtual bool init(JsonObject jo)
		{
			type = jo.get("type");
			drawOrder = jo.get("properties.DrawOrder");
			id = jo.get("properties.Id") ?? defaultId;
			positionX = jo.get("properties.Position.x") ?? defaultX;
			positionY = jo.get("properties.Position.y") ?? defaultY;

			//childs
			string s = jo.get("children");
			if (s != null)
			{
				JsonArray jaChilds = new JsonArray(s);
				fillChilds(jaChilds, childs, getScene());
			}//if

			return true;
		}//function

		public static SceneItem create(string type)
		{
			SceneItem Ret = null;

			if (type == Panel.TYPE)
				Ret = new Panel();
			else if (type == Button.TYPE)
				Ret = new Button();
			else if (type == Checkbox.TYPE)
				Ret = new Checkbox();
			else if (type == Sprite.TYPE)
				Ret = new Sprite();
			else if (type == Text.TYPE || type == Text.TYPE2)
				Ret = new Text();
			else
				Logger.def.warn("SceneItem.create wrong type {0}".fmt(type) );

			return Ret;
		}//function

		public static bool fillChilds(JsonArray jaChilds, IList<SceneItem> childs, Scene scene)
		{
			string typeChild;
			JsonObject jo;
			SceneItem child = null;
			object o;
			for (int i = 0; i < jaChilds.Count; i++)
			{
				o = jaChilds[i];
				if (o is JsonObject)
					jo = (JsonObject)o;
				else
					jo = scene.getFromPull(o.ToString());
				
				typeChild = jo.get("type");
				child = SceneItem.create(typeChild);
				childs.Add(child);
				child.init(jo);
			}//for

			return true;
		}//function

		public override string ToString()
		{
			return "({0} - {1})".fmt(type, id);
		}//function

		public IEnumerable<SceneItem> getChilds()
		{
			return childs;
		}
	}//class
}//ns
