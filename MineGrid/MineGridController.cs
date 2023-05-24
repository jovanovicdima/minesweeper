using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MineGrid {
    static public class MineGridController
    {

        static List<(int rowIndex, int columnIndex)> lista;
        static public int numberOfCorrectFlags;
        static public int numberOfAllFlags;
        static public int numberOfMines;
        static int numberOfOpenTiles;
        static public TimeSpan time;
        static public Timer timer;

        static public List<(int rowIndex, int columnIndex)> setupMines(int rowCount, int columnCount, int numberOfMines)
        {

            List<(int rowIndex, int columnIndex)> listaBombi = new List<(int rowIndex, int columnIndex)>();
            Random random = new Random();
            for (int i = 0; i < numberOfMines;)
            {
                int rowIndex = random.Next(0, rowCount);
                int columnIndex = random.Next(0, columnCount);
                if (listaBombi.Find(x => x.rowIndex == rowIndex && x.columnIndex == columnIndex).Equals(default((int, int))))
                {
                    listaBombi.Add((rowIndex, columnIndex));
                    i++;
                }
            }


            return listaBombi;
        }

        static public void setupMineGrid(DataGridView mineGrid, int rowCount, int columnCount, int numberOfMines)
        {
            mineGrid.RowCount = rowCount;
            mineGrid.ColumnCount = columnCount;

            mineGrid.Width = mineGrid.ColumnCount * 30;
            mineGrid.Height = mineGrid.RowCount * 30;


            for (int i = 0; i < mineGrid.ColumnCount; i++)
            {
                mineGrid.Columns[i].Width = 30;
            }

            for (int i = 0; i < mineGrid.RowCount; i++)
            {
                mineGrid.Rows[i].Height = 30;
            }

            Image closedTile = Image.FromFile("Icons/closedTile.ico");


            for (int i = 0; i < mineGrid.ColumnCount; i++)
            {
                for (int j = 0; j < mineGrid.RowCount; j++)
                {
                    setupIcon(mineGrid, j, i, closedTile);
                }
            }

            lista = setupMines(rowCount, columnCount, numberOfMines);

        }

        static public void endGameDefeat(DataGridView mineGrid)
        {
            Image bombTile = Image.FromFile("Icons/bomb.ico");
            timer.Stop();
            foreach (var item in lista)
            {
                setupIcon(mineGrid, item.rowIndex, item.columnIndex, bombTile);
            }
        }

        static public void openTiles(DataGridView mineGrid, int rowIndex, int columnIndex)
        {

            timer.Start();
            if (isBomb(rowIndex, columnIndex)) return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "open") return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "flag") return;
            if (mineGrid.Rows[rowIndex].Cells[columnIndex].Tag == "questionMark") return;

            int count = 0;

            if (rowIndex + 1 < mineGrid.RowCount)
            { //down
                if (isBomb(rowIndex + 1, columnIndex)) count++;
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex + 1 < mineGrid.ColumnCount)
            { //down-right
                if (isBomb(rowIndex + 1, columnIndex + 1)) count++;
            }
            if (columnIndex + 1 < mineGrid.ColumnCount)
            { //right
                if (isBomb(rowIndex, columnIndex + 1)) count++;
            }
            if (rowIndex - 1 >= 0 && columnIndex + 1 < mineGrid.ColumnCount)
            { //up-right
                if (isBomb(rowIndex - 1, columnIndex + 1)) count++;
            }
            if (rowIndex - 1 >= 0)
            { //up
                if (isBomb(rowIndex - 1, columnIndex)) count++;
            }
            if (rowIndex - 1 >= 0 && columnIndex - 1 >= 0)
            { //ip-left
                if (isBomb(rowIndex - 1, columnIndex - 1)) count++;
            }
            if (columnIndex - 1 >= 0)
            { //left
                if (isBomb(rowIndex, columnIndex - 1)) count++;
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex - 1 >= 0)
            { //down-left
                if (isBomb(rowIndex + 1, columnIndex - 1)) count++;
            }


            switch (count)
            {
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
            isEndGame(mineGrid);


            if (count != 0) return;

            if (rowIndex + 1 < mineGrid.RowCount)
            { //down
                openTiles(mineGrid, rowIndex + 1, columnIndex);

            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex + 1 < mineGrid.ColumnCount)
            { //down-right
                openTiles(mineGrid, rowIndex + 1, columnIndex + 1);
            }
            if (columnIndex + 1 < mineGrid.ColumnCount)
            { //right
                openTiles(mineGrid, rowIndex, columnIndex + 1);
            }
            if (rowIndex - 1 >= 0 && columnIndex + 1 < mineGrid.ColumnCount)
            { //up-right
                openTiles(mineGrid, rowIndex - 1, columnIndex + 1);
            }
            if (rowIndex - 1 >= 0)
            {
                openTiles(mineGrid, rowIndex - 1, columnIndex);
            }
            if (rowIndex - 1 >= 0 && columnIndex - 1 >= 0)
            {
                openTiles(mineGrid, rowIndex - 1, columnIndex - 1);
            }
            if (columnIndex - 1 >= 0)
            { //left
                openTiles(mineGrid, rowIndex, columnIndex - 1);
            }
            if (rowIndex + 1 < mineGrid.RowCount && columnIndex - 1 >= 0)
            { //down-left
                openTiles(mineGrid, rowIndex + 1, columnIndex - 1);
            }

        }

        static public bool isBomb(int rowIndex, int columnIndex)
        {
            return !lista.Find(x => x.rowIndex == rowIndex && x.columnIndex == columnIndex).Equals(default((int, int)));
        }

        static public bool isEndGame(DataGridView mineGrid)
        {
            if (numberOfCorrectFlags == numberOfAllFlags && numberOfAllFlags == numberOfMines && numberOfOpenTiles == mineGrid.RowCount * mineGrid.ColumnCount - numberOfMines)
            {
                MessageBox.Show($"{time.TotalSeconds} s");
                mineGrid.Enabled = false;
                timer.Stop();
            }
            return true;
        }

        static public void setupIcon(DataGridView mineGrid, int rowIndex, int columnIndex, string filePath)
        {
            mineGrid.Rows[rowIndex].Cells[columnIndex] = new DataGridViewImageCell();
            mineGrid.Rows[rowIndex].Cells[columnIndex].Value = Image.FromFile(filePath);
            mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "closed";
            mineGrid.Rows[rowIndex].Cells[columnIndex].ToolTipText = "";
            if (mineGrid.Rows[rowIndex].Cells[columnIndex] is DataGridViewImageCell imageCell)
            {
                imageCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        static public void setupIcon(DataGridView mineGrid, int rowIndex, int columnIndex, Image image)
        {
            mineGrid.Rows[rowIndex].Cells[columnIndex] = new DataGridViewImageCell();
            mineGrid.Rows[rowIndex].Cells[columnIndex].Value = image;
            mineGrid.Rows[rowIndex].Cells[columnIndex].ToolTipText = "";
            mineGrid.Rows[rowIndex].Cells[columnIndex].Tag = "closed";
            if (mineGrid.Rows[rowIndex].Cells[columnIndex] is DataGridViewImageCell imageCell)
            {
                imageCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        static public void newGame(DataGridView mineGrid, int rowCount, int columnCount, int numberOfMiness)
        {
            setupMineGrid(mineGrid, rowCount, columnCount, numberOfMiness);
            numberOfCorrectFlags = 0;
            numberOfAllFlags = 0;
            numberOfOpenTiles = 0;
            numberOfMines = numberOfMiness;
            mineGrid.Enabled = true;
        }

        public static void upisiUXMLFile(DataGridView mineGrid, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Data));

            List<string> list = new List<string>();
            for (int i = 0; i < mineGrid.ColumnCount; i++)
            {
                for (int j = 0; j < mineGrid.RowCount; j++)
                {
                    list.Add(mineGrid.Rows[j].Cells[i].Tag.ToString());
                }
            }

            Data x = new Data(mineGrid.RowCount, mineGrid.ColumnCount, lista, list, time);

            StreamWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer, x);
            writer.Close();


            //XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.Unicode);

            //writer.WriteStartElement("rowCount");
            //intSerializer.Serialize(writer, mineGrid.RowCount);
            //writer.WriteStartElement("/rowCount");

            //writer.WriteStartElement("columnCount");
            //intSerializer.Serialize(writer, mineGrid.ColumnCount);
            //writer.WriteEndElement();

            //writer.WriteStartElement("mines");
            //tupleSerializer.Serialize(writer, lista);
            //writer.WriteEndElement();

            //writer.WriteStartElement("tiles");
            //stringSerializer.Serialize(writer, list);
            //writer.WriteEndElement();

            //writer.WriteStartElement("timer");
            //timeSpanSerializer.Serialize(writer, time);
            //writer.WriteEndElement()

            writer.Close();
        }

        public static void ucitajUXMLFile(DataGridView mineGrid, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            StreamReader reader = new StreamReader(filePath);
            Data x = (Data)serializer.Deserialize(reader);

            int rowCount = x.rowCount;
            int columnCount = x.columnCount;
            List<(int rowIndex, int columnIndex)> lista = x.lista;
            List<string> list = x.list;
            TimeSpan t = x.t;
            reader.Close();

            newGame(mineGrid, rowCount, columnCount, lista.Count);
            MineGridController.lista = lista;
            time = t;
            int counter = 0;
            for (int i = 0; i < mineGrid.ColumnCount; i++)
            {
                for (int j = 0; j < mineGrid.RowCount; j++)
                {
                    mineGrid.Rows[j].Cells[i].Tag = list[counter];
                    counter++;
                    if (mineGrid.Rows[j].Cells[i].Tag.ToString() == "flag")
                    {
                        setupIcon(mineGrid, j, i, "Icons/flag.ico");
                    }
                    
                }
            }
            for (int i = 0; i < mineGrid.ColumnCount; i++)
            {
                for (int j = 0; j < mineGrid.RowCount; j++)
                {
                    if (mineGrid.Rows[j].Cells[i].Tag.ToString() == "open") openTiles(mineGrid, j, i);

                }
            }

            timer.Start();




        }

        static public void otvoriSve(DataGridView mineGrid)
        {
            for (int i = 0; i < mineGrid.ColumnCount; i++)
            {
                for (int j = 0; j < mineGrid.RowCount; j++)
                {
                    openTiles(mineGrid, j, i);
                    if (isBomb(j, i)) setupIcon(mineGrid, j, i, "Icons/bomb.ico");

                }
            }
        }

    }
}
