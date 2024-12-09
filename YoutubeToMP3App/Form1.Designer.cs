
namespace YoutubeToMP3App
{
    partial class Form1
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
            this.buttonFindResults = new System.Windows.Forms.Button();
            this.textBoxURLOrSongName = new System.Windows.Forms.TextBox();
            this.labelUrl = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxVideo = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonFindResults
            // 
            this.buttonFindResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindResults.Location = new System.Drawing.Point(779, 49);
            this.buttonFindResults.Name = "buttonFindResults";
            this.buttonFindResults.Size = new System.Drawing.Size(93, 23);
            this.buttonFindResults.TabIndex = 0;
            this.buttonFindResults.Text = "Find Videos";
            this.buttonFindResults.UseVisualStyleBackColor = true;
            this.buttonFindResults.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // textBoxURLOrSongName
            // 
            this.textBoxURLOrSongName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURLOrSongName.Location = new System.Drawing.Point(22, 52);
            this.textBoxURLOrSongName.Name = "textBoxURLOrSongName";
            this.textBoxURLOrSongName.Size = new System.Drawing.Size(740, 20);
            this.textBoxURLOrSongName.TabIndex = 1;
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(12, 36);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(188, 13);
            this.labelUrl.TabIndex = 2;
            this.labelUrl.Text = "Type in URL or song name to convert:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 110);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(884, 359);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // checkBoxVideo
            // 
            this.checkBoxVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxVideo.AutoSize = true;
            this.checkBoxVideo.Location = new System.Drawing.Point(768, 87);
            this.checkBoxVideo.Name = "checkBoxVideo";
            this.checkBoxVideo.Size = new System.Drawing.Size(104, 17);
            this.checkBoxVideo.TabIndex = 5;
            this.checkBoxVideo.Text = "Download Video";
            this.checkBoxVideo.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 469);
            this.Controls.Add(this.checkBoxVideo);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.textBoxURLOrSongName);
            this.Controls.Add(this.buttonFindResults);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube To MP3 Application";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFindResults;
        private System.Windows.Forms.TextBox textBoxURLOrSongName;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.CheckBox checkBoxVideo;
    }
}

