namespace markojudas_music
{
    partial class AddBand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBand));
            this.bname = new System.Windows.Forms.TextBox();
            this.bandName = new System.Windows.Forms.Label();
            this.bandImagePath = new System.Windows.Forms.TextBox();
            this.bandPhoto = new System.Windows.Forms.Label();
            this.albumName = new System.Windows.Forms.Label();
            this.aName = new System.Windows.Forms.TextBox();
            this.albumCoverPath = new System.Windows.Forms.Label();
            this.albumCoverImgPath = new System.Windows.Forms.TextBox();
            this.btnBandPhoto = new System.Windows.Forms.Button();
            this.btnAlbumPhoto = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.cnlBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bname
            // 
            this.bname.Location = new System.Drawing.Point(84, 18);
            this.bname.Name = "bname";
            this.bname.Size = new System.Drawing.Size(265, 20);
            this.bname.TabIndex = 0;
            // 
            // bandName
            // 
            this.bandName.AutoSize = true;
            this.bandName.Location = new System.Drawing.Point(12, 21);
            this.bandName.Name = "bandName";
            this.bandName.Size = new System.Drawing.Size(66, 13);
            this.bandName.TabIndex = 1;
            this.bandName.Text = "Band Name:";
            // 
            // bandImagePath
            // 
            this.bandImagePath.Location = new System.Drawing.Point(84, 44);
            this.bandImagePath.Name = "bandImagePath";
            this.bandImagePath.ReadOnly = true;
            this.bandImagePath.Size = new System.Drawing.Size(133, 20);
            this.bandImagePath.TabIndex = 2;
            // 
            // bandPhoto
            // 
            this.bandPhoto.AutoSize = true;
            this.bandPhoto.Location = new System.Drawing.Point(12, 47);
            this.bandPhoto.Name = "bandPhoto";
            this.bandPhoto.Size = new System.Drawing.Size(66, 13);
            this.bandPhoto.TabIndex = 3;
            this.bandPhoto.Text = "Band Photo:";
            // 
            // albumName
            // 
            this.albumName.AutoSize = true;
            this.albumName.Location = new System.Drawing.Point(12, 73);
            this.albumName.Name = "albumName";
            this.albumName.Size = new System.Drawing.Size(62, 13);
            this.albumName.TabIndex = 5;
            this.albumName.Text = "Album Title:";
            // 
            // aName
            // 
            this.aName.Location = new System.Drawing.Point(84, 70);
            this.aName.Name = "aName";
            this.aName.Size = new System.Drawing.Size(267, 20);
            this.aName.TabIndex = 4;
            // 
            // albumCoverPath
            // 
            this.albumCoverPath.AutoSize = true;
            this.albumCoverPath.Location = new System.Drawing.Point(12, 100);
            this.albumCoverPath.Name = "albumCoverPath";
            this.albumCoverPath.Size = new System.Drawing.Size(70, 13);
            this.albumCoverPath.TabIndex = 7;
            this.albumCoverPath.Text = "Album Cover:";
            // 
            // albumCoverImgPath
            // 
            this.albumCoverImgPath.Location = new System.Drawing.Point(84, 97);
            this.albumCoverImgPath.Name = "albumCoverImgPath";
            this.albumCoverImgPath.ReadOnly = true;
            this.albumCoverImgPath.Size = new System.Drawing.Size(133, 20);
            this.albumCoverImgPath.TabIndex = 6;
            // 
            // btnBandPhoto
            // 
            this.btnBandPhoto.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBandPhoto.Location = new System.Drawing.Point(232, 44);
            this.btnBandPhoto.Name = "btnBandPhoto";
            this.btnBandPhoto.Size = new System.Drawing.Size(75, 23);
            this.btnBandPhoto.TabIndex = 8;
            this.btnBandPhoto.Text = "Add Image";
            this.btnBandPhoto.UseVisualStyleBackColor = false;
            this.btnBandPhoto.Click += new System.EventHandler(this.btnBandPhoto_Click);
            // 
            // btnAlbumPhoto
            // 
            this.btnAlbumPhoto.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAlbumPhoto.Location = new System.Drawing.Point(232, 96);
            this.btnAlbumPhoto.Name = "btnAlbumPhoto";
            this.btnAlbumPhoto.Size = new System.Drawing.Size(75, 23);
            this.btnAlbumPhoto.TabIndex = 9;
            this.btnAlbumPhoto.Text = "Add Image";
            this.btnAlbumPhoto.UseVisualStyleBackColor = false;
            this.btnAlbumPhoto.Click += new System.EventHandler(this.BtnAlbumPhoto_Click);
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBtn.Location = new System.Drawing.Point(84, 153);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 10;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // cnlBtn
            // 
            this.cnlBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cnlBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cnlBtn.Location = new System.Drawing.Point(207, 153);
            this.cnlBtn.Name = "cnlBtn";
            this.cnlBtn.Size = new System.Drawing.Size(75, 23);
            this.cnlBtn.TabIndex = 11;
            this.cnlBtn.Text = "Cancel";
            this.cnlBtn.UseVisualStyleBackColor = false;
            // 
            // AddBand
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.CancelButton = this.cnlBtn;
            this.ClientSize = new System.Drawing.Size(385, 190);
            this.Controls.Add(this.cnlBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.btnAlbumPhoto);
            this.Controls.Add(this.btnBandPhoto);
            this.Controls.Add(this.albumCoverPath);
            this.Controls.Add(this.albumCoverImgPath);
            this.Controls.Add(this.albumName);
            this.Controls.Add(this.aName);
            this.Controls.Add(this.bandPhoto);
            this.Controls.Add(this.bandImagePath);
            this.Controls.Add(this.bandName);
            this.Controls.Add(this.bname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddBand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Band";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bname;
        private System.Windows.Forms.Label bandName;
        private System.Windows.Forms.TextBox bandImagePath;
        private System.Windows.Forms.Label bandPhoto;
        private System.Windows.Forms.Label albumName;
        private System.Windows.Forms.TextBox aName;
        private System.Windows.Forms.Label albumCoverPath;
        private System.Windows.Forms.TextBox albumCoverImgPath;
        private System.Windows.Forms.Button btnBandPhoto;
        private System.Windows.Forms.Button btnAlbumPhoto;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cnlBtn;
    }
}