using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	/// <summary>
	/// common msg
	/// </summary>
	class Msg
	{
		public static string fileNotFound(string file)
		{
			return string.Format("file not found - {0}", file);
		}//function

		public static string typeWrong(string yes, string not)
		{
			return string.Format("type must be {0}, not {1}", yes, not);
		}//function
	}//class
}//ns
