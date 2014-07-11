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
		internal string curDir = null;
		public frmMain()
		{
			InitializeComponent();
		}

		void runOpenFolder(string path)
		{
			curDir = System.IO.Path.GetDirectoryName(path);
			TreeView tv = tvMain;
			TreeNode node;
			tv.Nodes.Clear();
			foreach (var sheet in SpriteSheet.getFromTheme(curDir))
			{
				node = tvMain.addNode(sheet, false);
				foreach (var frame in sheet.Frames)
				{
					node.addNode(frame, false);
				}//for
			}//for

			this.Text = curDir;
		}//function

		private void btnOpenFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = curDir;
			DialogResult result = dlg.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				runOpenFolder(dlg.SelectedPath);
			}//if
		}//function

		private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			if (node.Tag is Frame)
			{
				Frame frame = (node.Tag as Frame);
				pictureSprite.Image = frame.Image;
			}//if
		}//function

		private void frmMain_Load(object sender, EventArgs e)
		{
			if (curDir != null)
				runOpenFolder(curDir);
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
