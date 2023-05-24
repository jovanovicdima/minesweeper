using System.Windows.Forms;
using System.Xml.Serialization;
using MineGrid;

namespace Minesweeper {
    public partial class Form1 : Form {

        List<(int rowIndex, int columnIndex)> lista;

        public Form1() {

            InitializeComponent();
            MineGridController.setupMineGrid(mineGrid: mineGrid, rowCount: 9, columnCount: 9, numberOfMines: 15, lista: out lista);
            Width = mineGrid.Width + 16;
            Height = mineGrid.Height + 53 + 9;
            MineGridController.numberOfCorrectFlags = 0;
            MineGridController.numberOfAllFlags = 0;
            MineGridController.numberOfMines = 15;
        }

        private void mineGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (mineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag == "flag") return;
                if (MineGridController.isBomb(mineGrid, e.RowIndex, e.ColumnIndex, lista)) {
                MineGridController.endGameDefeat(mineGrid, lista);
                MineGridController.setupIcon(mineGrid, e.RowIndex, e.ColumnIndex, "Icons/triggeredBomb.ico");
            } else {
                MineGridController.openTiles(mineGrid, e.RowIndex, e.ColumnIndex, lista);
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

        }

        private void mineGrid_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                int columnIndex = mineGrid.HitTest(e.X, e.Y).ColumnIndex;
                int rowIndex = mineGrid.HitTest(e.X, e.Y).RowIndex;
                if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "open") return;
                if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "flag") {
                    MineGridController.setupIcon(mineGrid, rowIndex, columnIndex, "Icons/closedTile.ico");
                    mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "";
                    if (MineGridController.isBomb(mineGrid, rowIndex, columnIndex, lista)) {
                        MineGridController.numberOfCorrectFlags--;
                    }
                    MineGridController.numberOfAllFlags--;

                } else {
                    MineGridController.setupIcon(mineGrid, rowIndex, columnIndex, "Icons/flag.ico");
                    mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "flag";
                    if (MineGridController.isBomb(mineGrid, rowIndex, columnIndex, lista)) {
                        MineGridController.numberOfCorrectFlags++;
                    }
                    MineGridController.numberOfAllFlags++;
                }


                MineGridController.isEndGame(mineGrid, lista);

            }
        }
    }
}