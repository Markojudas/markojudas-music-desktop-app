namespace markojudas_music
{
    partial class NewAlbum
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAlbum));
            this.albumName = new System.Windows.Forms.Label();
            this.albumCoverPath = new System.Windows.Forms.Label();
            this.aName = new System.Windows.Forms.TextBox();
            this.photoPath = new System.Windows.Forms.TextBox();
            this.addImage = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.cnlBtn = new System.Windows.Forms.Button();
            this.bandName = new System.Windows.Forms.Label();
            this.bName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // albumName
            // 
            this.albumName.AutoSize = true;
            this.albumName.Location = new System.Drawing.Point(27, 76);
            this.albumName.Name = "albumName";
            this.albumName.Size = new System.Drawing.Size(62, 13);
            this.albumName.TabIndex = 0;
            this.albumName.Text = "Album Title:";
            // 
            // albumCoverPath
            // 
            this.albumCoverPath.AutoSize = true;
            this.albumCoverPath.Location = new System.Drawing.Point(19, 118);
            this.albumCoverPath.Name = "albumCoverPath";
            this.albumCoverPath.Size = new System.Drawing.Size(70, 13);
            this.albumCoverPath.TabIndex = 1;
            this.albumCoverPath.Text = "Album Cover:";
            // 
            // aName
            // 
            this.aName.Location = new System.Drawing.Point(96, 73);
            this.aName.Name = "aName";
            this.aName.Size = new System.Drawing.Size(277, 20);
            this.aName.TabIndex = 2;
            // 
            // photoPath
            // 
            this.photoPath.Location = new System.Drawing.Point(96, 115);
            this.photoPath.Name = "photoPath";
            this.photoPath.ReadOnly = true;
            this.photoPath.Size = new System.Drawing.Size(156, 20);
            this.photoPath.TabIndex = 3;
            // 
            // addImage
            // 
            this.addImage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.addImage.Location = new System.Drawing.Point(258, 113);
            this.addImage.Name = "addImage";
            this.addImage.Size = new System.Drawing.Size(77, 23);
            this.addImage.TabIndex = 4;
            this.addImage.Text = "Add Image";
            this.addImage.UseVisualStyleBackColor = false;
            this.addImage.Click += new System.EventHandler(this.addImage_Click);
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBtn.Location = new System.Drawing.Point(96, 164);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 5;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cnlBtn
            // 
            this.cnlBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cnlBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cnlBtn.Location = new System.Drawing.Point(258, 164);
            this.cnlBtn.Name = "cnlBtn";
            this.cnlBtn.Size = new System.Drawing.Size(75, 23);
            this.cnlBtn.TabIndex = 6;
            this.cnlBtn.Text = "Cancel";
            this.cnlBtn.UseVisualStyleBackColor = false;
            // 
            // bandName
            // 
            this.bandName.AutoSize = true;
            this.bandName.Location = new System.Drawing.Point(27, 32);
            this.bandName.Name = "bandName";
            this.bandName.Size = new System.Drawing.Size(66, 13);
            this.bandName.TabIndex = 7;
            this.bandName.Text = "Band Name:";
            // 
            // bName
            // 
            this.bName.Location = new System.Drawing.Point(96, 29);
            this.bName.Name = "bName";
            this.bName.Size = new System.Drawing.Size(277, 20);
            this.bName.TabIndex = 8;
            // 
            // NewAlbum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.CancelButton = this.cnlBtn;
            this.ClientSize = new System.Drawing.Size(420, 208);
            this.Controls.Add(this.bName);
            this.Controls.Add(this.bandName);
            this.Controls.Add(this.cnlBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.addImage);
            this.Controls.Add(this.photoPath);
            this.Controls.Add(this.aName);
            this.Controls.Add(this.albumCoverPath);
            this.Controls.Add(this.albumName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewAlbum";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Album";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label albumName;
        private System.Windows.Forms.Label albumCoverPath;
        private System.Windows.Forms.TextBox aName;
        private System.Windows.Forms.TextBox photoPath;
        private System.Windows.Forms.Button addImage;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cnlBtn;
        private System.Windows.Forms.Label bandName;
        private System.Windows.Forms.TextBox bName;
    }
}