namespace Question_App
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.addNewButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.testsListBox = new System.Windows.Forms.ListBox();
            this.startTestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addNewButton
            // 
            resources.ApplyResources(this.addNewButton, "addNewButton");
            this.addNewButton.Name = "addNewButton";
            this.addNewButton.UseVisualStyleBackColor = true;
            this.addNewButton.Click += new System.EventHandler(this.AddNewButton_Click);
            // 
            // editButton
            // 
            resources.ApplyResources(this.editButton, "editButton");
            this.editButton.Name = "editButton";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // testsListBox
            // 
            resources.ApplyResources(this.testsListBox, "testsListBox");
            this.testsListBox.FormattingEnabled = true;
            this.testsListBox.Name = "testsListBox";
            this.testsListBox.SelectedIndexChanged += new System.EventHandler(this.TestsListBox_SelectedIndexChanged);
            // 
            // startTestButton
            // 
            resources.ApplyResources(this.startTestButton, "startTestButton");
            this.startTestButton.Name = "startTestButton";
            this.startTestButton.UseVisualStyleBackColor = true;
            this.startTestButton.Click += new System.EventHandler(this.StartTestButton_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.startTestButton);
            this.Controls.Add(this.testsListBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addNewButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

		}

        #endregion
        private System.Windows.Forms.Button addNewButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ListBox testsListBox;
        private System.Windows.Forms.Button startTestButton;
    }
}

