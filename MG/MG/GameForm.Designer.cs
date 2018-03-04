namespace MG
{
    partial class GameForm
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
            this.listViewPlayers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playPlayerTurnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playSingleTurnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.play10TurnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playUntilEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.play10GamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewPlayers
            // 
            this.listViewPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPlayers.FullRowSelect = true;
            this.listViewPlayers.Location = new System.Drawing.Point(10, 24);
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.Size = new System.Drawing.Size(1142, 284);
            this.listViewPlayers.TabIndex = 0;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewPlayers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Player";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Square";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Money";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Status";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "LastTurn";
            this.columnHeader5.Width = 237;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Assets";
            this.columnHeader6.Width = 560;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.turnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(10, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1142, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.play10GamesToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.gameToolStripMenuItem.Text = "&System";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "&New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.NewGame);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // turnToolStripMenuItem
            // 
            this.turnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playPlayerTurnToolStripMenuItem,
            this.playSingleTurnToolStripMenuItem,
            this.play10TurnsToolStripMenuItem,
            this.playUntilEndToolStripMenuItem});
            this.turnToolStripMenuItem.Name = "turnToolStripMenuItem";
            this.turnToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.turnToolStripMenuItem.Text = "&Current Game";
            // 
            // playPlayerTurnToolStripMenuItem
            // 
            this.playPlayerTurnToolStripMenuItem.Name = "playPlayerTurnToolStripMenuItem";
            this.playPlayerTurnToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.playPlayerTurnToolStripMenuItem.Text = "Play &Player Turn";
            this.playPlayerTurnToolStripMenuItem.Click += new System.EventHandler(this.PlayPlayerTurn);
            // 
            // playSingleTurnToolStripMenuItem
            // 
            this.playSingleTurnToolStripMenuItem.Name = "playSingleTurnToolStripMenuItem";
            this.playSingleTurnToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.playSingleTurnToolStripMenuItem.Text = "Play Full &Turn";
            this.playSingleTurnToolStripMenuItem.Click += new System.EventHandler(this.PlayAllPlayersTurn);
            // 
            // play10TurnsToolStripMenuItem
            // 
            this.play10TurnsToolStripMenuItem.Name = "play10TurnsToolStripMenuItem";
            this.play10TurnsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.play10TurnsToolStripMenuItem.Text = "Play &10 Turns";
            this.play10TurnsToolStripMenuItem.Click += new System.EventHandler(this.PlayTenTurns);
            // 
            // playUntilEndToolStripMenuItem
            // 
            this.playUntilEndToolStripMenuItem.Name = "playUntilEndToolStripMenuItem";
            this.playUntilEndToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.playUntilEndToolStripMenuItem.Text = "Play Until &End";
            this.playUntilEndToolStripMenuItem.Click += new System.EventHandler(this.PlayUntilEnd);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(10, 286);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1142, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // play10GamesToolStripMenuItem
            // 
            this.play10GamesToolStripMenuItem.Name = "play10GamesToolStripMenuItem";
            this.play10GamesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.play10GamesToolStripMenuItem.Text = "Play 10 Games";
            this.play10GamesToolStripMenuItem.Click += new System.EventHandler(this.play10GamesToolStripMenuItem_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 318);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listViewPlayers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.Text = "MG";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewPlayers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playSingleTurnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playUntilEndToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripMenuItem playPlayerTurnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem play10TurnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem play10GamesToolStripMenuItem;
    }
}

