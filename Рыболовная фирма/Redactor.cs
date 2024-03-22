using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Рыболовная_фирма
{
    public partial class Redactor : Form
    {
        public string index;
        int Role;
        public string Kater { get; set; }
        public string DataOtp { get; set; }
        public string DataPrib { get; set; }
        public Redactor(int Role)
        {
            this.Role = Role;
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(DataBaseWorker.GetConnString());
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        private void Redactor_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            foreach (TabPage tad in tabControl1.TabPages)
            {
                tad.Text = "";
            }

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Name == index)
                {
                    tabControl1.SelectedIndex = i;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string text = textBox1.Text;
                text = text.Trim();
                text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ");

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text) };
                SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.Int) { Value = Convert.ToInt32(comboBox1.SelectedValue) };
                SqlParameter parameter3 = new SqlParameter("@Vodoizmeschenie", SqlDbType.Int) { Value = Convert.ToInt32(textBox3.Text) };
                SqlParameter parameter4 = new SqlParameter("@Date_Sborki", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker13.Value.ToString("dd/MM/yyyy")) };

                executor.CallStoredProcedure("Katers", parameter1, parameter2, parameter3, parameter4);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox11.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                string text2 = textBox10.Text;
                text2 = text2.Trim();
                text2 = System.Text.RegularExpressions.Regex.Replace(text2, @"\s+", " ");

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 90) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@Adress", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text2) };
                SqlParameter parameter3 = new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = Convert.ToString(maskedTextBox3.Text) };
                SqlParameter parameter4 = new SqlParameter("@Post", SqlDbType.Int) { Value = Convert.ToInt32(comboBox3.SelectedValue) };

                executor.CallStoredProcedure("Team", parameter1, parameter2, parameter3, parameter4);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dateTimePicker9.Value >= dateTimePicker10.Value)
            {
                MessageBox.Show("Данные введены не верно. Проверьте даты");
                return;
            }

            try
            {
                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@ID_Kater", SqlDbType.Int) { Value = Convert.ToInt32(comboBox5.SelectedValue) };
                SqlParameter parameter2 = new SqlParameter("@Id_Location", SqlDbType.Int) { Value = Convert.ToInt32(comboBox6.SelectedValue) };
                SqlParameter parameter3 = new SqlParameter("@Data_D", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker9.Value.ToString("dd/MM/yyyy")) };
                SqlParameter parameter4 = new SqlParameter("@Data_R", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker10.Value.ToString("dd/MM/yyyy")) };

                executor.CallStoredProcedure("Fish_Trip", parameter1, parameter2, parameter3, parameter4);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Product_type", SqlDbType.NVarChar, 50) { Value = Convert.ToString(comboBox25.Text) + " " + Convert.ToString(comboBox26.Text) };
                SqlParameter parameter2 = new SqlParameter("@Price", SqlDbType.Int) { Value = Convert.ToInt32(textBox13.Text) };
                SqlParameter parameter3 = new SqlParameter("@Price_opt", SqlDbType.Int) { Value = Convert.ToInt32(textBox69.Text) };

                executor.CallStoredProcedure("Katalog_produkt", parameter1, parameter2, parameter3);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select  Id_Product from Product_storage where Id_Product = " + comboBox12.SelectedValue;
            object Quantity = cmd.ExecuteScalar();
            con.Close();
            if (Quantity == null)
            {
                try
                {
                    StoredProcedureExecutor executor = new StoredProcedureExecutor();

                    SqlParameter parameter1 = new SqlParameter("@Product_type", SqlDbType.Int) { Value = Convert.ToInt32(comboBox9.SelectedValue) };
                    SqlParameter parameter2 = new SqlParameter("@Quantity", SqlDbType.Int) { Value = Convert.ToInt32(textBox18.Text) };

                    executor.CallStoredProcedure("Produck_sklad", parameter1, parameter2);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Данные введены не верно");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "select Quantity from Product_storage where Id_Product = " + Convert.ToInt32(comboBox9.SelectedValue);
                    object Quan = cmd.ExecuteScalar();
                    int qualiti = Convert.ToInt32(textBox18.Text) + Convert.ToInt32(Quan);
                    cmd.CommandText = "select  Id from Product_storage where Id_Product = " + comboBox9.SelectedValue + " and Quantity = " + Convert.ToInt32(Quan);
                    object id = cmd.ExecuteScalar();
                    cmd.CommandText = "update Product_storage set Quantity = " + qualiti + " where  Id = " + Convert.ToInt32(id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select  sum(Quantity) from Product_storage where Id_Product = " + comboBox12.SelectedValue;
                object result = cmd.ExecuteScalar();
                con.Close();
                int sumQuantity = 0;
                if (result != null && result != DBNull.Value)
                {
                    sumQuantity = Convert.ToInt32(result);
                    if (sumQuantity < Convert.ToInt32(textBox21.Text))
                    {
                        MessageBox.Show("На складе нет нужно количества товара. В данный момент на складе хранится " + Convert.ToString(sumQuantity) + "кг данной продукции");
                        return;
                    }
                    else
                    {
                        try
                        {
                            StoredProcedureExecutor executor = new StoredProcedureExecutor();

                            SqlParameter parameter1 = new SqlParameter("@Id_Klienta", SqlDbType.Int) { Value = Convert.ToInt32(comboBox11.SelectedValue) };
                            SqlParameter parameter2 = new SqlParameter("@Product_type", SqlDbType.Int) { Value = Convert.ToInt32(comboBox12.SelectedValue) };
                            SqlParameter parameter3 = new SqlParameter("@Quantity", SqlDbType.Int) { Value = Convert.ToInt32(textBox21.Text) };
                            SqlParameter parameter4 = new SqlParameter("@Amount", SqlDbType.Int) { Value = Convert.ToInt32(textBox20.Text) };
                            SqlParameter parameter5 = new SqlParameter("@Data_p", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker8.Value.ToString("dd/MM/yyyy")) };

                            executor.CallStoredProcedure("Customer", parameter1, parameter2, parameter3, parameter4, parameter5);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Данные введены не верно");
                        }
                    }
                    if (sumQuantity >= Convert.ToInt32(textBox21.Text))
                    {
                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Quantity from Product_storage where Id_Product = " + Convert.ToInt32(comboBox12.SelectedValue);
                        object Quan = cmd.ExecuteScalar();
                        con.Close();
                        int qualiti = Convert.ToInt32(Quan) - Convert.ToInt32(textBox21.Text);
                        object id = null;
                        if (qualiti >= 0)
                        {
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "select  Id from Product_storage where Id_Product = " + comboBox12.SelectedValue + " and Quantity = " + Convert.ToInt32(Quan);
                            id = cmd.ExecuteScalar();
                            cmd.CommandText = "update Product_storage set Quantity = " + qualiti + " where  Id = " + Convert.ToInt32(id);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            this.Close();
                        }
                        if (qualiti == 0)
                        {
                            cmd = new SqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "delete from Product_storage  where Id = " + Convert.ToInt32(id);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            this.Close();
                        }
                    }
                }
                else
                    MessageBox.Show("На складе нет нужно количества товара. В данный момент на складе хранится " + Convert.ToString(sumQuantity) + "кг данной продукции");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker11.Value >= dateTimePicker12.Value)
            {
                MessageBox.Show("Данные введены не верно. Проверьте даты");
                return;
            }            

            try
            {
                string text1 = textBox8.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.Int) { Value = Convert.ToInt32(comboBox2.SelectedValue) };
                SqlParameter parameter3 = new SqlParameter("@Vodoizmeschenie", SqlDbType.Int) { Value = Convert.ToInt32(textBox6.Text) };
                SqlParameter parameter4 = new SqlParameter("@Date_Sborki", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker11.Value.ToString("dd/MM/yyyy")) };
                SqlParameter parameter5 = new SqlParameter("@Date_Spisaniy", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker12.Value.ToString("dd/MM/yyyy")) };
                SqlParameter parameter6 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Katers", parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
                Glav gl = new Glav(Role);
                gl.Get_List_Kater();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }
        DataGridViewSelectedRowCollection Rows;

        public void GiveRowsKatera(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            textBox8.Text = Rows[0].Cells[1].Value.ToString();
            comboBox2.Text = Rows[0].Cells[2].Value.ToString();
            textBox6.Text = Rows[0].Cells[3].Value.ToString();
            dateTimePicker11.Text = Rows[0].Cells[4].Value.ToString();
        }

        public void GiveRowsTeam(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            textBox29.Text = Rows[0].Cells[1].Value.ToString();
            textBox28.Text = Rows[0].Cells[2].Value.ToString();
            comboBox4.Text = Rows[0].Cells[4].Value.ToString();
            maskedTextBox4.Text = Rows[0].Cells[3].Value.ToString();
        }
        public void GiveRowsTrip(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            comboBox8.Text = Rows[0].Cells[1].Value.ToString();
            comboBox7.Text = Rows[0].Cells[2].Value.ToString();
            dateTimePicker15.Text = Rows[0].Cells[3].Value.ToString();
            dateTimePicker14.Text = Rows[0].Cells[4].Value.ToString();
        }
        public void GiveRowsKatalog(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            comboBox28.Text = Rows[0].Cells[1].Value.ToString();
            comboBox27.Text = Rows[0].Cells[1].Value.ToString();
            textBox35.Text = Rows[0].Cells[2].Value.ToString();
            textBox71.Text = Rows[0].Cells[3].Value.ToString();
        }
        public void GiveRowsSklad(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            comboBox10.Text = Rows[0].Cells[1].Value.ToString();
            textBox37.Text = Rows[0].Cells[2].Value.ToString();
        }
        public void GiveRowsCustomer(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            comboBox13.Text = Rows[0].Cells[1].Value.ToString();
            comboBox14.Text = Rows[0].Cells[2].Value.ToString();
            textBox40.Text = Rows[0].Cells[3].Value.ToString();
            textBox39.Text = Rows[0].Cells[4].Value.ToString();
            dateTimePicker7.Text = Rows[0].Cells[6].Value.ToString();
            textBox4.Text = Rows[0].Cells[5].Value.ToString();
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox29.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                string text2 = textBox28.Text;
                text2 = text2.Trim();
                text2 = System.Text.RegularExpressions.Regex.Replace(text2, @"\s+", " ");

                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 90) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@Adress", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text2) };
                SqlParameter parameter3 = new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = Convert.ToString(maskedTextBox4.Text) };
                SqlParameter parameter4 = new SqlParameter("@Post", SqlDbType.Int) { Value = Convert.ToInt32(comboBox4.SelectedValue) };
                SqlParameter parameter5 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Team", parameter1, parameter2, parameter3, parameter4, parameter5);
                con.Close();
                Glav gl = new Glav(Role);
                gl.Get_List_Team();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            if (dateTimePicker15.Value >= dateTimePicker14.Value)
            {
                MessageBox.Show("Данные введены не верно. Проверьте даты");
                return;
            }

            try
            {
                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@ID_Kater", SqlDbType.Int) { Value = Convert.ToInt32(comboBox8.SelectedValue) };
                SqlParameter parameter2 = new SqlParameter("@Id_Location", SqlDbType.Int) { Value = Convert.ToInt32(comboBox7.SelectedValue) };
                SqlParameter parameter3 = new SqlParameter("@Data_D", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker15.Value.ToString("dd/MM/yyyy")) };
                SqlParameter parameter4 = new SqlParameter("@Data_R", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker14.Value.ToString("dd/MM/yyyy")) };
                SqlParameter parameter5 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Fish_Trip", parameter1, parameter2, parameter3, parameter4, parameter5);
                Glav gl = new Glav(Role);
                gl.Get_List_Trip();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            try
            {
                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Product_type", SqlDbType.NVarChar, 50) { Value = Convert.ToString(comboBox28.Text) + " " + Convert.ToString(comboBox27.Text) };
                SqlParameter parameter2 = new SqlParameter("@Price", SqlDbType.Int) { Value = Convert.ToInt32(textBox35.Text) };
                SqlParameter parameter3 = new SqlParameter("@Price_opt", SqlDbType.Int) { Value = Convert.ToInt32(textBox71.Text) };
                SqlParameter parameter4 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Katalog_produkt", parameter1, parameter2, parameter3, parameter4);
                Glav gl = new Glav(Role);
                gl.Get_List_Katalog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            try
            {
                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Product_type", SqlDbType.Int) { Value = Convert.ToInt32(comboBox10.SelectedValue) };
                SqlParameter parameter2 = new SqlParameter("@Quantity", SqlDbType.Int) { Value = Convert.ToInt32(textBox37.Text) };
                SqlParameter parameter3 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Produck_sklad", parameter1, parameter2, parameter3);
                Glav gl = new Glav(Role);
                gl.Get_List_Katalog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select  sum(Quantity) from Product_storage where Id_Product = " + comboBox14.SelectedValue;
                object result = cmd.ExecuteScalar();
                con.Close();
                int sumQuantity = 0;
                if (result != null && result != DBNull.Value)
                {
                    sumQuantity = Convert.ToInt32(result);
                    if (sumQuantity < Convert.ToInt32(textBox40.Text))
                    {
                        MessageBox.Show("На складе нет нужно количества товара. В данный момент на складе хранится " + Convert.ToString(sumQuantity) + "кг данной продукции");
                        return;
                    }
                    else
                    {
                        try
                        {
                            GiveRows(Rows);
                            string id = Rows[0].Cells[0].Value.ToString();

                            StoredProcedureExecutor executor = new StoredProcedureExecutor();

                            SqlParameter parameter1 = new SqlParameter("@Product_type", SqlDbType.Int) { Value = Convert.ToInt32(comboBox14.SelectedValue) };
                            SqlParameter parameter2 = new SqlParameter("@Quantity", SqlDbType.Int) { Value = Convert.ToInt32(textBox40.Text) };
                            SqlParameter parameter3 = new SqlParameter("@Amount", SqlDbType.Int) { Value = Convert.ToInt32(textBox39.Text) };
                            SqlParameter parameter4 = new SqlParameter("@Id_Klienta", SqlDbType.Int) { Value = Convert.ToInt32(comboBox13.SelectedValue) };
                            SqlParameter parameter5 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };
                            SqlParameter parameter6 = new SqlParameter("@Data_p", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker7.Value.ToString("dd/MM/yyyy")) };

                            executor.CallStoredProcedure("up_Customer", parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
                            Glav gl = new Glav(Role);
                            gl.Get_List_Customer();
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Данные введены не верно");
                        }
                    }
                    if (sumQuantity >= Convert.ToInt32(textBox40.Text))
                    {
                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Quantity from Product_storage where Id_Product = " + Convert.ToInt32(comboBox14.SelectedValue);
                        object Quan = cmd.ExecuteScalar();
                        con.Close();
                        int qualiti = Convert.ToInt32(Quan) - Convert.ToInt32(textBox40.Text);
                        object id = null;
                        if (qualiti >= 0)
                        {
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "select  Id from Product_storage where Id_Product = " + comboBox14.SelectedValue + " and Quantity = " + Convert.ToInt32(Quan);
                            id = cmd.ExecuteScalar();
                            cmd.CommandText = "update Product_storage set Quantity = " + qualiti + " where  Id = " + Convert.ToInt32(id);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            this.Close();
                        }
                        if (qualiti == 0)
                        {
                            cmd = new SqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "delete from Product_storage  where Id = " + Convert.ToInt32(id);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            this.Close();
                        }
                    }
                }
                else
                    MessageBox.Show("На складе нет нужно количества товара. В данный момент на складе хранится " + Convert.ToString(sumQuantity) + "кг данной продукции");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void bunifuButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            if (Kater != comboBox15.Text)
            {
                MessageBox.Show("Судно выбрано не верно. Укажите судно: " + Kater);
                return;
            }

            if (dateTimePicker6.Value <= Convert.ToDateTime(DataOtp) || dateTimePicker6.Value >= Convert.ToDateTime(DataPrib))
            {
                MessageBox.Show("Дата указана не верно. Дата должна быть в интревале от " + DataOtp + " до " + DataPrib);
                return;
            }
            try
            {
                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@ID_Kater", SqlDbType.Int) { Value = Convert.ToInt32(comboBox15.SelectedValue) };
                SqlParameter parameter2 = new SqlParameter("@Id_Types_of_fish", SqlDbType.Int) { Value = Convert.ToInt32(comboBox16.SelectedValue) };
                SqlParameter parameter3 = new SqlParameter("@Fish_weight", SqlDbType.Real) { Value = Convert.ToDouble(textBox46.Text) };
                SqlParameter parameter4 = new SqlParameter("@Catch_date", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker6.Value.ToString("dd/MM/yyyy")) };
                SqlParameter parameter5 = new SqlParameter("@Id_quality", SqlDbType.Int) { Value = Convert.ToInt32(comboBox17.SelectedValue) };

                executor.CallStoredProcedure("Caught_fish_dob", parameter1, parameter2, parameter3, parameter4, parameter5);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton15_Click(object sender, EventArgs e)
        {
            if (Kater != comboBox18.Text)
            {
                MessageBox.Show("Судно выбрано не верно. Укажите судно: " + Kater);
                return;
            }

            if (dateTimePicker5.Value <= Convert.ToDateTime(DataOtp) || dateTimePicker5.Value >= Convert.ToDateTime(DataPrib))
            {
                MessageBox.Show("Дата указана не верно. Дата должна быть в интревале от " + DataOtp + " до " + DataPrib);
                return;
            }

            try
            {
                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@ID_Kater", SqlDbType.Int) { Value = Convert.ToInt32(comboBox18.SelectedValue) };
                SqlParameter parameter2 = new SqlParameter("@Id_Types_of_fish", SqlDbType.Int) { Value = Convert.ToInt32(comboBox19.SelectedValue) };
                SqlParameter parameter3 = new SqlParameter("@Fish_weight", SqlDbType.Real) { Value = Convert.ToDouble(textBox52.Text) };
                SqlParameter parameter4 = new SqlParameter("@Catch_date", SqlDbType.DateTime) { Value = Convert.ToDateTime(dateTimePicker5.Value.ToString("dd/MM/yyyy")) };
                SqlParameter parameter5 = new SqlParameter("@Id_quality", SqlDbType.Int) { Value = Convert.ToInt32(comboBox20.SelectedValue) };
                SqlParameter parameter6= new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Caught_fish", parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton16_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox56.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Fish_quality", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };

                executor.CallStoredProcedure("Fish_quality_dob", parameter1);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton17_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox55.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Fish_quality", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Fish_quality", parameter1, parameter2);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton18_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox58.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 90) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@phone", SqlDbType.NVarChar, 20) { Value = Convert.ToString(maskedTextBox1.Text) };
                SqlParameter parameter3 = new SqlParameter("@adress", SqlDbType.NVarChar, 90) { Value = Convert.ToString(textBox5.Text) };

                executor.CallStoredProcedure("Klient_dob", parameter1, parameter2, parameter3);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton19_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox60.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 90) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@phone", SqlDbType.NVarChar, 20) { Value = Convert.ToString(maskedTextBox2.Text) };
                SqlParameter parameter3 = new SqlParameter("@adress", SqlDbType.NVarChar, 90) { Value = Convert.ToString(textBox7.Text) };
                SqlParameter parameter4 = new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Klient", parameter1, parameter2, parameter3, parameter4);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox63.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name_post", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };

                executor.CallStoredProcedure("Post_dob", parameter1);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox64.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name_post", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@Id", SqlDbType.NVarChar, 50) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("Post_dob", parameter1, parameter2);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox65.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Type_name", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };

                executor.CallStoredProcedure("Types_of_fish_dob", parameter1);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox66.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Type_name", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@Id", SqlDbType.NVarChar, 50) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Types_of_fish", parameter1, parameter2);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox67.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name_types", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };

                executor.CallStoredProcedure("Types_of_katers_dob", parameter1);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            try
            {
                string text1 = textBox68.Text;
                text1 = text1.Trim();
                text1 = System.Text.RegularExpressions.Regex.Replace(text1, @"\s+", " ");

                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();

                StoredProcedureExecutor executor = new StoredProcedureExecutor();

                SqlParameter parameter1 = new SqlParameter("@Name_types", SqlDbType.NVarChar, 50) { Value = Convert.ToString(text1) };
                SqlParameter parameter2 = new SqlParameter("@Id", SqlDbType.NVarChar, 50) { Value = Convert.ToInt32(id) };

                executor.CallStoredProcedure("up_Types_of_katers", parameter1, parameter2);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        public void GiveRows(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
        }

        public void GiveRowsCaught(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            comboBox18.Text = Rows[0].Cells[1].Value.ToString();
            comboBox19.Text = Rows[0].Cells[2].Value.ToString();
            textBox52.Text = Rows[0].Cells[3].Value.ToString();
            dateTimePicker7.Text = Rows[0].Cells[4].Value.ToString();
            comboBox20.Text = Rows[0].Cells[5].Value.ToString();
        }
        public void GiveRowsFish_quality(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            textBox55.Text = Rows[0].Cells[1].Value.ToString();
        }
        public void GiveRowsKlient(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            textBox60.Text = Rows[0].Cells[1].Value.ToString();
            maskedTextBox2.Text = Rows[0].Cells[2].Value.ToString();
            textBox7.Text = Rows[0].Cells[3].Value.ToString();
        }
        public void GiveRowsPost(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            textBox64.Text = Rows[0].Cells[1].Value.ToString();
        }
        public void GiveRowsTypes_of_fish(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            textBox66.Text = Rows[0].Cells[1].Value.ToString(); 
        }
        public void GiveRowsTypes_of_katers(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            textBox68.Text = Rows[0].Cells[1].Value.ToString();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Данные введены не верно. Проверьте даты");
                return;
            }

            try
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SET DATEFORMAT dmy; insert into Katera_team (ID_Kater, ID_team, Data_Z, Data_C) values ('" + comboBox21.SelectedValue + "', ' " + comboBox22.SelectedValue + "', ' " + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "', ' " + dateTimePicker2.Value.ToString("dd/MM/yyyy") + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }
        public void GiveRowsKatera_team(DataGridViewSelectedRowCollection Rows)
        {
            this.Rows = Rows;
            comboBox23.Text = Rows[0].Cells[1].Value.ToString();
            comboBox24.Text = Rows[0].Cells[2].Value.ToString();
            dateTimePicker4.Text = Rows[0].Cells[3].Value.ToString();
            dateTimePicker3.Text = Rows[0].Cells[4].Value.ToString();
        }

        private void bunifuButton13_Click(object sender, EventArgs e)
        {
            if (dateTimePicker4.Value > dateTimePicker3.Value)
            {
                MessageBox.Show("Данные введены не верно. Проверьте даты");
                return;
            }

            try
            {
                GiveRows(Rows);
                string id = Rows[0].Cells[0].Value.ToString();
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SET DATEFORMAT dmy; update Katera_team set ID_Kater ='" + comboBox23.SelectedValue + "', ID_team ='" + comboBox24.SelectedValue + "', Data_Z='" + dateTimePicker4.Value.ToString("dd/MM/yyyy") + "', Data_C='" + dateTimePicker3.Value.ToString("dd/MM/yyyy") + "' where Id= '" + Convert.ToInt32(id) + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные введены не верно");
            }
        }

        private void OpenGiveRows(string columnName)
        {
            DataGrid fish = new DataGrid(Role);
            fish.index_DataGrid = columnName;
            fish.StartPosition = FormStartPosition.CenterParent;
            fish.ShowDialog();
        }

        private void bunifuButton20_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Types_of_katers");
        }

        private void bunifuButton29_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Post");
        }

        private void bunifuButton33_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Klient");
        }

        private void bunifuButton35_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Types_of_fish");
        }

        private void bunifuButton36_Click(object sender, EventArgs e)
        {
            OpenGiveRows("Fish_quality");
        }

        public void FillComboBoxes(string query, string displayMember, string valueMember, params System.Windows.Forms.ComboBox[] comboBoxes)
        {
            using (SqlConnection con = new SqlConnection(DataBaseWorker.GetConnString()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (System.Windows.Forms.ComboBox comboBox in comboBoxes)
                {
                    comboBox.DataSource = table;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                }
            }
        }

        public void comboboxTypeKater()
        {
            string query = "SELECT Id_types, Name_types FROM Types_of_katers";
            FillComboBoxes(query, "Name_types", "Id_types", comboBox1, comboBox2);
        }
        public void comboboxPost()
        {
            string query = "SELECT Id_Post, Name_post FROM Post";
            FillComboBoxes(query, "Name_post", "Id_Post", comboBox3, comboBox4);
        }
        public void comboboxKater()
        {
            string query = "SELECT ID_Kater, Name FROM Katera";
            FillComboBoxes(query, "Name", "ID_Kater", comboBox5, comboBox8, comboBox18, comboBox21, comboBox23, comboBox15);
        }
        public void comboboxLocation()
        {
            string query = "SELECT Id_Location, Name_Location FROM Locations";
            FillComboBoxes(query, "Name_Location", "Id_Location", comboBox6, comboBox7);
        }
        public void comboboxTovar()
        {
            string query = "SELECT Id_Product, Product_type FROM Product_type";
            FillComboBoxes(query, "Product_type", "Id_Product", comboBox9, comboBox10, comboBox12, comboBox14);
        }
        public void comboboxKlient()
        {
            string query = "SELECT Id_Klienta, Name FROM Klient";
            FillComboBoxes(query, "Name", "Id_Klienta", comboBox11, comboBox13);
        }
        public void comboboxTypeFish()
        {
            string query = "SELECT Id_Types_of_fish, Type_name FROM Types_of_fish";
            FillComboBoxes(query, "Type_name", "Id_Types_of_fish", comboBox16, comboBox19, comboBox25, comboBox28);
        }
        public void comboboxQualityFish()
        {
            string query = "SELECT Id_quality, Fish_quality FROM Fish_quality";
            FillComboBoxes(query, "Fish_quality", "Id_quality", comboBox17, comboBox20);
        }
        public void comboboxTeam()
        {
            string query = "SELECT ID, Name FROM Fisher_team";
            FillComboBoxes(query, "Name", "ID", comboBox22, comboBox24);
        }

        int d = 0;
        public void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox12.Focused == true && d == 0)
            {
                comboboxTovar();
                d = 1;
            }
            if (textBox21.Text != "")
            {
                try
                {
                    cmd = new SqlCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "select Price from Product_type where Id_Product = '" + Convert.ToInt32(comboBox12.SelectedValue) + "'";
                    if (!string.IsNullOrEmpty(textBox21.Text))
                    {
                        int quantity;
                        if (int.TryParse(textBox21.Text, out quantity) && quantity >= 20)
                        {
                            cmd.CommandText = "select Price_opt from Product_type where Id_Product = '" + Convert.ToInt32(comboBox12.SelectedValue) + "'";
                        }
                    }
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int price = (int)result;
                        textBox20.Text = Convert.ToString(price);
                    }
                    con.Close();
                    if (decimal.TryParse(textBox21.Text, out decimal value1) &&
                        decimal.TryParse(textBox20.Text, out decimal value2))
                    {
                        decimal result1 = value1 * value2;
                        textBox2.Text = result1.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                textBox20.Text = "";
                textBox2.Text = "";
            }
        }
          int i = 0;
        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox40.Text != "")
            {
                if (i == 3)
                {
                    try
                    {
                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Price from Product_type where Id_Product = '" + Convert.ToInt32(comboBox14.SelectedValue) + "'";
                        if (!string.IsNullOrEmpty(textBox40.Text))
                        {
                            int quantity;
                            if (int.TryParse(textBox40.Text, out quantity) && quantity >= 20)
                            {
                                cmd.CommandText = "select Price_opt from Product_type where Id_Product = '" + Convert.ToInt32(comboBox14.SelectedValue) + "'";
                            }
                        }
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int price = (int)result;
                            textBox39.Text = Convert.ToString(price);
                        }
                        con.Close();
                        if (decimal.TryParse(textBox40.Text, out decimal value1) &&
                            decimal.TryParse(textBox39.Text, out decimal value2))
                        {
                            decimal result1 = value1 * value2;
                            textBox4.Text = result1.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
                else i++;
            }
            else
            {
                textBox39.Text = "";
                textBox4.Text = "";
            }
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboboxTypeKater();
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            comboboxPost();
        }

        private void comboBox5_DropDown(object sender, EventArgs e)
        {
            comboboxKater();
        }

        private void comboBox6_DropDown(object sender, EventArgs e)
        {
            comboboxLocation();
        }

        private void comboBox25_DropDown(object sender, EventArgs e)
        {
            comboboxTypeFish();
        }

        private void comboBox9_DropDown(object sender, EventArgs e)
        {
            comboboxTovar();
        }

        private void comboBox11_DropDown(object sender, EventArgs e)
        {
            comboboxKlient();
        }

        private void comboBox17_DropDown(object sender, EventArgs e)
        {
            comboboxQualityFish();
        }

        private void comboBox22_DropDown(object sender, EventArgs e)
        {
            comboboxTeam();
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.AutoPopDelay = 20000;
            string text2 = "Цена расчитывается в зависимости от выбранного товара и его количества.\nРозничная цена при продаже до 20 кг \nа оптовая от 20кг";
            toolTip.SetToolTip(button2, text2);
            toolTip.SetToolTip(button4, text2);
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.AutoPopDelay = 20000;
            string text1 = "Судно должно совпадать с тем судном которое отправилоcь в рейс.\nДата вылова должна лежать в диапозоне даты оправления \nи даты прибытия этого рейса";
            toolTip.SetToolTip(button3, text1);
            toolTip.SetToolTip(button1, text1);
        }
    }
}