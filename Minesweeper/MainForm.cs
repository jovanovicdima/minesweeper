using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using MineGrid;

namespace Minesweeper
{
    public partial class MainForm : Form
    {

        public MainForm()
        {

            InitializeComponent();
            MineGridController.timer = timer;
            MineGridController.time = new();
            MineGridController.newGame(mineGrid, rowCount: 9, columnCount: 9, numberOfMiness: 10);
        }

        private void mineGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mineGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag == "flag") return;
            if (MineGridController.isBomb(e.RowIndex, e.ColumnIndex))
            {
                MineGridController.endGameDefeat(mineGrid);
                MineGridController.setupIcon(mineGrid, e.RowIndex, e.ColumnIndex, "Icons/triggeredBomb.ico");
            }
            else
            {
                MineGridController.openTiles(mineGrid, e.RowIndex, e.ColumnIndex);
            }

        }

        private void mineGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int columnIndex = mineGrid.HitTest(e.X, e.Y).ColumnIndex;
                int rowIndex = mineGrid.HitTest(e.X, e.Y).RowIndex;
                if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "open") return;
                if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "flag")
                {
                    MineGridController.setupIcon(mineGrid, rowIndex, columnIndex, "Icons/closedTile.ico");
                    mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "closed";
                    if (MineGridController.isBomb(rowIndex, columnIndex))
                    {
                        MineGridController.numberOfCorrectFlags--;
                    }
                    MineGridController.numberOfAllFlags--;

                }
                else
                {
                    MineGridController.setupIcon(mineGrid, rowIndex, columnIndex, "Icons/flag.ico");
                    mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "flag";
                    if (MineGridController.isBomb(rowIndex, columnIndex))
                    {
                        MineGridController.numberOfCorrectFlags++;
                    }
                    MineGridController.numberOfAllFlags++;
                }

                int counter = 0;
                for(int i = 0; i < mineGrid.ColumnCount; i++)
                {
                    if(MineGridController.isBomb(rowIndex, i))
                    {
                        counter++;
                    }
                }
                MessageBox.Show(counter.ToString());

                MineGridController.isEndGame(mineGrid);

            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            MineGridController.time += TimeSpan.FromSeconds(0.1);
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameForm newGameForm = new NewGameForm(mineGrid);
            newGameForm.ShowDialog();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "") return;
            MineGridController.upisiUXMLFile(mineGrid, saveFileDialog.FileName);
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == "") return;
            MineGridController.ucitajUXMLFile(mineGrid, openFileDialog.FileName);
        }

        private void surrenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MineGridController.otvoriSve(mineGrid);
        }
    }
}