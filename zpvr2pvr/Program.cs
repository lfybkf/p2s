using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace zpvr2pvr
{
	class Program
	{
		private static byte[] signature = System.Text.UTF8Encoding.UTF8.GetBytes("ZPVR");

		public static bool isCompatible(Stream st)
		{
			bool res = true;
			byte[] sign = new byte[4];
			st.Read(sign, 0, 4);
			for (int i = 0; i < 4; ++i)
			{
				if (signature[i] != sign[i])
				{
					res = false;
					break;
				}
			}
			return res;
		}//function

		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("usage: exe zpvr_file");
				Console.WriteLine("output: zpvr_file.pvr");
				Console.ReadLine();
				return;
			}//if

			string fileIn = args[0];
			using (var sr = new FileStream(fileIn, FileMode.Open, FileAccess.Read, FileShare.Read, 1024))
			{
				if (isCompatible(sr))
				{
					sr.Seek(4, SeekOrigin.Begin);
					using (var sw = File.Create(fileIn + ".pvr"))
					{
						sr.CopyTo(sw);
					}//using
				}//if
			}//using

		}//function
	}//class
}//ns
