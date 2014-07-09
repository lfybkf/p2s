using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ComputerBeacon.Json;

namespace synesis
{
	public class SpriteSheet
	{
		public static readonly string TYPE = "SpriteFrameSheet";
		public static readonly string fileMask = "*.spr";
		public static readonly string fileExtension = ".spr";
		//=================
		public string name { get; private set;} //file only
		public string ident { get; private set; } //name corrected to be ident
		public string Atlas { get; private set; }	//name of big image-file
		IList<Frame> frames = new List<Frame>();
		public string BaseDir { get { return Scene == null ? baseDir : Scene.BaseDir; } set { baseDir = value; } }
		string baseDir = Environment.CurrentDirectory;

		public string baseName { get { return Path.GetFileNameWithoutExtension(name); } }
		public int size { get { return frames.Count(); } }
		public IEnumerable<Frame> Frames { get { return frames; } }
		Bitmap atlasImage = null;
		public Scene Scene { get { return Scene.takeScene(this); } }


		//public string saveDir { get { return Scene.SaveDir; } }
		//=================

		public SpriteSheet(string name) { this.name = name; this.ident = name.onlySymbols();  }//constructor
		public SpriteSheet(string name, IEnumerable<Frame> frames) : this(name) { this.frames.Concat(frames); }//constructor
		public Frame getFrame(int index) { return frames.ElementAtOrDefault(index); }//function
		public override string ToString() { return "{0} - {1}".fmt(TYPE, name); }//function

		public bool load()
		{
			try
			{
				string file = Path.Combine(BaseDir, name);
				string fileMask = baseName + ".*";
				if (File.Exists(file) == false)
				{ //try to find smth else (no sheet.png? may be - sheet.spr?)
					Logger.def.warn("SpriteSheet file {0} is absent. We'll find smth on name".fmt(file));
					file = Directory.EnumerateFiles(BaseDir, fileMask).FirstOrDefault();
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

			Atlas = jo.get("resource.meta.image");
			JsonArray jaFrames = new JsonArray(jo.get("resource.frames"));
			Frame frame;
			for (int i = 0; i < jaFrames.Count; i++)
			{
				frame = new Frame();
				frame.num = i;
				frame.load(jaFrames.get(i));
				frame.sheet = this;
				frames.Add(frame);
				frame.Name = ident + i.ToString();//works only if frame.Name is empty
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


		public Image getImage(int index)		
		{			
			Image Ret = null;

			//create atlas if need
			if (atlasImage == null)
			{
				string fileAtlas = Path.Combine(BaseDir, Atlas);
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

		public bool load(XDocument xdoc)
		{
			Atlas = xdoc.Root.Attribute("imagePath").Value;
			Frame spr;
			int i = 0;
			foreach (XElement elem in xdoc.Root.Elements() )
			{
				spr = new Frame();
				spr.num = i++;
				spr.load(elem);
				spr.sheet = this;
				frames.Add(spr);
			}//for
			return true;

		}//function

		public static IEnumerable<SpriteSheet> getFromTheme(string dir)
		{
			IEnumerable<string> xml_files = Directory.GetFiles(dir, "*.xml").Where(s => Path.GetFileNameWithoutExtension(s) != "textureScaleDefinition");
			List<SpriteSheet> Ret = new List<SpriteSheet>();
			SpriteSheet sheet;
			foreach (var file in xml_files)
			{
				sheet = new SpriteSheet(Path.GetFileNameWithoutExtension(file));
				sheet.load(XDocument.Parse(File.ReadAllText(file)));
				sheet.baseDir = dir;
				Ret.Add(sheet);
			}//for
			return Ret;
		}//function

		public static IEnumerable<SpriteSheet> mergeOnAtlas(IEnumerable<SpriteSheet> sheets)
		{

			IEnumerable<string> atlases = sheets.Select(sheet => sheet.Atlas).Distinct();
			Func<string, IList<Frame>> framesOnAtlas = (atlas) => sheets.Where(sh => sh.Atlas == atlas).SelectMany(sh => sh.Frames).ToList();
			IEnumerable<SpriteSheet> Ret = atlases.Select(atlas => new SpriteSheet(atlas.onlySymbols()) {frames = framesOnAtlas(atlas), Atlas = atlas});
			return Ret;
		}//function

		public string[] View
		{
			get
			{
				string[] ss = new string[] { "name={0}".fmt(name), "ident={0}".fmt(ident), "atlas={0}".fmt(Atlas), "size={0}".fmt(size.ToString()) };
				return ss;
			}//get
		}//function
	}//class

	 
}//ns
