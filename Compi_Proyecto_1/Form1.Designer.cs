namespace Compi_Proyecto_1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saceAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadThompsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.expression_regular = new System.Windows.Forms.ComboBox();
            this.type_image = new System.Windows.Forms.ComboBox();
            this.button_graph = new System.Windows.Forms.Button();
            this.panel_image = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.console = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.analizeLexemesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel_image.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menu";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saceAsToolStripMenuItem,
            this.closeTabToolStripMenuItem,
            this.newTabToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.optionsToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saceAsToolStripMenuItem
            // 
            this.saceAsToolStripMenuItem.Name = "saceAsToolStripMenuItem";
            this.saceAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saceAsToolStripMenuItem.Text = "Sace As";
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.closeTabToolStripMenuItem.Text = "Close Tab";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // newTabToolStripMenuItem
            // 
            this.newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
            this.newTabToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newTabToolStripMenuItem.Text = "New Tab";
            this.newTabToolStripMenuItem.Click += new System.EventHandler(this.newTabToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadThompsonToolStripMenuItem,
            this.saveTokensToolStripMenuItem,
            this.saveErrorsToolStripMenuItem,
            this.analizeLexemesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // loadThompsonToolStripMenuItem
            // 
            this.loadThompsonToolStripMenuItem.Name = "loadThompsonToolStripMenuItem";
            this.loadThompsonToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.loadThompsonToolStripMenuItem.Text = "Load Thompson";
            this.loadThompsonToolStripMenuItem.Click += new System.EventHandler(this.loadThompsonToolStripMenuItem_Click);
            // 
            // saveTokensToolStripMenuItem
            // 
            this.saveTokensToolStripMenuItem.Name = "saveTokensToolStripMenuItem";
            this.saveTokensToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveTokensToolStripMenuItem.Text = "Save Tokens";
            this.saveTokensToolStripMenuItem.Click += new System.EventHandler(this.saveTokensToolStripMenuItem_Click);
            // 
            // saveErrorsToolStripMenuItem
            // 
            this.saveErrorsToolStripMenuItem.Name = "saveErrorsToolStripMenuItem";
            this.saveErrorsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveErrorsToolStripMenuItem.Text = "Save Errors";
            this.saveErrorsToolStripMenuItem.Click += new System.EventHandler(this.saveErrorsToolStripMenuItem_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFile";
            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(374, 436);
            this.tabControl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select an expression regular";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(554, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select an image type";
            // 
            // expression_regular
            // 
            this.expression_regular.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.expression_regular.FormattingEnabled = true;
            this.expression_regular.Location = new System.Drawing.Point(395, 43);
            this.expression_regular.Name = "expression_regular";
            this.expression_regular.Size = new System.Drawing.Size(137, 21);
            this.expression_regular.TabIndex = 4;
            // 
            // type_image
            // 
            this.type_image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type_image.FormattingEnabled = true;
            this.type_image.Location = new System.Drawing.Point(557, 42);
            this.type_image.Name = "type_image";
            this.type_image.Size = new System.Drawing.Size(103, 21);
            this.type_image.TabIndex = 5;
            // 
            // button_graph
            // 
            this.button_graph.Location = new System.Drawing.Point(678, 42);
            this.button_graph.Name = "button_graph";
            this.button_graph.Size = new System.Drawing.Size(75, 23);
            this.button_graph.TabIndex = 6;
            this.button_graph.Text = "Graph";
            this.button_graph.UseVisualStyleBackColor = true;
            this.button_graph.Click += new System.EventHandler(this.button_graph_Click);
            // 
            // panel_image
            // 
            this.panel_image.AutoScroll = true;
            this.panel_image.Controls.Add(this.pictureBox);
            this.panel_image.Location = new System.Drawing.Point(395, 71);
            this.panel_image.Name = "panel_image";
            this.panel_image.Size = new System.Drawing.Size(477, 392);
            this.panel_image.TabIndex = 7;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(477, 392);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(13, 484);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.console.Size = new System.Drawing.Size(859, 65);
            this.console.TabIndex = 8;
            this.console.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 466);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Console";
            // 
            // analizeLexemesToolStripMenuItem
            // 
            this.analizeLexemesToolStripMenuItem.Name = "analizeLexemesToolStripMenuItem";
            this.analizeLexemesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.analizeLexemesToolStripMenuItem.Text = "Analize Lexemes";
            this.analizeLexemesToolStripMenuItem.Click += new System.EventHandler(this.analizeLexemesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.console);
            this.Controls.Add(this.panel_image);
            this.Controls.Add(this.button_graph);
            this.Controls.Add(this.type_image);
            this.Controls.Add(this.expression_regular);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Proyect #1 Compiladores 1 2020";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_image.ResumeLayout(false);
            this.panel_image.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saceAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadThompsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveErrorsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox expression_regular;
        private System.Windows.Forms.ComboBox type_image;
        private System.Windows.Forms.Button button_graph;
        private System.Windows.Forms.Panel panel_image;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox console;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem analizeLexemesToolStripMenuItem;
    }
}

