using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace synesis
{
	public partial class frmMain : Form
	{
		string curDir = Environment.CurrentDirectory;
		public frmMain()
		{
			InitializeComponent();
		}

		private void btnOpenFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = curDir;
			DialogResult result = dlg.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				curDir = dlg.SelectedPath;
				TreeView tv = tvMain;
				TreeNode node;
				tv.Nodes.Clear();
				foreach (var sheet in SpriteSheet.getFromTheme(dlg.SelectedPath))
				{
					node = tvMain.addNode(sheet, false);
					foreach (var frame in sheet.Frames)
					{
						node.addNode(frame, false);
					}//for
				}//for
			}//if
		}

		private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			if (node.Tag is Frame)
			{
				Frame frame = (node.Tag as Frame);
				pictureSprite.Image = frame.Image;
			}//if
		}//function
	}//class

	public static class TreeExtension
	{
		public static TreeNode addNode(this TreeView tv, object obj, bool withDummy)
		{
			TreeNode node = new TreeNode(obj.ToString());
			node.Tag = obj;
			if (withDummy)
				node.Nodes.Add(new TreeNode());

			tv.Nodes.Add(node);
			return node;
		}//function

		public static TreeNode addNode(this TreeNode parent, object obj, bool withDummy)
		{
			TreeNode node = new TreeNode(obj.ToString());
			node.Tag = obj;
			if (withDummy)
				node.Nodes.Add(new TreeNode());

			parent.Nodes.Add(node);

			return node;
		}//function
	}//class

}//ns
