using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace Рыболовная_фирма
{
    public partial class Glav : Form
    {
        int page = -1;
        public int roll;
        int Role;
        public Glav(int Role)
        {
            this.Role = Role;
            InitializeComponent();
            ChangeVisible(Role);
            Pages.Add(tabControl1.SelectedIndex);
        }

        public void ChangeVisible(int Role)
        {
            if (Role == 0)
            {
                bunifuButton18.Visible = false;
                bunifuButton19.Visible = false;
                bunifuButton20.Visible = false;
                bunifuButton23.Visible = false;
                bunifuButton22.Visible = false;
                bunifuButton21.Visible = false;
                bunifuButton26.Visible = false;
                bunifuButton25.Visible = false;
                bunifuButton24.Visible = false;
                bunifuButton29.Visible = false;
                bunifuButton28.Visible = false;
                bunifuButton27.Visible = false;
                bunifuButton32.Visible = false;
                bunifuButton31.Visible = false;
                bunifuButton30.Visible = false;
                bunifuButton35.Visible = false;
                bunifuButton34.Visible = false;
                bunifuButton33.Visible = false;
                bunifuButton37.Visible = false;
                bunifuButton38.Visible = false;
                bunifuButton50.Visible = false;
            }
        }

        SqlConnection con = new SqlConnection(DataBaseWorker.GetConnString());
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        int count = 0;

        private void OpenRedactor(string columnName)
        {
            Redactor redactor = new Redactor(Role);
            redactor.index = columnName;
            redactor.StartPosition = FormStartPosition.CenterParent;
            redactor.ShowDialog();
        }

        private void OpenGiveRows(string columnName)
        {
            DataGrid fish = new DataGrid(Role);
            fish.index_DataGrid = columnName;
            fish.StartPosition = FormStartPosition.CenterParent;
            fish.ShowDialog();
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
                Get_List_Kater();
                Get_List_Team();
                Get_List_Trip();
                Get_List_Katalog();
                Get_List_Sklad();
                Get_List_Customer();
                Get_List_Katera_team();
            }
        }

        public void Get_List_Kater()
        {
            DataBaseWorker.LoadData("Kater", dataGridView8);
        }

        public void Get_List_Team()
        {
            DataBaseWorker.LoadData("Teams", dataGridView1);
        }
        public void Get_List_Trip()
        {
            DataBaseWorker.LoadData("Trip", dataGridView2);
        }
        public void Get_List_Katalog()
        {
            DataBaseWorker.LoadData("Katalog", dataGridView3);
        }
        public void Get_List_Sklad()
        {
            DataBaseWorker.LoadData("Sklad", dataGridView4);
        }
        public void Get_List_Customer()
        {
            DataBaseWorker.LoadData("Customer_requests_pred", dataGridView5);
        }

        private void Glav_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        List<int> Pages = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOfKey("Katera_and_teams"))
                return;

            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Katera_and_teams);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Katera);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Teams);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOfKey("Fishing_trip"))
                return;

            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Fishing_trip);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOfKey("Product"))
                return;

            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Product);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Katalog_product);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Sklad_product);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOfKey("Customer_requests"))
                return;

            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Customer_requests);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOfKey("Tax_deductions"))
                return;

            map();
            map();

            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Tax_deductions);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.IndexOfKey("Glavnay"))
                return;

            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Glavnay);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(Pages.Last());
            Pages.RemoveAt(Pages.Count - 1);  
        }

        public void button20_Click(object sender, EventArgs e)
        {
            OpenRedactor("Kater_dob");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView8, "GiveRowsKatera", "Kater_izm");
        }

        private void Glav_Activated(object sender, EventArgs e)
        {
            Get_List_Kater();
            Get_List_Team();
            Get_List_Trip();
            Get_List_Katalog();
            Get_List_Sklad();
            Get_List_Customer();
            Get_List_Katera_team();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Delete(dataGridView8, "Katera", "ID_Kater");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            OpenRedactor("Team_dob");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Delete(dataGridView1, "Fisher_Team", "ID");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            OpenRedactor("Trip_dob");
        }

        private void bunifuButton29_Click(object sender, EventArgs e)
        {
            OpenRedactor("Katalog_dob");
        }

        private void bunifuButton32_Click(object sender, EventArgs e)
        {
            OpenRedactor("Sklad_dob");
        }

        private void bunifuButton35_Click(object sender, EventArgs e)
        {
            OpenRedactor("Customer_dob");
        }

        private void bunifuButton41_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            Delete(dataGridView2, "Fishing_trip", "Id_Fishing_trip");
        }


        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            Delete(dataGridView3, "Product_type", "Id_Product");
        }

        private void bunifuButton30_Click(object sender, EventArgs e)
        {
            Delete(dataGridView4, "Product_storage", "Id");
        }

        private void bunifuButton33_Click(object sender, EventArgs e)
        {
            Delete(dataGridView5, "Customer_requests", "Id_Applecation");
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView1, "GiveRowsTeam", "Team_izm");
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView2, "GiveRowsTrip", "Trip_izm");
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView3, "GiveRowsKatalog", "Katalog_izm");
        }

        private void bunifuButton31_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView4, "GiveRowsSklad", "Sklad_izm");
        }

        private void bunifuButton34_Click(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView5, "GiveRowsCustomer", "Customer_izm");
        }

        private void bunifuButton42_Click_1(object sender, EventArgs e)
        {
            string Kater = dataGridView2.SelectedCells[1].Value.ToString();
            string DataOtp = dataGridView2.SelectedCells[3].Value.ToString();
            string DataPrib = dataGridView2.SelectedCells[4].Value.ToString();

            DataGrid fish = new DataGrid(Role);
            fish.fish1 = 1;
            fish.label2.Text = DataOtp;
            fish.label3.Text = Kater;
            fish.label4.Text = DataPrib;
            DataGridViewSelectedRowCollection Rows = dataGridView2.SelectedRows;
            fish.GiveRowsCaught(Rows);
            fish.index_DataGrid = "Caught_fish";
            fish.StartPosition = FormStartPosition.CenterParent;
            fish.ShowDialog();
        }

        private void bunifuButton43_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Types_of_katers");
        }

        private void bunifuButton44_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Types_of_fish");
        }

        private void bunifuButton46_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Klient");
        }

        private void bunifuButton45_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Locations");
        }

        private void bunifuButton47_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Post");
        }

        private void bunifuButton48_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Fish_quality");
        }

        private void bunifuButton49_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Application.Workbooks.Add(Type.Missing);
            exApp.Application.Columns.ColumnWidth = 25;

            Excel.Range _excelCells = (Excel.Range)exApp.get_Range("A1", "G1").Cells;
            _excelCells.Merge(Type.Missing);
            exApp.Cells[1, 1].Value = "Отчёт по продажам";
            exApp.Cells[1, 1].Font.Size = 14;
            exApp.Cells[1, 1].Font.Italic = true;
            exApp.Cells[1, 1].Font.Bold = true;
            exApp.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 1] = "№";
            exApp.Cells[3, 1].Font.Bold = true;
            exApp.Cells[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 2] = "Клиент";
            exApp.Cells[3, 2].Font.Bold = true;
            exApp.Cells[3, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 3] = "Название товара";
            exApp.Cells[3, 3].Font.Bold = true;
            exApp.Cells[3, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 4] = "Количество";
            exApp.Cells[3, 4].Font.Bold = true;
            exApp.Cells[3, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 5] = "Цена";
            exApp.Cells[3, 5].Font.Bold = true;
            exApp.Cells[3, 5].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 6] = "Сумма";
            exApp.Cells[3, 6].Font.Bold = true;
            exApp.Cells[3, 6].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            exApp.Cells[3, 7] = "Дата продажи";
            exApp.Cells[3, 7].Font.Bold = true;
            exApp.Cells[3, 7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            int j = 0, i = 0;
            for (i = 0; i < dataGridView5.ColumnCount; i++)
            {
                for (j = 0; j < dataGridView5.RowCount; j++)
                {
                    exApp.Cells[j + 4, i + 1] = (dataGridView5[i, j].Value).ToString();
                    exApp.Cells[j + 4, i + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
            }

            exApp.Cells[j + 5, 1].Value = "Сумма продаж: ";
            exApp.Cells[j + 5, 1].Font.Size = 14;
            exApp.Cells[j + 5, 1].Font.Bold = true;
            exApp.Cells[j + 5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            exApp.Cells[j + 5, 2].FormulaLocal = "=СУММ(F4:F9999)";
            exApp.Cells[j + 5, 2].Font.Size = 14;
            exApp.Cells[j + 5, 2].Font.Bold = true;
            exApp.Cells[j + 5, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            exApp.Visible = true;
        }

        IList points1 = new List<PointLatLng>();
        IList points2 = new List<PointLatLng>();

        GMapPolygon polygon01;
        GMapPolygon polygon02;


        private void map()
        {
            gMapControl1.Overlays.Clear();
            while (gMapControl1.Overlays.Count > 0)
            {
                gMapControl1.Overlays.RemoveAt(0);
            }
            string katera1 = null;
            string katera2 = null;

            con = new SqlConnection(DataBaseWorker.GetConnString());

            con.Open();
            string Sql1 = "select count(*) from Trip where [Дата отправления] <= GETDATE() and [Дата прибытия] >= GETDATE() and [Место ловли] = N'Зона 1'";
            SqlCommand com1 = new SqlCommand(Sql1, con);
            SqlDataReader dr1 = com1.ExecuteReader();
            while (dr1.Read())
            {
                katera1 = dr1[0].ToString();
            }
            con.Close();

            con.Open();
            string Sql2 = "select count(*) from Trip where [Дата отправления] <= GETDATE() and [Дата прибытия] >= GETDATE() and [Место ловли] = N'Зона 2'";
            SqlCommand com2 = new SqlCommand(Sql2, con);
            SqlDataReader dr2 = com2.ExecuteReader();
            while (dr2.Read())
            {
                katera2 = dr2[0].ToString();
            }
            con.Close();

            GMapPolygon polygon1 = new GMapPolygon((List<PointLatLng>)points1, "\nРыболовная зона №1 \nАктивных рыболовных суден в зоне: " + katera1);
            GMapPolygon polygon2 = new GMapPolygon((List<PointLatLng>)points2, "\nРыболовная зона №2 \nАктивных рыболовных суден в зоне: " + katera2);
            polygon01 = polygon1;
            polygon02 = polygon2;


            GMapOverlay markers = new GMapOverlay("markers");

            gMapControl1.Zoom = 7;
            gMapControl1.Overlays.Add(markers);
            gMapControl1.Zoom = 8;
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 100;

            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            points1.Add(new PointLatLng(59.9398800678482, 28.6550903320313));
            points1.Add(new PointLatLng(60.2166264735486, 28.5122680664063));
            points1.Add(new PointLatLng(60.136034630691, 29.11376953125));
            points1.Add(new PointLatLng(59.9989861206044, 29.300537109375));
            polygon1.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon1.Stroke = new Pen(Color.Red, 1);
            polygon1.Tag = "Зона ловли 1";

            points2.Add(new PointLatLng(59.7176376177877, 26.1474609375));
            points2.Add(new PointLatLng(60.0648404601045, 25.24658203125));
            points2.Add(new PointLatLng(60.1004567761828, 26.444091796875));
            points2.Add(new PointLatLng(59.6732883683712, 26.817626953125));
            polygon2.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon2.Stroke = new Pen(Color.Red, 1);
            polygon1.Tag = "Зона ловли 2";

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(59.9605104388605, 27.652587890625);

            polyOverlay.Polygons.Add(polygon2);
            polyOverlay.Polygons.Add(polygon1);
            gMapControl1.Overlays.Add(polyOverlay);
            gMapControl1.Refresh();
        }

        GMapOverlay markers = new GMapOverlay("markers");

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

                PointLatLng CheckLoc = new PointLatLng(lat, lng);

                Boolean Poly1 = polygon01.IsInside(CheckLoc);
                Boolean Poly2 = polygon02.IsInside(CheckLoc);

                if (!Poly1 && !Poly2)
                    return;

                PointLatLng point = new PointLatLng(lat, lng);
                Bitmap MarkerImage = (Bitmap)System.Drawing.Image.FromFile("image/icons8-information-24.png");
                GMapMarker marker = new GMarkerGoogle(point, MarkerImage);

                if (Poly1)
                    marker.ToolTipText = polygon01.Name;
                else
                    marker.ToolTipText = polygon02.Name;

                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
                gMapControl1.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;

                int CountMarkers = markers.Markers.Count-1;

                List<int> MarkersPoly1 = new List<int>();
                List<int> MarkersPoly2 = new List<int>();
                int index = -1;
                for (int i = 0; i <= CountMarkers; i++)
                {
                    if (polygon01.IsInside(markers.Markers[i].Position))
                        MarkersPoly1.Add(i);
                    else
                        if (polygon02.IsInside(markers.Markers[i].Position))
                            MarkersPoly2.Add(i);

                    if (MarkersPoly1.Count > 1)
                       index = MarkersPoly1.ElementAt(0);
                    else
                        if (MarkersPoly2.Count > 1)
                            index = MarkersPoly2.ElementAt(0);
                }
                if (index != -1)
                    markers.Markers.RemoveAt(index);

                gMapControl1.Zoom = gMapControl1.Zoom + 1;
                gMapControl1.Zoom = gMapControl1.Zoom - 1;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            (dataGridView8.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox1.Text + "] LIKE '{0}%'", textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            (dataGridView1.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox2.Text + "] LIKE '{0}%'", textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            (dataGridView2.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox3.Text + "] LIKE '{0}%'", textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                if (comboBox4.Text == "Название")
                    (dataGridView3.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox4.Text + "] LIKE '{0}%'", textBox4.Text);
                else
                {
                    string input = textBox4.Text;
                    string digitsOnly = Regex.Replace(input, @"[^\d]", "");
                    textBox4.Text = digitsOnly;
                    textBox4.SelectionStart = textBox4.TextLength;
                    int searchValue;
                    if (int.TryParse(textBox4.Text, out searchValue))
                        (dataGridView3.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox4.Text + "] = {0}", searchValue);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
            {
                if (comboBox5.Text == "Название")
                    (dataGridView4.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox5.Text + "] LIKE '{0}%'", textBox5.Text);
                else
                {
                    string input = textBox5.Text;
                    string digitsOnly = Regex.Replace(input, @"[^\d]", "");
                    textBox5.Text = digitsOnly;
                    textBox5.SelectionStart = textBox5.TextLength;
                    int searchValue;
                    if (int.TryParse(textBox5.Text, out searchValue))
                        (dataGridView4.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox5.Text + "] = {0}", searchValue);
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text != "")
            {
                if (comboBox6.Text == "Название" || comboBox6.Text == "Клиент")
                    (dataGridView5.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox6.Text + "] LIKE '{0}%'", textBox6.Text);
                else
                {
                    string input = textBox6.Text;
                    string digitsOnly = Regex.Replace(input, @"[^\d]", "");
                    textBox6.Text = digitsOnly;
                    textBox6.SelectionStart = textBox6.TextLength;
                    int searchValue;
                    if (int.TryParse(textBox6.Text, out searchValue))
                        (dataGridView5.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox6.Text + "] = {0}", searchValue);
                }
            }
        }

        private void bunifuButton36_Click(object sender, EventArgs e)
        {
            Pages.Add(tabControl1.SelectedIndex);
            tabControl1.SelectTab(Kater_team);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (comboBox7.Text != "")
            {
                (dataGridView6.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox7.Text + "] LIKE '{0}%'", textBox7.Text);
            }
        }
        public void Get_List_Katera_team()
        {
            DataBaseWorker.LoadData("Katera_team_pred", dataGridView6);
        }

        private void bunifuButton50_Click(object sender, EventArgs e)
        {
            OpenRedactor("Katera_team_dob");
        }

        private void bunifuButton38_Click_1(object sender, EventArgs e)
        {
            OpenRedactorIzm(dataGridView6, "GiveRowsKatera_team", "Katera_team_izm");
        }

        private void bunifuButton37_Click(object sender, EventArgs e)
        {
            Delete(dataGridView6, "Katera_team", "Id");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[Дата отправления] = #{0}#", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
        }

        private void bunifuButton52_Click(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Empty;
            textBox3.Text = "";
        }

        private void bunifuButton53_Click(object sender, EventArgs e)
        {
            (dataGridView8.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Empty;
            textBox1.Text = "";
        }

        private void bunifuButton54_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            (dataGridView1.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Empty;
        }

        private void bunifuButton55_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void bunifuButton56_Click(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Empty;
            textBox5.Text = "";
        }

        private void bunifuButton57_Click(object sender, EventArgs e)
        {
            (dataGridView5.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Empty;
            textBox6.Text = "";
        }

        private void bunifuButton58_Click(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Empty;
            textBox7.Text = "";
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Судно" || comboBox1.Text == "Тип")
            {
                textBox1.Visible = true;
                dateTimePicker3.Visible = false;
            }
            else 
            {
                textBox1.Visible = false;
                dateTimePicker3.Visible = true;
                dateTimePicker3.Location = new System.Drawing.Point(758, 44);
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
                (dataGridView8.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox1.Text + "] = #{0}#", dateTimePicker3.Value.ToString("MM/dd/yyyy"));
        }

        private void comboBox3_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Судно" || comboBox3.Text == "Место ловли")
            {
                textBox2.Visible = true;
                dateTimePicker1.Visible = false;
            }
            else
            {
                textBox2.Visible = false;
                dateTimePicker1.Visible = true;
                dateTimePicker1.Location = new System.Drawing.Point(751, 42);
            }
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox3.Text + "] = #{0}#", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
        }

        private void comboBox6_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox6.Text == "Дата продажи")
            {
                textBox6.Visible = false;
                dateTimePicker2.Visible = true;
                dateTimePicker2.Location = new System.Drawing.Point(727, 43);
            }
            else
            {
                textBox6.Visible = true;
                dateTimePicker2.Visible = false;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            (dataGridView5.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox6.Text + "] = #{0}#", dateTimePicker2.Value.ToString("MM/dd/yyyy"));
        }

        private void comboBox7_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox7.Text == "Дата зачисления на судно" || comboBox7.Text == "Дата снятия с судна")
            {
                textBox7.Visible = false;
                dateTimePicker4.Visible = true;
                dateTimePicker4.Location = new System.Drawing.Point(729, 41);
            }
            else
            {
                textBox7.Visible = true;
                dateTimePicker4.Visible = false;
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as System.Data.DataTable).DefaultView.RowFilter = string.Format("[" + comboBox7.Text + "] = #{0}#", dateTimePicker4.Value.ToString("MM/dd/yyyy"));
        }
    }
}