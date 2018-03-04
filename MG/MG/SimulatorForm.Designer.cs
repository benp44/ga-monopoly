namespace MG
{
    partial class SimulatorForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxPopulation = new System.Windows.Forms.TextBox();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.textBoxCumulative = new System.Windows.Forms.TextBox();
            this.textBoxWorkersRunning = new System.Windows.Forms.TextBox();
            this.textBoxWorkersIdle = new System.Windows.Forms.TextBox();
            this.listViewData = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnImage = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textBoxGamesPlayed = new System.Windows.Forms.TextBox();
            this.textBoxRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAvGeneration = new System.Windows.Forms.TextBox();
            this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
            this.textBoxTimeRunning = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(229, 34);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 13);
            label1.TabIndex = 1;
            label1.Text = "Population";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(229, 60);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 13);
            label2.TabIndex = 3;
            label2.Text = "Cumulative";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(437, 60);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(90, 13);
            label3.TabIndex = 7;
            label3.Text = "Workers Running";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(437, 34);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(67, 13);
            label4.TabIndex = 5;
            label4.Text = "Workers Idle";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(13, 34);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(75, 13);
            label5.TabIndex = 11;
            label5.Text = "Games Played";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(229, 86);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(102, 13);
            label7.TabIndex = 15;
            label7.Text = "Average Generation";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(13, 86);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(73, 13);
            label8.TabIndex = 18;
            label8.Text = "Time Running";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.runToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1218, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemReset,
            this.toolStripSeparator1,
            this.MenuItemExit});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // MenuItemReset
            // 
            this.MenuItemReset.Name = "MenuItemReset";
            this.MenuItemReset.Size = new System.Drawing.Size(102, 22);
            this.MenuItemReset.Text = "Reset";
            this.MenuItemReset.Click += new System.EventHandler(this.Menu_Reset);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(99, 6);
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.Size = new System.Drawing.Size(102, 22);
            this.MenuItemExit.Text = "Exit";
            this.MenuItemExit.Click += new System.EventHandler(this.Menu_Exit);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemStart,
            this.MenuItemStop});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // MenuItemStart
            // 
            this.MenuItemStart.Name = "MenuItemStart";
            this.MenuItemStart.Size = new System.Drawing.Size(98, 22);
            this.MenuItemStart.Text = "Start";
            this.MenuItemStart.Click += new System.EventHandler(this.Menu_Start);
            // 
            // MenuItemStop
            // 
            this.MenuItemStop.Name = "MenuItemStop";
            this.MenuItemStop.Size = new System.Drawing.Size(98, 22);
            this.MenuItemStop.Text = "Stop";
            this.MenuItemStop.Click += new System.EventHandler(this.Menu_Stop);
            // 
            // textBoxPopulation
            // 
            this.textBoxPopulation.Location = new System.Drawing.Point(342, 31);
            this.textBoxPopulation.Name = "textBoxPopulation";
            this.textBoxPopulation.ReadOnly = true;
            this.textBoxPopulation.Size = new System.Drawing.Size(77, 20);
            this.textBoxPopulation.TabIndex = 2;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 1000;
            // 
            // textBoxCumulative
            // 
            this.textBoxCumulative.Location = new System.Drawing.Point(342, 57);
            this.textBoxCumulative.Name = "textBoxCumulative";
            this.textBoxCumulative.ReadOnly = true;
            this.textBoxCumulative.Size = new System.Drawing.Size(77, 20);
            this.textBoxCumulative.TabIndex = 4;
            // 
            // textBoxWorkersRunning
            // 
            this.textBoxWorkersRunning.Location = new System.Drawing.Point(550, 57);
            this.textBoxWorkersRunning.Name = "textBoxWorkersRunning";
            this.textBoxWorkersRunning.ReadOnly = true;
            this.textBoxWorkersRunning.Size = new System.Drawing.Size(77, 20);
            this.textBoxWorkersRunning.TabIndex = 8;
            // 
            // textBoxWorkersIdle
            // 
            this.textBoxWorkersIdle.Location = new System.Drawing.Point(550, 31);
            this.textBoxWorkersIdle.Name = "textBoxWorkersIdle";
            this.textBoxWorkersIdle.ReadOnly = true;
            this.textBoxWorkersIdle.Size = new System.Drawing.Size(77, 20);
            this.textBoxWorkersIdle.TabIndex = 6;
            // 
            // listViewData
            // 
            this.listViewData.AllColumns.Add(this.olvColumn3);
            this.listViewData.AllColumns.Add(this.olvColumnImage);
            this.listViewData.AllColumns.Add(this.olvColumn4);
            this.listViewData.AllColumns.Add(this.olvColumn1);
            this.listViewData.AllColumns.Add(this.olvColumn2);
            this.listViewData.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3,
            this.olvColumnImage,
            this.olvColumn4,
            this.olvColumn1,
            this.olvColumn2});
            this.listViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewData.FullRowSelect = true;
            this.listViewData.GridLines = true;
            this.listViewData.Location = new System.Drawing.Point(0, 0);
            this.listViewData.Name = "listViewData";
            this.listViewData.ShowGroups = false;
            this.listViewData.ShowImagesOnSubItems = true;
            this.listViewData.Size = new System.Drawing.Size(321, 316);
            this.listViewData.TabIndex = 9;
            this.listViewData.UseAlternatingBackColors = true;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Order";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Order";
            this.olvColumn3.Width = 40;
            // 
            // olvColumnImage
            // 
            this.olvColumnImage.IsEditable = false;
            this.olvColumnImage.Text = "";
            this.olvColumnImage.Width = 20;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "GeneType";
            this.olvColumn4.Text = "Type";
            this.olvColumn4.Width = 70;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "AssetName";
            this.olvColumn1.AspectToStringFormat = "";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Asset";
            this.olvColumn1.Width = 150;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "AveragePreference";
            this.olvColumn2.AspectToStringFormat = "{0:N3}";
            this.olvColumn2.AutoCompleteEditor = false;
            this.olvColumn2.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Average P";
            this.olvColumn2.Width = 80;
            // 
            // textBoxGamesPlayed
            // 
            this.textBoxGamesPlayed.Location = new System.Drawing.Point(126, 31);
            this.textBoxGamesPlayed.Name = "textBoxGamesPlayed";
            this.textBoxGamesPlayed.ReadOnly = true;
            this.textBoxGamesPlayed.Size = new System.Drawing.Size(77, 20);
            this.textBoxGamesPlayed.TabIndex = 12;
            // 
            // textBoxRate
            // 
            this.textBoxRate.Location = new System.Drawing.Point(126, 57);
            this.textBoxRate.Name = "textBoxRate";
            this.textBoxRate.ReadOnly = true;
            this.textBoxRate.Size = new System.Drawing.Size(77, 20);
            this.textBoxRate.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Rate";
            // 
            // textBoxAvGeneration
            // 
            this.textBoxAvGeneration.Location = new System.Drawing.Point(342, 83);
            this.textBoxAvGeneration.Name = "textBoxAvGeneration";
            this.textBoxAvGeneration.ReadOnly = true;
            this.textBoxAvGeneration.Size = new System.Drawing.Size(77, 20);
            this.textBoxAvGeneration.TabIndex = 16;
            // 
            // pictureBoxGraph
            // 
            this.pictureBoxGraph.BackColor = System.Drawing.Color.White;
            this.pictureBoxGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxGraph.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxGraph.Name = "pictureBoxGraph";
            this.pictureBoxGraph.Size = new System.Drawing.Size(869, 316);
            this.pictureBoxGraph.TabIndex = 17;
            this.pictureBoxGraph.TabStop = false;
            this.pictureBoxGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGraph_Paint);
            // 
            // textBoxTimeRunning
            // 
            this.textBoxTimeRunning.Location = new System.Drawing.Point(126, 83);
            this.textBoxTimeRunning.Name = "textBoxTimeRunning";
            this.textBoxTimeRunning.ReadOnly = true;
            this.textBoxTimeRunning.Size = new System.Drawing.Size(77, 20);
            this.textBoxTimeRunning.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelFilters);
            this.groupBox1.Location = new System.Drawing.Point(642, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 63);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // panelFilters
            // 
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilters.Location = new System.Drawing.Point(3, 16);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(468, 44);
            this.panelFilters.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 109);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxGraph);
            this.splitContainer1.Size = new System.Drawing.Size(1194, 316);
            this.splitContainer1.SplitterDistance = 321;
            this.splitContainer1.TabIndex = 21;
            // 
            // SimulatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 437);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxTimeRunning);
            this.Controls.Add(label8);
            this.Controls.Add(this.textBoxAvGeneration);
            this.Controls.Add(label7);
            this.Controls.Add(this.textBoxRate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxGamesPlayed);
            this.Controls.Add(label5);
            this.Controls.Add(this.textBoxWorkersRunning);
            this.Controls.Add(label3);
            this.Controls.Add(this.textBoxWorkersIdle);
            this.Controls.Add(label4);
            this.Controls.Add(this.textBoxCumulative);
            this.Controls.Add(label2);
            this.Controls.Add(this.textBoxPopulation);
            this.Controls.Add(label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimulatorForm";
            this.Text = "GeneticsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneticsForm_FormClosing);
            this.Load += new System.EventHandler(this.GeneticsForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
        private System.Windows.Forms.TextBox textBoxPopulation;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStart;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStop;
        private System.Windows.Forms.TextBox textBoxCumulative;
        private System.Windows.Forms.TextBox textBoxWorkersRunning;
        private System.Windows.Forms.TextBox textBoxWorkersIdle;
        private BrightIdeasSoftware.ObjectListView listViewData;
        private System.Windows.Forms.TextBox textBoxGamesPlayed;
        private System.Windows.Forms.TextBox textBoxRate;
        private System.Windows.Forms.Label label6;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumnImage;
        private System.Windows.Forms.TextBox textBoxAvGeneration;
        private System.Windows.Forms.PictureBox pictureBoxGraph;
        private System.Windows.Forms.TextBox textBoxTimeRunning;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel panelFilters;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}