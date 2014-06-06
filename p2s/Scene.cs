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
	public class Scene
	{

		static List<Scene> scenes = new List<Scene>();
		//=====================
		IList<SceneItem> childs = new List<SceneItem> ();
		IList<SpriteSheet> sheets = new List<SpriteSheet>();
		string fileName;
		public string baseDir { get { return Path.GetDirectoryName(fileName); } }
		//====================

		public Scene(string fileName)
		{
			this.fileName = fileName;
			scenes.Add(this);
		}//function

		public bool load()
		{
			JsonObject jo = null;
			#region loading and testing
			if (File.Exists(fileName) ==false)
			{
				Logger.def.err(Msg.fileNotFound(fileName));
				return false;
			}//if

			try
			{
				string s = File.ReadAllText(fileName);
				jo = new JsonObject(s);
			}
			catch (Exception e)
			{
				Logger.def.err(e.Message);
				return false;
			}//try

			string typeMust = "Scene";
			string typeCurrent = jo.get("header.type");
			if (typeMust != typeCurrent)
			{
				Logger.def.err(Msg.typeWrong(typeMust, typeCurrent));
				return false;
			}//if
			#endregion

			#region childs
			JsonArray jaChilds = new JsonArray(jo.get("resource.root.children"));
			bool isChildsOK = SceneItem.fillChilds(jaChilds, childs);
			#endregion

			#region sheets
			IEnumerable<string> sheetNamesFromSprites = childs
				.Where(sceneItem => sceneItem is Sprite)
				.Select(sprite => ((Sprite)sprite).sheetName)
				.Distinct();

			SpriteSheet sheet;
			foreach (string sheetName in sheetNamesFromSprites)
			{
				sheet = new SpriteSheet(sheetName);
				sheet.load();
				sheets.Add(sheet);
			}//for
			#endregion

			return isChildsOK;
		}//function

		public SpriteSheet getSpriteSheet(string sheetName)
		{
			return sheets.FirstOrDefault(sheet => sheet.name == sheetName);
		}//function

		public static Scene takeScene(SceneItem item)
		{
			return scenes.FirstOrDefault(scene => scene.childs.Contains(item));
		}//function

		public static Scene takeScene(SpriteSheet item)
		{
			return scenes.FirstOrDefault(scene => scene.sheets.Contains(item));
		}//function
	}//class
}//ns
