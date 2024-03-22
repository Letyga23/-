using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Рыболовная_фирма
{
    public class DataBaseWorker
    {

        public static string GetConnString()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FishingCompany.mdf;Integrated Security=True;Connect Timeout=30";
        }

        public static void LoadData(string tableName, DataGridView dataGridView)
        {
            SqlConnection con = new SqlConnection(GetConnString());
            SqlDataAdapter da = new SqlDataAdapter($"select * from {tableName}", con);
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds, tableName);
            dataGridView.DataSource = ds.Tables[tableName];
            con.Close();
        }
    }
}
