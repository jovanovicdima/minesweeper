using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineGrid {
    static public class MineGridController {

        static public int numberOfCorrectFlags;
        static public int numberOfAllFlags;
        static public int numberOfMines;
        static public int numberOfOpenTiles;

        static public List<(int rowIndex, int columnIndex)> setupMines(int rowCount, int columnCount, int numberOfMines) {

            List<(int rowIndex, int columnIndex)> listaBombi = new List<(int rowIndex, int columnIndex)>();
            Random random = new Random();
            for (int i = 0; i < numberOfMines;) {
                int rowIndex = random.Next(0, rowCount);
                int columnIndex = random.Next(0, columnCount);
                if (listaBombi.Find(x => x.rowIndex == rowIndex && x.columnIndex == columnIndex).Equals(default((int, int)))) {
                    listaBombi.Add((rowIndex, columnIndex));
                    i++;
                }
            }


            return listaBombi;
        }

        static public void setupMineGrid(DataGridView mineGrid, int rowCount, int columnCount, int numberOfMines, out List<(int rowIndex, int columnIndex)> lista) {
            mineGrid.RowCount = rowCount;
            mineGrid.ColumnCount = columnCount;

            mineGrid.Width = mineGrid.ColumnCount * 30;
            mineGrid.Height = mineGrid.RowCount * 30;

            
            for (int i = 0; i < mineGrid.ColumnCount; i++) {
                mineGrid.Columns[i].Width = 30;
            }

            for (int i = 0; i < mineGrid.RowCount; i++) {
                mineGrid.Rows[i].Height = 30;
            }

            Image closedTile = Image.FromFile("Icons/closedTile.ico");


            for (int i = 0; i < mineGrid.ColumnCount; i++) {
                for (int j = 0; j < mineGrid.RowCount; j++) {
                    setupIcon(mineGrid, j, i, closedTile);
                }
            }

            lista = setupMines(rowCount, columnCount, numberOfMines);

            //XmlSerializer x = new(typeof((int, int)));
            //StreamWriter writer = new("xd");
            //x.Serialize(writer, lista);
            //writer.Close();

        }

        static public void endGameDefeat(DataGridView mineGrid, List<(int rowIndex, int columnIndex)> lista) {
            Image bombTile = Image.FromFile("Icons/bomb.ico");
            foreach (var item in lista) {
                setupIcon(mineGrid, item.rowIndex, item.columnIndex, bombTile);
            }
            mineGrid.Enabled = false;
        }

        static public void openTiles(DataGridView mineGrid, int rowIndex, int columnIndex, List<(int rowIndex, int columnIndex)> lista) {

            if (isBomb(mineGrid, rowIndex, columnIndex, lista)) return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "open") return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "flag") return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "questionMark") return;

            int count = 0;

            if (rowIndex + 1 < mineGrid.RowCount) { //down
                if (isBomb(mineGrid, rowIndex + 1, columnIndex, lista)) count++;
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex + 1 < mineGrid.ColumnCount) { //down-right
                if (isBomb(mineGrid, rowIndex + 1, columnIndex + 1, lista)) count++;
            }
            if (columnIndex + 1 < mineGrid.ColumnCount) { //right
                if (isBomb(mineGrid, rowIndex, columnIndex + 1, lista)) count++;
            }
            if (rowIndex - 1 >= 0 && columnIndex + 1 < mineGrid.ColumnCount) { //up-right
                if (isBomb(mineGrid, rowIndex - 1, columnIndex + 1, lista)) count++;
            }
            if (rowIndex - 1 >= 0) { //up
                if (isBomb(mineGrid, rowIndex - 1, columnIndex, lista)) count++;
            }
            if (rowIndex - 1 >= 0 && columnIndex - 1 >= 0) { //ip-left
                if (isBomb(mineGrid, rowIndex - 1, columnIndex - 1, lista)) count++;
            }
            if (columnIndex - 1 >= 0) { //left
                if (isBomb(mineGrid, rowIndex, columnIndex - 1, lista)) count++;
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex - 1 >= 0) { //down-left
                if (isBomb(mineGrid, rowIndex + 1, columnIndex - 1, lista)) count++;
            }


            switch(count) {
                case 0: 
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile0.ico");
                    break;
                case 1:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile1.ico");
                    break;
                case 2:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile2.ico");
                    break;
                case 3:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile3.ico");
                    break;
                case 4:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile4.ico");
                    break;
                case 5:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile5.ico");
                    break;
                case 6:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile6.ico");
                    break;
                case 7:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile7.ico");
                    break;
                case 8:
                    setupIcon(mineGrid, rowIndex, columnIndex, "Icons/openTile8.ico");
                    break;
            }

            mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "open";
            numberOfOpenTiles++;
            isEndGame(mineGrid, lista);


            if (count != 0) return;

            if (rowIndex + 1 < mineGrid.RowCount) { //down
                openTiles(mineGrid, rowIndex + 1, columnIndex, lista);

            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex + 1 < mineGrid.ColumnCount) { //down-right
                openTiles(mineGrid, rowIndex + 1, columnIndex + 1, lista);
            }
            if (columnIndex + 1 < mineGrid.ColumnCount) { //right
                openTiles(mineGrid, rowIndex, columnIndex + 1, lista);
            }   
            if (rowIndex - 1 >= 0 && columnIndex + 1 < mineGrid.ColumnCount) { //up-right
                openTiles(mineGrid, rowIndex - 1, columnIndex + 1, lista);
            }
            if (rowIndex - 1 >= 0) {
                openTiles(mineGrid, rowIndex - 1, columnIndex, lista);
            }
            if (rowIndex - 1 >= 0 && columnIndex - 1 >= 0) {
                openTiles(mineGrid, rowIndex - 1, columnIndex - 1, lista);
            }
            if (columnIndex - 1 >= 0) { //left
                openTiles(mineGrid, rowIndex, columnIndex - 1, lista);
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex - 1 >= 0) { //down-left
                openTiles(mineGrid, rowIndex + 1, columnIndex - 1, lista);
            }

        }

        static public bool isBomb(DataGridView mineGrid, int rowIndex, int columnIndex, List<(int rowIndex, int columnIndex)> lista) {
            return !lista.Find(x => x.rowIndex == rowIndex && x.columnIndex == columnIndex).Equals(default((int, int)));
        }

        static public bool isEndGame(DataGridView mineGrid, List<(int rowIndex, int columnIndex)> lista) {
            if (numberOfCorrectFlags == numberOfAllFlags && numberOfAllFlags == numberOfMines && numberOfOpenTiles == mineGrid.RowCount * mineGrid.ColumnCount - numberOfMines) {
                MessageBox.Show("xd");
                mineGrid.Enabled = false;
            }
            return true;
        }

        static public void setupIcon(DataGridView mineGrid, int rowIndex, int columnIndex, string filePath) {
            mineGrid.Rows[rowIndex].Cells[columnIndex] = new DataGridViewImageCell();
            mineGrid.Rows[rowIndex].Cells[columnIndex].Value = Image.FromFile(filePath);
            mineGrid.Rows[rowIndex].Cells[columnIndex].ToolTipText = "";
            if (mineGrid.Rows[rowIndex].Cells[columnIndex] is DataGridViewImageCell imageCell) {
                imageCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        static public void setupIcon(DataGridView mineGrid, int rowIndex, int columnIndex, Image image) {
            mineGrid.Rows[rowIndex].Cells[columnIndex] = new DataGridViewImageCell();
            mineGrid.Rows[rowIndex].Cells[columnIndex].Value = image;
            mineGrid.Rows[rowIndex].Cells[columnIndex].ToolTipText = "";
            if (mineGrid.Rows[rowIndex].Cells[columnIndex] is DataGridViewImageCell imageCell) {
                imageCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

    }
}
