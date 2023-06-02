namespace Minesweeper {
    partial class MainForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            mineGrid = new DataGridView();
            menuStrip1 = new MenuStrip();
            gameToolStripMenuItem = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            saveGameToolStripMenuItem = new ToolStripMenuItem();
            loadGameToolStripMenuItem = new ToolStripMenuItem();
            surrenderToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)mineGrid).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // mineGrid
            // 
            mineGrid.AllowUserToAddRows = false;
            mineGrid.AllowUserToDeleteRows = false;
            mineGrid.AllowUserToResizeColumns = false;
            mineGrid.AllowUserToResizeRows = false;
            mineGrid.BorderStyle = BorderStyle.None;
            mineGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mineGrid.ColumnHeadersVisible = false;
            mineGrid.EnableHeadersVisualStyles = false;
            mineGrid.Location = new Point(0, 31);
            mineGrid.Margin = new Padding(0);
            mineGrid.Name = "mineGrid";
            mineGrid.ReadOnly = true;
            mineGrid.RowHeadersVisible = false;
            mineGrid.RowHeadersWidth = 51;
            mineGrid.RowTemplate.Height = 25;
            mineGrid.ScrollBars = ScrollBars.None;
            mineGrid.Size = new Size(901, 561);
            mineGrid.TabIndex = 0;
            mineGrid.CellContentClick += mineGrid_CellContentClick;
            mineGrid.MouseClick += mineGrid_MouseClick;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { gameToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(914, 30);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            gameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, saveGameToolStripMenuItem, loadGameToolStripMenuItem, surrenderToolStripMenuItem });
            gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            gameToolStripMenuItem.Size = new Size(62, 24);
            gameToolStripMenuItem.Text = "Game";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.Size = new Size(168, 26);
            newGameToolStripMenuItem.Text = "New Game";
            newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            // 
            // saveGameToolStripMenuItem
            // 
            saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            saveGameToolStripMenuItem.Size = new Size(168, 26);
            saveGameToolStripMenuItem.Text = "Save Game";
            saveGameToolStripMenuItem.Click += saveGameToolStripMenuItem_Click;
            // 
            // loadGameToolStripMenuItem
            // 
            loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            loadGameToolStripMenuItem.Size = new Size(168, 26);
            loadGameToolStripMenuItem.Text = "Load Game";
            loadGameToolStripMenuItem.Click += loadGameToolStripMenuItem_Click;
            // 
            // surrenderToolStripMenuItem
            // 
            surrenderToolStripMenuItem.Name = "surrenderToolStripMenuItem";
            surrenderToolStripMenuItem.Size = new Size(168, 26);
            surrenderToolStripMenuItem.Text = "Surrender";
            surrenderToolStripMenuItem.Click += surrenderToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(914, 724);
            Controls.Add(mineGrid);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)mineGrid).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView mineGrid;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem saveGameToolStripMenuItem;
        private ToolStripMenuItem loadGameToolStripMenuItem;
        private ToolStripMenuItem surrenderToolStripMenuItem;
    }
}