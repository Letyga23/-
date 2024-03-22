using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Рыболовная_фирма
{
    internal class DataGridWorker
    {
        /// <summary>
        /// Метод для фильтрации DataGrid.
        /// Для его работы, ему необходимо передать DataGrid м номер столбца в котором будет осуществлен поиск, так же необходимо передать само искомое значение.
        /// По результатам поиска, метод возвращает DataGrid.
        /// <example>
        /// <code>
        /// Пример правильного использования метода:
        /// 1) Необходимо объявить глобальную переменную List≺DataGridViewRow≻ CloneRows = new List≺DataGridViewRow≻();
        /// 2) После заполнения данными датагрида выполнить его клонирование:
        /// foreach (DataGridViewRow row in DataGrid.Rows)
        ///     CloneRows.Add(row);
        /// 3) Объявить глобальную переменную int count = 0;
        /// 4) В событии TextChanged TextBox'a выполнить следующую проверку:
        /// if (TextBoxSearch.Text.Length > count)
        ///     count++;
        /// else
        /// {
        ///     count--;
        ///     DataGrid = DataGridWorker.ResetFilter(CloneRows, DataGrid);
        ///     return;
        /// }
        /// 5) Вызвать поиск значения в датагриде DataGrid = DataGridWorker.SearchInDataGrid(DataGrid, 4, TextBoxSearch.Text);
        /// </code>
        /// </example>
        /// </summary>
        public static DataGridView SearchInDataGrid(DataGridView DataGrid, int Collumn, string Search)
        {
            int CountRows = DataGrid.RowCount;
            int CountDeleteRows = 0;

            for (int i = 0; i < CountRows; i++)
            {
                int index = i - CountDeleteRows;
                DataGrid.Rows[index].Selected = false;

                if (DataGrid.Rows[index].Cells[Collumn].Value.ToString().StartsWith(Search))
                    DataGrid.Rows[index].Selected = true;
                else
                {
                    DataGridViewRow row = DataGrid.Rows[index];
                    DataGrid.Rows.Remove(row);
                    CountDeleteRows++;
                }
            }

            return DataGrid;
        }

        /// <summary>
        /// Метод для сброса фильтра поиска в DataGrid.
        /// Для его работы, ему необходимо передать лист строк датагрида и сам датагрид в котором необходимо сбросить фильтр поиска.
        /// По результатам поиска, метод возвращает DataGrid.
        /// <example>
        /// <code>
        /// Пример правильного использования метода:
        /// DataGrid = DataGridWorker.ResetFilter(CloneRows, DataGrid);
        /// </code>
        /// </example>
        /// </summary>
        public static DataGridView ResetFilter(List<DataGridViewRow> CloneRows, DataGridView DataGrid)
        {
            DataGrid.Rows.Clear();

            foreach (DataGridViewRow row in CloneRows)
            {
                DataGrid.Rows.Add(row);
            }

            return DataGrid;
        }
    }
}
