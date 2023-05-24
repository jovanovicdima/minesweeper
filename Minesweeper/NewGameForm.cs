using MineGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class NewGameForm : Form
    {
        static DataGridView mineGrid;
        public NewGameForm(DataGridView grid)
        {
            InitializeComponent();
            mineGrid = grid;
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(impossibleButton, "literally imposible");
            numberOfMinesTrackBar.Maximum = (int)((gridSizeTrackBar.Value * gridSizeTrackBar.Value) * 0.4f);
        }

        private void gridSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            difficultyLabel.Text = "Difficulty: Custom";
            numberOfMinesTrackBar.Maximum = (int)((gridSizeTrackBar.Value * gridSizeTrackBar.Value) * 0.4f);
            numberOfMinesLabel.Text = $"Number Of Mines: {numberOfMinesTrackBar.Value}";
            gridSizeLabel.Text = $"Grid Size: {gridSizeTrackBar.Value}";
        }

        private void numberOfMinesTrackBar_Scroll(object sender, EventArgs e)
        {
            if(difficultyLabel.Text == "Difficulty: Impossible")
            {
                gridSizeTrackBar.Value = 30;
                gridSizeLabel.Text = $"Grid Size: {gridSizeTrackBar.Value}";
                numberOfMinesTrackBar.Maximum = (int)((gridSizeTrackBar.Value * gridSizeTrackBar.Value) * 0.4f);
            }
            numberOfMinesLabel.Text = $"Number Of Mines: {numberOfMinesTrackBar.Value}";
            difficultyLabel.Text = "Difficulty: Custom";
            
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            difficultyLabel.Text = "Difficulty: Easy";
            gridSizeTrackBar.Value = 9;
            gridSizeLabel.Text = $"Grid Size: {9}";
            numberOfMinesTrackBar.Maximum = (int)((gridSizeTrackBar.Value * gridSizeTrackBar.Value) * 0.4f);
            numberOfMinesTrackBar.Value = 10;
            numberOfMinesLabel.Text = $"Number Of Mines: {10}";
        }

        private void mediumButton_Click(object sender, EventArgs e)
        {
            difficultyLabel.Text = "Difficulty: Medium";
            gridSizeTrackBar.Value = 15;
            gridSizeLabel.Text = $"Grid Size: {15}";
            numberOfMinesTrackBar.Maximum = (int)((gridSizeTrackBar.Value * gridSizeTrackBar.Value) * 0.4f);
            numberOfMinesTrackBar.Value = 40;
            numberOfMinesLabel.Text = $"Number Of Mines: {40}";
        }

        private void hardButton_Click(object sender, EventArgs e)
        {
            difficultyLabel.Text = "Difficulty: Hard";
            gridSizeTrackBar.Value = 20;
            gridSizeLabel.Text = $"Grid Size: {20}";
            numberOfMinesTrackBar.Maximum = (int)((gridSizeTrackBar.Value * gridSizeTrackBar.Value) * 0.4f);
            numberOfMinesTrackBar.Value = 55;
            numberOfMinesLabel.Text = $"Number Of Mines: {55}";
        }


        private void impossibleButton_Click(object sender, EventArgs e)
        {
            difficultyLabel.Text = "Difficulty: Impossible";
            numberOfMinesTrackBar.Maximum = 900;
            gridSizeTrackBar.Value = 30;
            gridSizeLabel.Text = $"Grid Size: ???";
            numberOfMinesTrackBar.Value = 900;
            numberOfMinesLabel.Text = $"Number Of Mines: ???";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MineGridController.newGame(mineGrid, gridSizeTrackBar.Value, gridSizeTrackBar.Value, numberOfMinesTrackBar.Value);
            Close();
        }
    }
}
