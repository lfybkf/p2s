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
			this.splitContainer1.Panel1.Controls.Add(this.btnSave);
			this.splitContainer1.Panel1.Controls.Add(this.btnOpenFolder);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pictureSprite);
			this.splitContainer1.Size = new System.Drawing.Size(966, 578);
			this.splitContainer1.SplitterDistance = 321;
			this.splitContainer1.TabIndex = 1;
			// 
			// tvMain
			// 
			this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvMain.Location = new System.Drawing.Point(0, 40);
			this.tvMain.Name = "tvMain";
			this.tvMain.Size = new System.Drawing.Size(321, 492);
			this.tvMain.TabIndex = 1;
			this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
			// 
			// btnSave
			// 
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnSave.Location = new System.Drawing.Point(0, 532);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(321, 46);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// btnOpenFolder
			// 
			this.btnOpenFolder.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnOpenFolder.Location = new System.Drawing.Point(0, 0);
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new System.Drawing.Size(321, 40);
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
			this.pictureSprite.Size = new System.Drawing.Size(641, 578);
			this.pictureSprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureSprite.TabIndex = 0;
			this.pictureSprite.TabStop = false;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(966, 578);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "frmMain";
			this.Text = "Form1";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureSprite)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tvMain;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnOpenFolder;
		private System.Windows.Forms.PictureBox pictureSprite;
	}
}

