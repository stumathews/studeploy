namespace StuDeploy
{
    partial class ComputerActionsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerActionsForm));
            this.computerActionsImages = new System.Windows.Forms.ImageList(this.components);
            this.theTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // computerActionsImages
            // 
            this.computerActionsImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("computerActionsImages.ImageStream")));
            this.computerActionsImages.TransparentColor = System.Drawing.Color.Transparent;
            this.computerActionsImages.Images.SetKeyName(0, "action");
            // 
            // theTreeView
            // 
            this.theTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theTreeView.ImageIndex = 0;
            this.theTreeView.ImageList = this.computerActionsImages;
            this.theTreeView.Location = new System.Drawing.Point(0, 0);
            this.theTreeView.Name = "theTreeView";
            this.theTreeView.SelectedImageIndex = 0;
            this.theTreeView.Size = new System.Drawing.Size(292, 266);
            this.theTreeView.TabIndex = 0;
            // 
            // ComputerActionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.theTreeView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ComputerActionsForm";
            this.Text = "ComputerActionsForm";
            this.Load += new System.EventHandler(this.ComputerActionsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList computerActionsImages;
        private System.Windows.Forms.TreeView theTreeView;
    }
}