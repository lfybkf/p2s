using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	public class Idgen
	{
		static IDictionary<string, int> seq = new Dictionary<string, int>();
		//================
		
		//================
		public static int next(string key)
		{
			if (seq.Keys.Any(k => k == key) == false)
				seq.Add(key, 0);
			
			int n = seq[key];
			n++;
			seq[key] = n;
			return n;
		}//function

		public static string next(object o)
		{
			string s = o.GetType().Name;
			return s + next(s).ToString() ;
		}//function
	}//class
}//ns
