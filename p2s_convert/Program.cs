using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	class Program
	{
		static void Main(string[] args)
		{
			string fileScene, dirOutput;
			if (args.Length != 2)			{printHelp(); return;}

			fileScene = args[0];
			dirOutput = args[1];

			if (!File.Exists(fileScene))
			{ Console.WriteLine("fileScene not exists: {0}".fmt(fileScene)); printHelp(); return; }

			if (!File.Exists(fileScene))
			{ Console.WriteLine("dirOutput not exists: {0}".fmt(dirOutput)); printHelp(); return; }

			Console.WriteLine("fileScene: {0}".fmt(fileScene));
			Console.WriteLine("dirOutput: {0}".fmt(dirOutput));
			Scene scene = new Scene(fileScene);
			scene.Name = "playtika";
			scene.load();
			scene.SaveDir = dirOutput;
			scene.saveToXmlTheme();
			scene.saveToXmlLayout();
			scene.saveToXmlInitializer();
		}//function

		static void printHelp()
		{
			Console.WriteLine("Usage: exe fileScene dirOutput");
			Console.Read();
		}//function
	}//class
}//ns
