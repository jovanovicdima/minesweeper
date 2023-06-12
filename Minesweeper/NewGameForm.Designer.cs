namespace Minesweeper {
    partial class NewGameForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGameForm));
            easyButton = new Button();
            mediumButton = new Button();
            hardButton = new Button();
            impossibleButton = new Button();
            gridSizeTrackBar = new TrackBar();
            gridSizeLabel = new Label();
            numberOfMinesLabel = new Label();
            numberOfMinesTrackBar = new TrackBar();
            button1 = new Button();
            difficultyLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)gridSizeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numberOfMinesTrackBar).BeginInit();
            SuspendLayout();
            // 
            // easyButton
            // 
            easyButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            easyButton.Location = new Point(12, 12);
            easyButton.Name = "easyButton";
            easyButton.Size = new Size(200, 75);
            easyButton.TabIndex = 0;
            easyButton.Text = "Preset: Easy";
            easyButton.UseVisualStyleBackColor = true;
            easyButton.Click += easyButton_Click;
            // 
            // mediumButton
            // 
            mediumButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            mediumButton.Location = new Point(12, 93);
            mediumButton.Name = "mediumButton";
            mediumButton.Size = new Size(200, 75);
            mediumButton.TabIndex = 1;
            mediumButton.Text = "Preset: Medium";
            mediumButton.UseVisualStyleBackColor = true;
            mediumButton.Click += mediumButton_Click;
            // 
            // hardButton
            // 
            hardButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            hardButton.Location = new Point(12, 174);
            hardButton.Name = "hardButton";
            hardButton.Size = new Size(200, 75);
            hardButton.TabIndex = 2;
            hardButton.Text = "Preset: Hard";
            hardButton.UseVisualStyleBackColor = true;
            hardButton.Click += hardButton_Click;
            // 
            // impossibleButton
            // 
            impossibleButton.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            impossibleButton.Location = new Point(12, 255);
            impossibleButton.Name = "impossibleButton";
            impossibleButton.Size = new Size(200, 75);
            impossibleButton.TabIndex = 3;
            impossibleButton.Text = "Preset: Impossible";
            impossibleButton.UseVisualStyleBackColor = true;
            impossibleButton.Click += impossibleButton_Click;
            // 
            // gridSizeTrackBar
            // 
            gridSizeTrackBar.Location = new Point(218, 129);
            gridSizeTrackBar.Maximum = 30;
            gridSizeTrackBar.Minimum = 9;
            gridSizeTrackBar.Name = "gridSizeTrackBar";
            gridSizeTrackBar.Size = new Size(465, 56);
            gridSizeTrackBar.TabIndex = 4;
            gridSizeTrackBar.Value = 9;
            gridSizeTrackBar.Scroll += gridSizeTrackBar_Scroll;
            // 
            // gridSizeLabel
            // 
            gridSizeLabel.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            gridSizeLabel.Location = new Point(218, 88);
            gridSizeLabel.Name = "gridSizeLabel";
            gridSizeLabel.Size = new Size(454, 35);
            gridSizeLabel.TabIndex = 5;
            gridSizeLabel.Text = "Grid Size: 9";
            gridSizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numberOfMinesLabel
            // 
            numberOfMinesLabel.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            numberOfMinesLabel.Location = new Point(218, 169);
            numberOfMinesLabel.Name = "numberOfMinesLabel";
            numberOfMinesLabel.Size = new Size(454, 35);
            numberOfMinesLabel.TabIndex = 6;
            numberOfMinesLabel.Text = "Number Of Mines: 10";
            numberOfMinesLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numberOfMinesTrackBar
            // 
            numberOfMinesTrackBar.Location = new Point(218, 210);
            numberOfMinesTrackBar.Maximum = 30;
            numberOfMinesTrackBar.Minimum = 10;
            numberOfMinesTrackBar.Name = "numberOfMinesTrackBar";
            numberOfMinesTrackBar.Size = new Size(465, 56);
            numberOfMinesTrackBar.TabIndex = 7;
            numberOfMinesTrackBar.Value = 10;
            numberOfMinesTrackBar.Scroll += numberOfMinesTrackBar_Scroll;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(472, 255);
            button1.Name = "button1";
            button1.Size = new Size(200, 75);
            button1.TabIndex = 8;
            button1.Text = "Start Game";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // difficultyLabel
            // 
            difficultyLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            difficultyLabel.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            difficultyLabel.Location = new Point(218, 14);
            difficultyLabel.Name = "difficultyLabel";
            difficultyLabel.Size = new Size(465, 57);
            difficultyLabel.TabIndex = 9;
            difficultyLabel.Text = "Difficulty: Easy\r\n";
            difficultyLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NewGameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(684, 342);
            Controls.Add(difficultyLabel);
            Controls.Add(button1);
            Controls.Add(numberOfMinesTrackBar);
            Controls.Add(numberOfMinesLabel);
            Controls.Add(gridSizeLabel);
            Controls.Add(gridSizeTrackBar);
            Controls.Add(impossibleButton);
            Controls.Add(hardButton);
            Controls.Add(mediumButton);
            Controls.Add(easyButton);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "NewGameForm";
            Text = "New Game";
            ((System.ComponentModel.ISupportInitialize)gridSizeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)numberOfMinesTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button easyButton;
        private Button mediumButton;
        private Button hardButton;
        private Button impossibleButton;
        private TrackBar gridSizeTrackBar;
        private Label gridSizeLabel;
        private Label numberOfMinesLabel;
        private TrackBar numberOfMinesTrackBar;
        private Button button1;
        private Label difficultyLabel;
    }
}