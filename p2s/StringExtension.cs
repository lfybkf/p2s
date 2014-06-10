using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	public static class StringExtension
	{
		public static string fmt(this string s, params string[] args)
		{
			return string.Format(s, args);
		}//function
	}
}
