using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineGrid {
    [Serializable]
    public class Data {

        public int rowCount { get; set; }
        public int columnCount { get; set; }
        public List<(int rowIndex, int columnIndex)> lista { get; set; }
        public List<string> list { get; set; }
        public Data(int rowCount, int columnCount, List<(int, int)> lista, List<string> list) {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.lista = lista;
            this.list = list;
        }

        public Data() {

        }
    }
}