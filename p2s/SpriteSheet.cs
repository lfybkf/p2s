using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public class SpriteSheet
	{
		public static readonly string TYPE = "SpriteFrameSheet";
		public static readonly string fileMask = "*.spr";
		public static readonly string fileExtension = ".spr";
		//=================
		public string name { get; private set;}
		string atlas;
		IList<Frame> frames = new List<Frame>();
		public int size { get { return frames.Count(); } }
		public IEnumerable<Frame> Frames { get { return frames; } }
		Bitmap atlasImage = null;
		//=================

		public SpriteSheet(string name)
		{
			this.name = name;
		}//function

		public string baseDir
		{
			get { Scene scene = Scene.takeScene(this); return scene != null ? scene.baseDir : Environment.CurrentDirectory; }
		}//function

		public bool load()
		{
			try
			{
				string file = Path.Combine(baseDir, name);
				string fileMask = Path.GetFileNameWithoutExtension(name) + ".*";
				if (File.Exists(file) == false)
				{ //try to find smth else (no sheet.png? may be - sheet.spr?)
					Logger.def.warn("SpriteSheet file {0} is absent. We'll find smth on name.".fmt(file));
					file = Directory.EnumerateFiles(baseDir, fileMask).FirstOrDefault();
				}//if
				string json = File.ReadAllText(file);
				return load(json);
			}
			catch (Exception e)
			{
				Logger.def.err("SpriteSheet {0} is absent".fmt(name));
				Logger.def.err(e.Message);
				return false;
			}
		}//function

		public bool load(string jsonString)
		{
			try
			{
			JsonObject jo = new JsonObject(jsonString);
			string type = jo.get("header.type");
			if (type != TYPE)
				return false;

			atlas = jo.get("resource.meta.image");
			JsonArray jaFrames = new JsonArray(jo.get("resource.frames"));
			Frame spr;
			for (int i = 0; i < jaFrames.Count; i++)
			{
				spr = new Frame();
				spr.num = i;
				spr.load(jaFrames.get(i));
				spr.sheet = this;
				frames.Add(spr);
			}//for
			}
			catch (Exception e)
			{
				Logger.def.err(this.ToString());
				Logger.def.err(e.Message);
				return false;
			}

			return true;
		}//function

		public bool isContentValid
		{
			get 
			{
				var listOfInvalid = frames.Where(sprite => sprite.isValid == false);
				return listOfInvalid.Any() == false;
			}
		}//function

		public Frame getFrame(int index)
		{
			return frames.ElementAtOrDefault(index);
		}//function

		public Image getImage(int index)
		{
			Image Ret = null;

			//create atlas if need
			if (atlasImage == null)
			{
				string fileAtlas = Path.Combine(baseDir, atlas);
				if (File.Exists(fileAtlas) == false)
					return null;
				atlasImage = new Bitmap(fileAtlas);
			}//if

			//crop sprite
			Frame frame = getFrame(index);
			System.Drawing.Imaging.PixelFormat format = atlasImage.PixelFormat;
			Ret = atlasImage.Clone(frame.rectangle, format);

			return Ret;
		}//function

		public override string ToString()
		{
			return "{0} - {1}".fmt(TYPE, name);
		}//function
	}//class
}//ns
