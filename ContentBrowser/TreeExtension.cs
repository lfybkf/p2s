using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace synesis
{
	public static class TreeExtension
	{
		public static void addNode(this TreeView tv, object obj, bool withDummy)
		{
			TreeNode node = new TreeNode(obj.ToString());
			node.Tag = obj;
			if (withDummy)
				node.Nodes.Add(new TreeNode());

			tv.Nodes.Add(node);
		}//function

		public static void addNode(this TreeNode parent, object obj, bool withDummy)
		{
			TreeNode node = new TreeNode(obj.ToString());
			node.Tag = obj;
			if (withDummy)
				node.Nodes.Add(new TreeNode());

			parent.Nodes.Add(node);
		}//function
	}//class
}
