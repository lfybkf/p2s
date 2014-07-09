using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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

		public static JsonObject get(this JsonArray ja, int index)		{		Object o = ja[index];		return (JsonObject)o;	}//function

	}//class

	public static class StringExtension
	{
		static char[] forbiddenForIdent = " -_.,".ToCharArray();

		public static string fmt(this string s, params string[] args)	{	return string.Format(s, args);	}//function
		
		public static string onlySymbols(this string s)
		{
			return new String(s.ToCharArray().Where(ch => !forbiddenForIdent.Contains(ch)).ToArray());
		}//function
	}//class


	public static class XDocumentExtension
	{
		static XDeclaration xdecl = new XDeclaration("1.0", "UTF-8", null);
		public static XDocument declare(this XDocument x)	{	x.Declaration = xdecl; return x;	}//function
		public static XDocument comment(this XDocument x, string s) { x.Add(new XComment(s)); return x; }//function
		public static XElement comment(this XElement x, string s) { x.Add(new XComment(s)); return x; }//function
	}//class

	//public static class XmlDocumentExtension
	//{
	//	public static XmlDocument buildDefaultDeclaration(this XmlDocument doc)
	//	{
	//		doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
	//		return doc;
	//	}//function

	//	public static XmlDocument buildComment(this XmlDocument doc, string s)
	//	{
	//		doc.AppendChild(doc.CreateComment(s));
	//		return doc;
	//	}//function

	//	public static XmlElement buildRoot(this XmlDocument doc, string sRoot)
	//	{
	//		XmlElement root = doc.CreateElement(sRoot);
	//		doc.AppendChild(root);
	//		return root;
	//	}//function

	//	public static XmlDocument buildNS(this XmlDocument doc, string nsName, string nsUri)
	//	{
	//		XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
	//		nsm.AddNamespace(nsName, nsUri);
	//		return doc;
	//	}//function

	//	public static XmlElement buildAttrNS(this XmlElement elem, string prefix, string name, string uri, string value)
	//	{
	//		XmlAttribute attr = elem.OwnerDocument.CreateAttribute(prefix, name, uri);
	//		attr.Value = value;
	//		elem.SetAttributeNode(attr);
	//		return elem;
	//	}//function

	//	public static XmlElement buildAttributes(this XmlElement elem, params string[] pairs)
	//	{
	//		string name, value;
	//		for (int i = 0; i < pairs.Length; i+=2)
	//		{
	//			name = pairs[i]; value = pairs[i + 1];
	//			elem.SetAttribute(name, value);
	//		}//for
	//		return elem;
	//	}//function

	//	public static XmlElement buildChild(this XmlElement elem, string name)
	//	{
	//		XmlElement node = elem.OwnerDocument.CreateElement(name);
	//		elem.AppendChild(node);
	//		return node;
	//	}//function
	//}//class

}//ns
