using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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
		//protected static readonly string defaultId = "NO ID";
		static readonly string defaultX = "0";
		static readonly string defaultY = "0";
		//==================
		protected string type;
		protected internal string id;
		protected string drawOrder;
		protected string x;
		protected string y;
		IList<SceneItem> childs = new List<SceneItem>();
		public Scene Scene { get { return Scene.takeScene(this); } }
		public int DrawOrder { get { int i = 0;  bool b = Int32.TryParse(drawOrder, out i); return i; } }
		public string Style { get { return id; } }
		//==================

		public override string ToString() { return "({0} - {1})".fmt(type, id); }//function
		public IEnumerable<SceneItem> getChilds() { return childs; }
		protected virtual string makeId()		{			return Idgen.next(this);		}//function

		public virtual bool init(JsonObject jo)
		{
			type = jo.get("type");
			drawOrder = jo.get("properties.DrawOrder");
			id = jo.get("properties.Id") ?? makeId();
			x = jo.get("properties.Position.x") ?? defaultX;
			y = jo.get("properties.Position.y") ?? defaultY;

			//childs
			string s = jo.get("children");
			if (s != null)
			{
				JsonArray jaChilds = new JsonArray(s);
				fillChilds(jaChilds, childs, Scene);
			}//if

			//id work
			id = id.onlySymbols().ToLower();
			return true;
		}//function

		public virtual string[] View
		{
			get {
				return new string[] { "type={0}".fmt(type), "id={0}".fmt(id), "x={0}".fmt(x), "y={0}".fmt(y), "drawOrder={0}".fmt(drawOrder) };
			}//get
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
				if (child != null)
				{
					childs.Add(child);
					child.init(jo);
				}//if
			}//for

			return true;
		}//function

		internal virtual XElement toXmlConstant		{
			get { 
				return new XElement(Air.CONSTANT
					, new XAttribute(Air.NAME, id.ToUpper())
					, new XAttribute(Air.TYPE, "string")
					, new XAttribute(Air.VALUE, id)
				);
			}
		}//function

		internal virtual XElement toXmlChild
		{
			get
			{
				return new XElement(Air.CHILD
					, new XAttribute(Air.CID, id.ToUpper())
					, new XAttribute(Air.STYLE, Style)
				);
			}
		}//function

		internal virtual IEnumerable<XElement> toXmlLayout
		{
			get
			{
				return new XElement[] 
				{
					new XElement(Air.ACTION
						, new XAttribute(Air.TARGET_COMPONENT_CID, id.ToUpper())
						, new XAttribute(Air.TARGET_PROPERTY, "x")
						, new XElement(Air.DP 
							, new XAttribute(Air.VALUE, x)
							, new XAttribute(Air.ACTION, Air.ASSIGN)
						)
					)
					, new XElement(Air.ACTION
						, new XAttribute(Air.TARGET_COMPONENT_CID, id.ToUpper())
						, new XAttribute(Air.TARGET_PROPERTY, "y")
						, new XElement(Air.DP 
							, new XAttribute(Air.VALUE, y)
							, new XAttribute(Air.ACTION, Air.ASSIGN)
						)
					) 
				};
			}
		}//function

		internal virtual XElement toXmlInitializer		{			get
			{
				return null;
			}
		}//function

		internal virtual XElement toXmlComponent		{			get
			{
				return new XElement(Air.COMPONENT
					, new XAttribute(Air.CLASS, Air.getComp(this))
					, new XAttribute(Air.ID, this.id)
					);
			}
		}//function
	}//class
}//ns
