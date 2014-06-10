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
			string json = File.ReadAllText(Path.Combine(baseDir, name));
			return load(json);
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
				atlasImage = new Bitmap(Path.Combine(baseDir, atlas));
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
