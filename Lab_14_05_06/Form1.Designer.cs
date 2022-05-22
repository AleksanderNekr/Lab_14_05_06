namespace Lab_14_05_06
{
    internal sealed partial class IsCorrectWorkshopNumber
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
            this.menuStrip1                              = new System.Windows.Forms.MenuStrip();
            this.CreateMainCollectionToolStripMenuItem   = new System.Windows.Forms.ToolStripMenuItem();
            this.WorkersNamesOfWorkshopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListBox                                 = new System.Windows.Forms.ListBox();
            this.RequestListBox                          = new System.Windows.Forms.ListBox();
            this.label1                                  = new System.Windows.Forms.Label();
            this.label2                                  = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.CreateMainCollectionToolStripMenuItem, this.WorkersNamesOfWorkshopToolStripMenuItem });
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
            // WorkersNamesOfWorkshopToolStripMenuItem
            // 
            this.WorkersNamesOfWorkshopToolStripMenuItem.Name  =  "WorkersNamesOfWorkshopToolStripMenuItem";
            this.WorkersNamesOfWorkshopToolStripMenuItem.Size  =  new System.Drawing.Size(193, 20);
            this.WorkersNamesOfWorkshopToolStripMenuItem.Text  =  "Имена рабочих заданного цеха";
            this.WorkersNamesOfWorkshopToolStripMenuItem.Click += new System.EventHandler(this.WorkersNamesOfWorkshopToolStripMenuItem_Click);
            // 
            // ListBox
            // 
            this.ListBox.FormattingEnabled = true;
            this.ListBox.Location          = new System.Drawing.Point(12, 53);
            this.ListBox.Name              = "ListBox";
            this.ListBox.Size              = new System.Drawing.Size(450, 381);
            this.ListBox.TabIndex          = 1;
            // 
            // RequestListBox
            // 
            this.RequestListBox.FormattingEnabled = true;
            this.RequestListBox.Location          = new System.Drawing.Point(482, 53);
            this.RequestListBox.Name              = "RequestListBox";
            this.RequestListBox.Size              = new System.Drawing.Size(306, 381);
            this.RequestListBox.TabIndex          = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 33);
            this.label1.Name     = "label1";
            this.label1.Size     = new System.Drawing.Size(205, 20);
            this.label1.TabIndex = 3;
            this.label1.Text     = "Коллекция";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(482, 33);
            this.label2.Name     = "label2";
            this.label2.Size     = new System.Drawing.Size(205, 20);
            this.label2.TabIndex = 4;
            this.label2.Text     = "Результат запроса";
            // 
            // IsCorrectWorkshopNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RequestListBox);
            this.Controls.Add(this.ListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name          = "IsCorrectWorkshopNumber";
            this.Text          = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.ListBox RequestListBox;

        private System.Windows.Forms.ToolStripMenuItem WorkersNamesOfWorkshopToolStripMenuItem;

        private System.Windows.Forms.ListBox ListBox;

        private System.Windows.Forms.MenuStrip         menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CreateMainCollectionToolStripMenuItem;

        #endregion
    }
}
