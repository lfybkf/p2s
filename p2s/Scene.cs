using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
		string fileName;
		JsonObject joMain;
		public string baseDir { get { return Path.GetDirectoryName(fileName); } }
		public IEnumerable<SpriteSheet> Sheets { get { return sheets; } }
		//====================

		public Scene(string fileName)
		{
			this.fileName = fileName;
			scenes.Add(this);
		}//function

		public bool load()
		{
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
			#endregion

			#region sheets
			/*
			IEnumerable<string> sheetNamesFromSprites = childs
				.Where(sceneItem => sceneItem is Sprite)
				.Select(sprite => ((Sprite)sprite).sheetName)
				.Distinct();

			SpriteSheet sheet;
			foreach (string sheetName in sheetNamesFromSprites)
			{
				sheet = new SpriteSheet(sheetName);
				sheets.Add(sheet);
				sheet.load();
			}//for
			*/
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

		public static Scene takeScene(SceneItem item)
		{
			return scenes.FirstOrDefault(scene => scene.contains(item));
		}//function

		public static Scene takeScene(SpriteSheet item)
		{
			return scenes.FirstOrDefault(scene => scene.sheets.Contains(item));
		}//function

		public override string ToString()
		{
			return "Scene: {0}".fmt(fileName);
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



		public IEnumerable<SceneItem> getChilds()
		{
			return childs;
		}
	}//class
}//ns
