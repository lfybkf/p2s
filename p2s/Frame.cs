using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public class Frame
	{
		static readonly string fmtMain = "({0}) x={1} y={2} h={3} w={4}";
		static readonly string fmtWithName = "({0}) x={1} y={2} h={3} w={4} name={5}";
		//===================
		string x, y, h, w;
		string name;
		public int num = -1;
		public int X { get { return Int32.Parse(x); } }
		public int Y { get { return Int32.Parse(y); } }
		public int H { get { return Int32.Parse(h); } }
		public int W { get { return Int32.Parse(w); } }
		public Rectangle rectangle { get { return new Rectangle(X, Y, W, H); } }

		internal SpriteSheet sheet;
		//===================
		public Frame()
		{
			;
		}//function

		public override string ToString()
		{
			if (name == null)
				return string.Format(fmtMain, num, x, y, h, w);
			else
				return string.Format(fmtWithName, num, x, y, h, w, name);
		}//function

		public bool isValid
		{
			get
			{
				bool b = string.IsNullOrWhiteSpace(x) == false
					&& string.IsNullOrWhiteSpace(y) == false
					&& string.IsNullOrWhiteSpace(w) == false
					&& string.IsNullOrWhiteSpace(h) == false;
				return b;
			}
		}//function

		public void load(JsonObject jo)
		{
			name = jo.get("filename") ?? string.Empty;
			x = jo.get("frame.x");
			y = jo.get("frame.y");
			h = jo.get("frame.h");
			w = jo.get("frame.w");
		}//function

		public Image getImage()
		{
			return sheet.getImage(num);
		}//function
	}//class
}//ns
