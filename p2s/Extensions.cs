using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerBeacon.Json;

namespace synesis
{
	public static class JsonObjectExtension
	{
		public static String get(this JsonObject jo, string path)
		{ 
			return jo.get(path.Split('.'));
		}//function

		public static String get(this JsonObject jo, IEnumerable<string> path)
		{
			int pathCount = path.Count();
			if (pathCount == 0) //wrong arg
				return null;

			string key = path.ElementAt(0);
			string value = jo.get(key, null);
			if (pathCount == 1) //end of recursion
				return value;

			if (string.IsNullOrWhiteSpace(value)) //str isnt json
				return null;

			//path has more? then recursion
			if (pathCount > 1)
			{
				JsonObject joChild = new JsonObject(value);
				return joChild.get(path.Skip(1));
			}//if
			else //something wrong, end of all
				return null;
		}//function

		public static string get(this JsonObject jo, string key, string def)
		{
			Object value = null;
			bool b = jo.TryGetValue(key, out value);

			if (b == false)
				return def;

			if (value == null)
				return null;

			return value.ToString();
		}//function

		public static int get(this JsonObject jo, string key, int def)
		{
			string value = jo.get(key, null);

			if (value == null)
				return def;

			int Ret;
			if (Int32.TryParse(value, out Ret))
				return Ret;
			else
				return def;

		}//function

		public static JsonObject get(this JsonArray ja, int index)
		{
			Object o = ja[index];
			return (JsonObject)o;
		}//function

	}//class

	public static class StringExtension
	{
		public static string fmt(this string s, params string[] args)
		{
			return string.Format(s, args);
		}//function
	}//class


}//ns
