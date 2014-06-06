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
	public abstract class SceneItem
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
			return true;
		}//function

		public static SceneItem create(string type)
		{
			SceneItem Ret = null;

			if (type == Panel.TYPE)
				Ret = new Panel();
			else if (type == Button.TYPE)
				Ret = new Button();
			else if (type == Text.TYPE)
				Ret = new Text();
			else if (type == Sprite.TYPE)
				Ret = new Sprite();

			return Ret;
		}//function

		public static bool fillChilds(JsonArray jaChilds, IList<SceneItem> childs)
		{
			string typeChild;
			JsonObject jo;
			SceneItem child = null;
			for (int i = 0; i < jaChilds.Count; i++)
			{
				jo = jaChilds.get(i);
				typeChild = jo.get("type");

				child = SceneItem.create(typeChild);
				child.init(jo);
				childs.Add(child);
			}//for

			return true;
		}//function

		public override string ToString()
		{
			return string.Format("({0} - {1})", type, id);
		}//function
	}//class
}//ns
