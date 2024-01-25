namespace recheck
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openFolderBtn = new Button();
            pathBox = new TextBox();
            runrbtn = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // openFolderBtn
            // 
            openFolderBtn.Location = new Point(12, 26);
            openFolderBtn.Name = "openFolderBtn";
            openFolderBtn.Size = new Size(96, 33);
            openFolderBtn.TabIndex = 0;
            openFolderBtn.Text = "Open Folder";
            openFolderBtn.UseVisualStyleBackColor = true;
            openFolderBtn.Click += openFolderBtn_Click;
            // 
            // pathBox
            // 
            pathBox.Location = new Point(12, 77);
            pathBox.Multiline = true;
            pathBox.Name = "pathBox";
            pathBox.Size = new Size(410, 23);
            pathBox.TabIndex = 1;
            // 
            // runrbtn
            // 
            runrbtn.Location = new Point(326, 115);
            runrbtn.Name = "runrbtn";
            runrbtn.Size = new Size(96, 32);
            runrbtn.TabIndex = 2;
            runrbtn.Text = "RUN";
            runrbtn.UseVisualStyleBackColor = true;
            runrbtn.Click += runrbtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.FromArgb(66, 69, 245);
            label1.Location = new Point(28, 167);
            label1.Name = "label1";
            label1.Size = new Size(383, 15);
            label1.TabIndex = 4;
            label1.Text = "Who needs overpriced software when you can afford a vacation instead";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 30, 54);
            ClientSize = new Size(449, 191);
            Controls.Add(label1);
            Controls.Add(runrbtn);
            Controls.Add(pathBox);
            Controls.Add(openFolderBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Recheck";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button openFolderBtn;
        private TextBox pathBox;
        private Button runrbtn;
        private Label label1;
    }
}
