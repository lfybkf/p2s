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
		static string INFO = "info: {0}\r\n";
		static string WARN = "warn: {0}\r\n";
		static string ERR = "err: {0}\r\n";
		static readonly Logger instance = new Logger();
		//================
		string fileName = nameDefault;
		//================

		public static Logger def
		{
			get { return instance; }
		}//function

		public void info(string msg)
		{
			File.AppendAllText(fileName, INFO.fmt(msg));
		}//function

		public void warn(string msg)
		{
			File.AppendAllText(fileName, WARN.fmt(msg));
		}//function

		public void err(string msg)
		{
			File.AppendAllText(fileName, ERR.fmt(msg));
		}//function


	}//class
}//ns
