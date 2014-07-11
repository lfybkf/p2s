using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	/// <summary>
	/// contains all objects of scene
	/// </summary>
	public class Scene: IContainerOfSceneItems
	{
		static readonly string TYPE = "Scene";
		static List<Scene> scenes = new List<Scene>();
		//=====================
		IList<SceneItem> childs = new List<SceneItem> ();
		IList<SpriteSheet> sheets = new List<SpriteSheet>();
		public string Name { get; set; }
		string fileName;
		JsonObject joMain;
		public string SaveDir { get; set; }
		public string BaseDir { get { return Path.GetDirectoryName(fileName); } }
		public IEnumerable<SpriteSheet> Sheets { get { return sheets; } }
		//====================
		
		public IEnumerable<SceneItem> getChilds() { return childs; }//function
		public static Scene takeScene(SceneItem item) { return scenes.FirstOrDefault(scene => scene.contains(item)); }//function
		public static Scene takeScene(SpriteSheet item) { return scenes.FirstOrDefault(scene => scene.sheets.Contains(item)); }//function
		public override string ToString() { return "Scene: {0}".fmt(fileName); }//function

		public Scene(string fileName)
		{
			this.fileName = fileName;
			this.SaveDir = Environment.CurrentDirectory;
			this.Name = "playtika";
			scenes.Add(this);
		}//function

		public bool load()
		{
			Logger.def.clear();

			#region loading and testing
			if (File.Exists(fileName) ==false)
			{
				Logger.def.err(Msg.fileNotFound(fileName));
				return false;
			}//if

			try
			{
				string s = File.ReadAllText(fileName);
				joMain = new JsonObject(s);
			}
			catch (Exception e)
			{
				Logger.def.err(e.Message);
				return false;
			}//try

			string type = joMain.get("header.type");
			if (type != TYPE)
			{
				Logger.def.err(Msg.typeWrong(TYPE, type));
				return false;
			}//if
			#endregion

			#region childs
			JsonArray jaChilds = new JsonArray(joMain.get("resource.root.children"));
			bool isChildsOK = SceneItem.fillChilds(jaChilds, childs, this);
			//make id uniq
			SceneItem[] all = this.getChildsAll().ToArray();
			string[] id_dupls = all.GroupBy(si => si.id).Where(g => g.Count() > 1).Select(g => g.Key ).ToArray();
			foreach (string id in id_dupls)
			{
				foreach (SceneItem item in all)
				{
					if (item.id == id)
						item.id = id + Idgen.next(id).ToString();
				}//for
			}//for
			#endregion

			return isChildsOK;
		}//function

		public SpriteSheet getSpriteSheet(string sheetName)
		{
			SpriteSheet sheet = sheets.FirstOrDefault(s => s.name == sheetName);
			if (sheet == null)
			{
				sheet = new SpriteSheet(sheetName);
				sheets.Add(sheet);
				sheet.load();
			}//if
			return sheet;
		}//function

		public JsonObject getFromPull(string path)
		{
			JsonObject Ret = null;
			string s = joMain.get("resource.{0}".fmt(path));
			if (s == null)
				Logger.def.err("Scene.getFromPull wrong path {0}".fmt(path));
			else
				Ret = new JsonObject(s);

			return Ret;
		}//function

		public void saveToXml()
		{
			saveToXmlTheme();
			saveToXmlLayout();
			saveToXmlInitializer();
		}//function

		public void saveToXmlTheme()
		{
			XDocument xdoc;
			string pathSave = Path.Combine(SaveDir, @"theme\{0}\drawable-mdpi\".fmt(Name));

			if (Directory.Exists(pathSave) == false)
				Directory.CreateDirectory(pathSave);

			//scale
			IEnumerable<Frame> frames = sheets.SelectMany(sheet => sheet.Frames).OrderBy(f => f.Name);
			xdoc = new XDocument(	new XElement("textures", frames.Select(f => f.toXmlScale()))).declare();
			XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
			xdoc.Root.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi), new XAttribute(xsi + "noNamespaceSchemaLocation", "schema/texture-scale-definition.xsd"));
			xdoc.Save(Path.Combine(pathSave, "textureScaleDefinition.xml"));
			
			//atlas
			sheets = SpriteSheet.mergeOnAtlas(sheets).ToList();
			int atlasNum = 0;		string atlasName = null;
			foreach (var sheet in sheets)
			{
				atlasName = "atlas{0}".fmt(atlasNum.ToString());
				atlasNum++;

				//make xml
				xdoc = new XDocument().declare().comment(sheet.Atlas);
				xdoc.Add(new XElement("TextureAtlas", new XAttribute("imagePath", atlasName + ".png")
					, sheet.Frames.Select(f => f.toXmlAtlas())));
				xdoc.Save(Path.Combine(pathSave, atlasName + ".xml"));

				//copy png with rename
				if (File.Exists(Path.Combine(pathSave, atlasName + ".png")) == false)
					{ File.Copy(Path.Combine(BaseDir, sheet.Atlas), Path.Combine(pathSave, atlasName + ".png")); }//if
			}//for
		}//function

		public void saveToXmlLayout()
		{
			string pathSave = Path.Combine(SaveDir, @"layouts\{0}\low100px\".fmt(Name));
			if (Directory.Exists(pathSave) == false)
				Directory.CreateDirectory(pathSave);

			XDocument xdoc = new XDocument(new XElement("components") ).declare();
			XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
			xdoc.Root.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi), new XAttribute(xsi + "noNamespaceSchemaLocation", "schema/layout-schema.xsd"));

			#region me to component
			XElement xeMe = new XElement(Air.COMPONENT, new XAttribute(Air.CLASS, Air.getComp(this)), new XAttribute(Air.ID, "main")
					, new XElement(Air.CONSTANTS
						, childs.OrderBy(si => si.DrawOrder).Select(sitem => sitem.toXmlConstant))
					, new XElement(Air.CHILDS
						, childs.OrderBy(si => si.DrawOrder).Select(sitem => sitem.toXmlChild))
					, new XElement(Air.LAYOUT
						, childs.OrderBy(si => si.DrawOrder).Select(sitem => sitem.toXmlLayout))
			);
			xdoc.Root.Add(xeMe);
			#endregion

			xdoc.Root.Add(this.getChildsAll().Select(si => si.toXmlComponent));
			xdoc.Save(Path.Combine(pathSave, "layout.xml"));
		}//function

		public void saveToXmlInitializer()
		{
			string pathSave = Path.Combine(SaveDir, @"theme\{0}\layout\".fmt(Name));
			if (Directory.Exists(pathSave) == false)
				Directory.CreateDirectory(pathSave);

			XDocument xdoc = new XDocument(new XElement("initializers")).declare();
			XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
			xdoc.Root.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi), new XAttribute(xsi + "noNamespaceSchemaLocation", "schema/component-initializer-definition.xsd")	);

			//xdoc.Root.Add(this.toXmlComponent());
			xdoc.Root.Add(this.getChildsAll().Select(si => si.toXmlInitializer).Where(xe => xe != null));
			xdoc.Save(Path.Combine(pathSave, "componentInitializerDefinition.xml"));
		}//function
	}//class
}//ns
