using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Рыболовная_фирма
{
    public partial class Vhod : Form
    {     
        public Vhod()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(DataBaseWorker.GetConnString());
            conn.Open();
            string Sql = "select Roll from Login where Login = '" + textBox1.Text + "'" + " and Password = '" + textBox2.Text + "'";

            SqlCommand com = new SqlCommand(Sql, conn);
            SqlDataReader reader = com.ExecuteReader();

            String Roll = null;

            while (reader.Read()) 
            {
                Roll = reader[0].ToString();
            }
            conn.Close();

            if (Roll != null)
            {
                int Role = Convert.ToInt32(Roll);
                Glav gl = new Glav(Role);
                this.Hide();
                gl.Show();
            }
            else
            {
                MessageBox.Show("Пароль или логин введены не верно!");
            }
            
        }

        private void bunifuButton41_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Vhod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void Vhod_Load(object sender, EventArgs e)
        {

        }
    }
}