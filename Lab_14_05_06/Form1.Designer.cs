namespace Lab_14_05_06
{
    internal sealed partial class Form1
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
            this.menuStrip1                            = new System.Windows.Forms.MenuStrip();
            this.CreateMainCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListBox                               = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.CreateMainCollectionToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name     = "menuStrip1";
            this.menuStrip1.Size     = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text     = "menuStrip1";
            // 
            // CreateMainCollectionToolStripMenuItem
            // 
            this.CreateMainCollectionToolStripMenuItem.Name  =  "CreateMainCollectionToolStripMenuItem";
            this.CreateMainCollectionToolStripMenuItem.Size  =  new System.Drawing.Size(128, 20);
            this.CreateMainCollectionToolStripMenuItem.Text  =  "Создать коллекцию";
            this.CreateMainCollectionToolStripMenuItem.Click += new System.EventHandler(this.CreateMainCollectionToolStripMenuItem_Click);
            // 
            // ListBox
            // 
            this.ListBox.FormattingEnabled = true;
            this.ListBox.Location          = new System.Drawing.Point(12, 27);
            this.ListBox.Name              = "ListBox";
            this.ListBox.Size              = new System.Drawing.Size(776, 407);
            this.ListBox.TabIndex          = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name          = "Form1";
            this.Text          = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListBox ListBox;

        private System.Windows.Forms.MenuStrip         menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CreateMainCollectionToolStripMenuItem;

        #endregion
    }
}
