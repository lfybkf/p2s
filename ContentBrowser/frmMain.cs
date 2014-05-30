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
		string keySpriteSeeet = "keySpriteSeeet";
		string keySprite = "keySprite";
		TreeNode dummy = new TreeNode();
		string folderGameContent = null;

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
				Image img = ContentManager.getImage(sprite);
				pictureSprite.Image = img;
			}//if
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			this.Text = "GameContent";
			//doLoad();
		}

		private void btnOpenFolder_Click(object sender, EventArgs e)
		{

/*
			string propName = "folderGameContent";
			Properties.Settings settings = Properties.Settings.Default;
			string folderGameContent = (string)settings[propName];
			if (folderGameContent == null)
			{
				SettingsProperty prop = new SettingsProperty(propName);
				settings.Properties.Add(prop);
				settings.Save();
			}//if
*/

			//choose folder
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Choose folder with game content";
			dialog.RootFolder = Environment.SpecialFolder.MyComputer;
			dialog.ShowNewFolderButton = false;
			if (folderGameContent != null)
				dialog.SelectedPath = folderGameContent;

			//result
			DialogResult res = dialog.ShowDialog();
			if (res == System.Windows.Forms.DialogResult.OK)
			{
				ContentManager.BasePath = dialog.SelectedPath;
				folderGameContent = dialog.SelectedPath;
				this.Text = folderGameContent;
				doLoad();
			}//if
		}

		public void refreshSpriteSheetView()
		{
			TreeView tv = tvSpriteSheet;
			tv.Nodes.Clear();
			IEnumerable<string> files = ContentManager.getSpriteSheetFiles();
			TreeNode node;
			foreach (var item in files)
			{
				node = new TreeNode(Path.GetFileName(item));
				node.ImageKey = keySpriteSeeet;
				node.Nodes.Add(new TreeNode());
				tv.Nodes.Add(node);
			}//for
		}

		private void tvSpriteSheet_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;
			TreeNode nodeSprite;
			if (node.ImageKey == keySpriteSeeet && node.Tag == null)
			{
				node.Nodes.Clear();
				SpriteSheet obj = ContentManager.getSpriteSheet(node.Text);
				foreach (var item in obj.content)
				{
					nodeSprite = new TreeNode(item.ToString());
					nodeSprite.ImageKey = keySprite;
					nodeSprite.Tag = item;
					node.Nodes.Add(nodeSprite);
				}//for
			}//if
		}//function

		private void doLoad()
		{
			refreshSpriteSheetView();
		}//function
	}//class
}
