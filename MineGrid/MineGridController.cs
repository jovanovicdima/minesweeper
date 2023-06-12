using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MineGrid {
    static public class MineGridController {

        static List<(int rowIndex, int columnIndex)> lista;
        static public int numberOfCorrectFlags;
        static public int numberOfAllFlags;
        static public int numberOfMines;
        static int numberOfOpenTiles;

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

        static public void setupMineGrid(DataGridView mineGrid, int rowCount, int columnCount, int numberOfMines) {
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
        }

        static public void endGameDefeat(DataGridView mineGrid) {
            Image bombTile = Image.FromFile("Icons/bomb.ico");
            foreach (var item in lista) {
                setupIcon(mineGrid, item.rowIndex, item.columnIndex, bombTile);
            }
            mineGrid.Enabled = false;
        }

        static public void openTilesRecursive(DataGridView mineGrid, int rowIndex, int columnIndex, bool recursion) {
            if (isBomb(rowIndex, columnIndex)) return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag.ToString() == "open") return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag.ToString() == "flag") return;
            if (recursion && mineGrid.Rows[rowIndex].Cells[columnIndex].Tag.ToString() == "questionMark") return;

            int count = countBombs(mineGrid, rowIndex, columnIndex);

            switch (count) {
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
            GameWon(mineGrid);

            if (count != 0) return;

            if (rowIndex + 1 < mineGrid.RowCount) { //down
                openTilesRecursive(mineGrid, rowIndex + 1, columnIndex, true);
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex + 1 < mineGrid.ColumnCount) { //down-right
                openTilesRecursive(mineGrid, rowIndex + 1, columnIndex + 1, true);
            }
            if (columnIndex + 1 < mineGrid.ColumnCount) { //right
                openTilesRecursive(mineGrid, rowIndex, columnIndex + 1, true);
            }
            if (rowIndex - 1 >= 0 && columnIndex + 1 < mineGrid.ColumnCount) { //up-right
                openTilesRecursive(mineGrid, rowIndex - 1, columnIndex + 1, true);
            }
            if (rowIndex - 1 >= 0) { //up
                openTilesRecursive(mineGrid, rowIndex - 1, columnIndex, true);
            }
            if (rowIndex - 1 >= 0 && columnIndex - 1 >= 0) { //up-left
                openTilesRecursive(mineGrid, rowIndex - 1, columnIndex - 1, true);
            }
            if (columnIndex - 1 >= 0) { //left
                openTilesRecursive(mineGrid, rowIndex, columnIndex - 1, true);
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex - 1 >= 0) { //down-left
                openTilesRecursive(mineGrid, rowIndex + 1, columnIndex - 1, true);
            }
        }

        static public int countBombs(DataGridView mineGrid, int rowIndex, int columnIndex) {
            int count = 0;
            if (rowIndex + 1 < mineGrid.RowCount) { //down
                if (isBomb(rowIndex + 1, columnIndex)) count++;
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex + 1 < mineGrid.ColumnCount) { //down-right
                if (isBomb(rowIndex + 1, columnIndex + 1)) count++;
            }
            if (columnIndex + 1 < mineGrid.ColumnCount) { //right
                if (isBomb(rowIndex, columnIndex + 1)) count++;
            }
            if (rowIndex - 1 >= 0 && columnIndex + 1 < mineGrid.ColumnCount) { //up-right
                if (isBomb(rowIndex - 1, columnIndex + 1)) count++;
            }
            if (rowIndex - 1 >= 0) { //up
                if (isBomb(rowIndex - 1, columnIndex)) count++;
            }
            if (rowIndex - 1 >= 0 && columnIndex - 1 >= 0) { //up-left
                if (isBomb(rowIndex - 1, columnIndex - 1)) count++;
            }
            if (columnIndex - 1 >= 0) { //left
                if (isBomb(rowIndex, columnIndex - 1)) count++;
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex - 1 >= 0) { //down-left
                if (isBomb(rowIndex + 1, columnIndex - 1)) count++;
            }

            return count;
        }

        static public void openTile(DataGridView mineGrid, int rowIndex, int columnIndex) {
            if(isBomb(rowIndex, columnIndex)) return;
            switch (countBombs(mineGrid, rowIndex, columnIndex)) {
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
        }
        static public bool isBomb(int rowIndex, int columnIndex) {
            return !lista.Find(x => x.rowIndex == rowIndex && x.columnIndex == columnIndex).Equals(default((int, int)));
        }

        static public bool GameWon(DataGridView mineGrid) {
            if (numberOfCorrectFlags == numberOfAllFlags && numberOfAllFlags == numberOfMines && numberOfOpenTiles == mineGrid.RowCount * mineGrid.ColumnCount - numberOfMines) {
                mineGrid.Enabled = false;
                return true;
            }
            return false;
        }

        static public void setupIcon(DataGridView mineGrid, int rowIndex, int columnIndex, string filePath) {
            mineGrid.Rows[rowIndex].Cells[columnIndex] = new DataGridViewImageCell();
            mineGrid.Rows[rowIndex].Cells[columnIndex].Value = Image.FromFile(filePath);
            mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "closed";
            mineGrid.Rows[rowIndex].Cells[columnIndex].ToolTipText = "";
            if (mineGrid.Rows[rowIndex].Cells[columnIndex] is DataGridViewImageCell imageCell) {
                imageCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        static public void setupIcon(DataGridView mineGrid, int rowIndex, int columnIndex, Image image) {
            mineGrid.Rows[rowIndex].Cells[columnIndex] = new DataGridViewImageCell();
            mineGrid.Rows[rowIndex].Cells[columnIndex].Value = image;
            mineGrid.Rows[rowIndex].Cells[columnIndex].ToolTipText = "";
            mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "closed";
            if (mineGrid.Rows[rowIndex].Cells[columnIndex] is DataGridViewImageCell imageCell) {
                imageCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        static public void newGame(DataGridView mineGrid, int rowCount, int columnCount, int numberOfMiness) {
            setupMineGrid(mineGrid, rowCount, columnCount, numberOfMiness);
            numberOfCorrectFlags = 0;
            numberOfAllFlags = 0;
            numberOfOpenTiles = 0;
            numberOfMines = numberOfMiness;
            mineGrid.Enabled = true;
        }

        public static void saveXML(DataGridView mineGrid, string filePath) {
            XmlSerializer serializer = new XmlSerializer(typeof(Data));

            List<string> list = new List<string>();
            for (int i = 0; i < mineGrid.ColumnCount; i++) {
                for (int j = 0; j < mineGrid.RowCount; j++) {
                    list.Add(mineGrid.Rows[j].Cells[i].Tag.ToString());
                }
            }

            Data x = new Data(mineGrid.RowCount, mineGrid.ColumnCount, lista, list);

            StreamWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer, x);
            writer.Close();
        }

        public static void loadXML(DataGridView mineGrid, string filePath) {
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            StreamReader reader = new StreamReader(filePath);
            Data x = (Data)serializer.Deserialize(reader);

            int rowCount = x.rowCount;
            int columnCount = x.columnCount;
            List<(int rowIndex, int columnIndex)> lista = x.lista;
            List<string> list = x.list;
            reader.Close();

            newGame(mineGrid, rowCount, columnCount, lista.Count);
            MineGridController.lista = lista;
            int counter = 0;
            for (int i = 0; i < mineGrid.ColumnCount; i++) {
                for (int j = 0; j < mineGrid.RowCount; j++) {
                    mineGrid.Rows[j].Cells[i].Tag = list[counter];
                    counter++;
                    if (mineGrid.Rows[j].Cells[i].Tag.ToString() == "flag") {
                        setupIcon(mineGrid, j, i, "Icons/flag.ico");
                    }
                    if (mineGrid.Rows[j].Cells[i].Tag.ToString() == "questionMark") {
                        setupIcon(mineGrid, j, i, "Icons/questionMark.ico");
                    }
                    if (mineGrid.Rows[j].Cells[i].Tag.ToString() == "open") openTile(mineGrid, j, i);
                }
            }
        }

        static public void openAllTiles(DataGridView mineGrid) {
            for (int i = 0; i < mineGrid.ColumnCount; i++) {
                for (int j = 0; j < mineGrid.RowCount; j++) {
                    openTile(mineGrid, j, i);
                    if (isBomb(j, i)) setupIcon(mineGrid, j, i, "Icons/bomb.ico");
                }
            }
        }
    }
}