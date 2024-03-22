using Bunifu.UI.WinForms.BunifuButton;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace Рыболовная_фирма
{
    public partial class DataGrid : Form
    {
        public string index_DataGrid;
        DataGridViewSelectedRowCollection Rows;
        int Role = -1;
        public int fish1;
        public DataGrid(int Role)
        {
            this.Role = Role;
            InitializeComponent();
            ChangeVisible(Role);
        }

        public DataGrid()
        {
        }

        public void ChangeVisible(int Role)
        {
            if (Role == 0)
            {
                bunifuButton26.Visible = false;
                bunifuButton25.Visible = false;
                bunifuButton24.Visible = false;
                bunifuButton2.Visible = false;
                bunifuButton3.Visible = false;
                bunifuButton1.Visible = false;
                bunifuButton5.Visible = false;
                bunifuButton6.Visible = false;
                bunifuButton4.Visible = false;
                bunifuButton8.Visible = false;
                bunifuButton9.Visible = false;
                bunifuButton7.Visible = false;
                bunifuButton14.Visible = false;
                bunifuButton15.Visible = false;
                bunifuButton13.Visible = false;
                bunifuButton17.Visible = false;
                bunifuButton18.Visible = false;
                bunifuButton16.Visible = false;
            }
        }

        SqlConnection con = new SqlConnection(DataBaseWorker.GetConnString());
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        private void Fish_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Name == index_DataGrid)
                {
                    tabControl1.SelectedIndex = i;
                }
            }

            Glav gl = new Glav(Role);
            if (gl.tabControl1.SelectedTab == gl.Fishing_trip)
                GiveRowsCaught(Rows);

            FishSum();
        }

        private void OpenRedactor(string columnName)
        {
            Redactor redactor = new Redactor(Role);

            redactor.Kater = label3.Text;
            redactor.DataOtp = label2.Text;
            redactor.DataPrib = label4.Text;
            redactor.index = columnName;
            redactor.StartPosition = FormStartPosition.CenterParent;
            redactor.ShowDialog();
        }

        private void OpenRedactorIzm(DataGridView dataGridView, string methodName, string columnName)
        {
            Redactor redactor = new Redactor(Role);
            DataGridViewSelectedRowCollection Rows = dataGridView.SelectedRows;

            MethodInfo method = redactor.GetType().GetMethod(methodName);
            if (method != null)
            {
                method.Invoke(redactor, new object[] { Rows });
            }

            redactor.index = columnName;
            redactor.StartPosition = FormStartPosition.CenterParent;
            redactor.ShowDialog();
        }

        private void Delete(DataGridView dataGridView, string table, string Id)
        {
            string id = dataGridView[0, dataGridView.CurrentRow.Index].Value.ToString();
            DialogResult dr = MessageBox.Show("Вы уверены? При удалении записи, также будут удалены все записи связанные с ней.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from " + table + " where " + Id + " = " + Convert.ToInt32(id);
                cmd.ExecuteNonQuery();
                con.Close();
                Get_List_Caught_fish();
                Get_List_Types_of_katers();
                Get_List_Types_of_fish();
                Get_List_Locations();
                Get_List_klient();
                Get_List_Post();
                Get_List_Fish_quality();

                if (fish1 == 1)
                    GiveRowsCaught(Rows);

                FishSum();
            }
        }
        public void Get_List_Caught_fish()
        {
            DataBaseWorker.LoadData("Caught_fish_pred", dataGridView1);
        }
        public void Get_List_Types_of_katers()
        {
            DataBaseWorker.LoadData("Types_of_katers_pred", dataGridView2);
        }
        public void Get_List_Types_of_fish()
        {
            DataBaseWorker.LoadData("Types_of_fish_pred", dataGridView3);
        }
        public void Get_List_Locations()
        {
            DataBaseWorker.LoadData("Locations_pred", dataGridView5);
        }
        public void Get_List_klient()
        {
            DataBaseWorker.LoadData("klient_pred", dataGridView4);
        }
        public void Get_List_Post()
        {
            DataBaseWorker.LoadData("Post_pred", dataGridView6);
        }
        public void Get_List_Fish_quality()
        {
            DataBaseWorker.LoadData("Fish_quality_pred", dataGridView7);
        }
        private void bunifuButton41_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            OpenRedactor("Caught_fish_dob");
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView1, "GiveRowsCaught", "Caught_fish_izm");
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            OpenRedactor("Types_of_katers_dob");
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView2, "GiveRowsTypes_of_katers", "Types_of_katers_izm");
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            OpenRedactor("Types_of_fish_dob");
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView3, "GiveRowsTypes_of_fish", "Types_of_fish_izm");
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            OpenRedactor("Klient_dob");
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView4, "GiveRowsKlient", "Klient_izm");
        }

        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            OpenRedactor("Post_dob");
        }

        private void bunifuButton15_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView6, "GiveRowsPost", "Post_izm");
        }

        private void bunifuButton17_Click(object sender, EventArgs e)
        {
            OpenRedactor("Fish_quality_dob");
        }

        private void bunifuButton18_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView7, "GiveRowsFish_quality", "Fish_quality_izm");
        }
        public void GiveRowsCaught(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            con = new SqlConnection(DataBaseWorker.GetConnString());
            da = new SqlDataAdapter("SET DATEFORMAT dmy; select * from Caught_fish_pred where [Дата вылова] between '" + Rows[0].Cells[3].Value.ToString() + "' and '" + Rows[0].Cells[4].Value.ToString() + "' and [Судно] = N'" + Rows[0].Cells[1].Value.ToString() + "'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Caught_fish_pred");
            dataGridView1.DataSource = ds.Tables["Caught_fish_pred"];
            con.Close();       
        }

        private void FishSum()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }
            label1.Text = "Всего поймано: " + Convert.ToString(sum) + "кг";
        }

        private void DataGrid_Activated(object sender, EventArgs e)
        {
            Get_List_Caught_fish();
            Get_List_Types_of_katers();
            Get_List_Types_of_fish();
            Get_List_Locations();
            Get_List_klient();
            Get_List_Post();
            Get_List_Fish_quality();

            if (fish1 == 1)
            GiveRowsCaught(Rows);

            FishSum();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            Delete(dataGridView1, "Caught_fish", "Id_Caught_fish");
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Delete(dataGridView2, "Types_of_katers", "Id_types");
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            Delete(dataGridView3, "Types_of_fish", "Id_Types_of_fish");
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            Delete(dataGridView4, "Klient", "Id_Klienta");
        }

        private void bunifuButton13_Click(object sender, EventArgs e)
        {
            Delete(dataGridView6, "Post", "Id_Post");
        }

        private void bunifuButton16_Click(object sender, EventArgs e)
        {
            Delete(dataGridView7, "Fish_quality", "Id_quality");
        }

        private void bunifuButton19_Click(object sender, EventArgs e)
        {
            this.Rows = Rows;
            Excel.Application exApp = new Excel.Application();
            exApp.Application.Workbooks.Add(Type.Missing);
            exApp.Application.Columns.ColumnWidth = 20;

            Excel.Range _excelCells = (Excel.Range)exApp.get_Range("A1", "F1").Cells;
            _excelCells.Merge(Type.Missing);
            exApp.Cells[1, 1].Value = "Информация о рейсе с   " + Rows[0].Cells[3].Value.ToString() + "   по   " + Rows[0].Cells[4].Value.ToString();
            exApp.Cells[1, 1].Font.Size = 14;
            exApp.Cells[1, 1].Font.Italic = true;
            exApp.Cells[1, 1].Font.Bold = true;
            exApp.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 1] = "№";
            exApp.Cells[3, 1].Font.Bold = true;
            exApp.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 2] = "Судно";
            exApp.Cells[3, 2].Font.Bold = true;
            exApp.Cells[3, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 3] = "Вид рыбы";
            exApp.Cells[3, 3].Font.Bold = true;
            exApp.Cells[3, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 4] = "Количество";
            exApp.Cells[3, 4].Font.Bold = true;
            exApp.Cells[3, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 5] = "Дата вылова";
            exApp.Cells[3, 5].Font.Bold = true;
            exApp.Cells[3, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 6] = "Качество";
            exApp.Cells[3, 6].Font.Bold = true;
            exApp.Cells[3, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            int j = 0 , i = 0;
            for (i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (j = 0; j < dataGridView1.RowCount; j++)
                {
                    exApp.Cells[j + 4, i + 1] = (dataGridView1[i, j].Value).ToString();
                    exApp.Cells[j + 4, i + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
            }

            exApp.Cells[j + 5, 1].Value = "Всего поймано: ";
            exApp.Cells[j + 5, 1].Font.Size = 14;
            exApp.Cells[j + 5, 1].Font.Bold = true;
            exApp.Cells[j + 5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            exApp.Cells[j+5, 2].FormulaLocal = "=СУММ(D4:D999)";
            exApp.Cells[j+5, 2].Font.Size = 14;
            exApp.Cells[j+5, 2].Font.Bold = true;
            exApp.Cells[j+5, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            exApp.Visible = true;
        }
        public DataTable GetSelectedData()
        {
            DataTable selectedData = new DataTable();
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                selectedData.Columns.Add(column.Name);
            }
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                DataRow dataRow = selectedData.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataRow[cell.ColumnIndex] = cell.Value;
                }
                selectedData.Rows.Add(dataRow);
            }
            return selectedData;
        }
    }
}