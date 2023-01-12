namespace markojudas_music
{
    partial class MainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.addBand = new System.Windows.Forms.Button();
            this.updateBand = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addBand
            // 
            this.addBand.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.addBand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addBand.Location = new System.Drawing.Point(143, 19);
            this.addBand.Margin = new System.Windows.Forms.Padding(5);
            this.addBand.Name = "addBand";
            this.addBand.Size = new System.Drawing.Size(197, 81);
            this.addBand.TabIndex = 0;
            this.addBand.Text = "New Band/Album";
            this.addBand.UseVisualStyleBackColor = false;
            this.addBand.Click += new System.EventHandler(this.addBand_Click);
            // 
            // updateBand
            // 
            this.updateBand.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.updateBand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateBand.Location = new System.Drawing.Point(143, 110);
            this.updateBand.Margin = new System.Windows.Forms.Padding(5);
            this.updateBand.Name = "updateBand";
            this.updateBand.Size = new System.Drawing.Size(197, 82);
            this.updateBand.TabIndex = 1;
            this.updateBand.Text = "New Album";
            this.updateBand.UseVisualStyleBackColor = false;
            this.updateBand.Click += new System.EventHandler(this.updateBand_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(175, 247);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(125, 37);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(477, 304);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.updateBand);
            this.Controls.Add(this.addBand);
            this.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Markojudas\' Music";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addBand;
        private System.Windows.Forms.Button updateBand;
        private System.Windows.Forms.Button closeBtn;
    }
}

