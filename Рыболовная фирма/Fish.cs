using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Рыболовная_фирма
{
    public partial class Fish : Form
    {
        public Fish()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        private void Fish_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(DataBaseWorker.GetConnString());
            da = new SqlDataAdapter("select * from Caught_fish_pred", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Caught_fish_pred");
            dataGridView1.DataSource = ds.Tables["Caught_fish_pred"];
            con.Close();
        }

        private void bunifuButton41_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
