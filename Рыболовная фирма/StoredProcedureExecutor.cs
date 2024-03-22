using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Рыболовная_фирма
{
    public class StoredProcedureExecutor
    {
        public void CallStoredProcedure(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(DataBaseWorker.GetConnString()))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
