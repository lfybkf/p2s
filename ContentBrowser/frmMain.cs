using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
		Scene scene;
		TreeNode dummy = new TreeNode();

		public frmMain()
		{
			InitializeComponent();
		}

		private void tvSpriteSheet_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			if (node.Tag is Sprite)
			{
				Sprite sprite = (Sprite)node.Tag;
				pictureSprite.Image = sprite.getImage(); 
			}//if
			else if (node.Tag is Frame)
			{
				pictureSprite.Image = (node.Tag as Frame).getImage();
			}//if
			
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			this.Text = "GameContent";
			//doLoad();
		}

		private void btnOpenFolder_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Title = "Choose scene.object";
			dialog.Filter = "Scene|*.object";
			dialog.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();
			if (scene != null)
				dialog.InitialDirectory = scene.baseDir;

			DialogResult res = dialog.ShowDialog();
			if (res == System.Windows.Forms.DialogResult.OK)
			{
				scene = new Scene(dialog.FileName);
				scene.load();

				tvSpriteSheet.Nodes.Clear();
				tvSpriteSheet.addNode(scene, true);
			}//if
		}//function

		private void tvSpriteSheet_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;
			if (node.Tag is IContainerOfSceneItems)
			{
				node.Nodes.Clear();
				foreach (var item in (node.Tag as IContainerOfSceneItems).getChilds())
				{
					node.addNode(item, (item as IContainerOfSceneItems).hasChilds());
				}//for
			}//if
			else if (node.Tag is SpriteSheet)
			{
				node.Nodes.Clear();
				SpriteSheet sheet = (SpriteSheet)node.Tag;
				foreach (var item in sheet.Frames)
				{
					node.addNode(item, false);
				}//for
			}//if

			//this worked upper, but scene has spritesheet also
			if (node.Tag is Scene)
			{ 
				foreach (var item in (node.Tag as Scene).Sheets)
				{
					node.addNode(item, true);
				}//for
			}//if
		}//function

	}//class
}
