using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Console
{
    public static class ViewsGenerator
    {
        public static List<POCO> GetViews(string connectionString)
        {
            List<POCO> ret = new List<POCO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = "SELECT * FROM sys.views WHERE is_ms_shipped = 0";

                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["name"].ToString();
                            POCO vw = new POCO();
                            vw.Name = name;
                            ret.Add(vw);
                        }
                    }
                }
            }
            return ret;
        }

    }
}
