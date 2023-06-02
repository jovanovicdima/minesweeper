using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using MineGrid;

namespace Minesweeper {
    public partial class MainForm : Form {

        public MainForm() {

            InitializeComponent();
            MineGridController.newGame(mineGrid, rowCount: 9, columnCount: 9, numberOfMiness: 10);
        }

        private void mineGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (mineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToString() == "flag") return;
            if (MineGridController.isBomb(e.RowIndex, e.ColumnIndex) && mineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString() != "flag") {
                MineGridController.endGameDefeat(mineGrid);
                MineGridController.setupIcon(mineGrid, e.RowIndex, e.ColumnIndex, "Icons/triggeredBomb.ico");
            } else {
                MineGridController.openTilesRecursive(mineGrid, e.RowIndex, e.ColumnIndex, false);
            }
        }
        private void mineGrid_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                int columnIndex = mineGrid.HitTest(e.X, e.Y).ColumnIndex;
                int rowIndex = mineGrid.HitTest(e.X, e.Y).RowIndex;
                if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag.ToString() == "open") return;
                if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag.ToString() == "closed") {
                    MineGridController.setupIcon(mineGrid, rowIndex, columnIndex, "Icons/flag.ico");
                    mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "flag";
                    if (MineGridController.isBomb(rowIndex, columnIndex)) {
                        MineGridController.numberOfCorrectFlags++;
                    }
                    MineGridController.numberOfAllFlags++;
                } else if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag.ToString() == "flag") {
                    MineGridController.setupIcon(mineGrid, rowIndex, columnIndex, "Icons/questionMark.ico");
                    mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "questionMark";
                    if (MineGridController.isBomb(rowIndex, columnIndex)) {
                        MineGridController.numberOfCorrectFlags--;
                    }
                    MineGridController.numberOfAllFlags--;

                } else if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag.ToString() == "questionMark") {
                    MineGridController.setupIcon(mineGrid, rowIndex, columnIndex, "Icons/closedTile.ico");
                    mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "closed";
                }
                

                if(MineGridController.isGameWon(mineGrid)) {
                    MessageBox.Show("You won!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e) {
            NewGameForm newGameForm = new NewGameForm(mineGrid);
            newGameForm.ShowDialog();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e) {
            if(!mineGrid.Enabled) {
                MessageBox.Show("Ended game cannot be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            MineGridController.saveXML(mineGrid, saveFileDialog.FileName);
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            MineGridController.loadXML(mineGrid, openFileDialog.FileName);
        }

        private void surrenderToolStripMenuItem_Click(object sender, EventArgs e) {
            MineGridController.openAllTiles(mineGrid);
        }
    }
}