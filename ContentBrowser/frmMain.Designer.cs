namespace synesis
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tvMain = new System.Windows.Forms.TreeView();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnOpenFolder = new System.Windows.Forms.Button();
			this.pictureSprite = new System.Windows.Forms.PictureBox();
			this.ctlView = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureSprite)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tvMain);
			this.splitContainer1.Panel1.Controls.Add(this.ctlView);
			this.splitContainer1.Panel1.Controls.Add(this.btnSave);
			this.splitContainer1.Panel1.Controls.Add(this.btnOpenFolder);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pictureSprite);
			this.splitContainer1.Size = new System.Drawing.Size(937, 546);
			this.splitContainer1.SplitterDistance = 312;
			this.splitContainer1.TabIndex = 0;
			// 
			// tvMain
			// 
			this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvMain.Location = new System.Drawing.Point(0, 40);
			this.tvMain.Name = "tvMain";
			this.tvMain.Size = new System.Drawing.Size(312, 335);
			this.tvMain.TabIndex = 1;
			this.tvMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvSpriteSheet_BeforeExpand);
			this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSpriteSheet_AfterSelect);
			// 
			// btnSave
			// 
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnSave.Location = new System.Drawing.Point(0, 500);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(312, 46);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Visible = false;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnOpenFolder
			// 
			this.btnOpenFolder.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnOpenFolder.Location = new System.Drawing.Point(0, 0);
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new System.Drawing.Size(312, 40);
			this.btnOpenFolder.TabIndex = 0;
			this.btnOpenFolder.Text = "Open Folder";
			this.btnOpenFolder.UseVisualStyleBackColor = true;
			this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
			// 
			// pictureSprite
			// 
			this.pictureSprite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureSprite.Location = new System.Drawing.Point(0, 0);
			this.pictureSprite.Name = "pictureSprite";
			this.pictureSprite.Size = new System.Drawing.Size(621, 546);
			this.pictureSprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureSprite.TabIndex = 0;
			this.pictureSprite.TabStop = false;
			// 
			// ctlView
			// 
			this.ctlView.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ctlView.Location = new System.Drawing.Point(0, 375);
			this.ctlView.Multiline = true;
			this.ctlView.Name = "ctlView";
			this.ctlView.ReadOnly = true;
			this.ctlView.Size = new System.Drawing.Size(312, 125);
			this.ctlView.TabIndex = 3;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(937, 546);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmMain";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureSprite)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btnOpenFolder;
		private System.Windows.Forms.TreeView tvMain;
		private System.Windows.Forms.PictureBox pictureSprite;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox ctlView;
	}
}

