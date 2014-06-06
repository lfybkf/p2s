using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	class Logger
	{
		static string nameDefault = "application.log";
		static string INFO = "info: ";
		static string WARN = "warn: ";
		static string ERR = "err: ";
		static Logger instance = new Logger();
		//================
		string fileName = nameDefault;
		//================

		public static Logger def
		{
			get { return instance; }
		}//function

		public void info(string msg)
		{
			File.AppendAllText(fileName, INFO + msg);
		}//function

		public void warn(string msg)
		{
			File.AppendAllText(fileName, WARN + msg);
		}//function

		public void err(string msg)
		{
			File.AppendAllText(fileName, ERR + msg);
		}//function


	}//class
}//ns
