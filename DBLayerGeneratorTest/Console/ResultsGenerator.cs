using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Console
{
    public static class ResultsGenerator
    {
        #region Resuls
        public static void BuildResultClass(string procName, List<SqlParameter> values, ref StringBuilder sb)
        {
            sb.AppendLine($"\tpublic class {procName.Substring(2, procName.Length-2)}Result");
            sb.AppendLine("\t{");
            foreach (SqlParameter value in values)
            {
                string name = value.ParameterName;
                string type = GeneratorHelper.ParamType(value.SqlDbType);
                string size = "";


                if (type.Contains("("))
                {
                    int start = type.IndexOf("(") + 1;
                    int stop = type.IndexOf(")");
                    int length = stop - start;
                    size = type.Substring(start, length);
                }

                string typeStripped = size == "" ? type : type.Replace(size, "").Replace("(", "").Replace(")", "");
                sb.AppendLine(String.Format("\t\tpublic {0} {1} {2}", type, name, @"{get;set;}"));

            }
            sb.AppendLine(String.Format("\t{0}", @"}"));
        }

        public static List<string> GetResults2(string connectionString, string procName)
        {
            List<string> values = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = String.Format(@"SELECT name, system_type_name, is_nullable
FROM sys.dm_exec_describe_first_result_set_for_object
(
  OBJECT_ID('{0}'), 
  NULL
); ", procName);
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            string type = reader["system_type_name"].ToString();
                            string nullable = reader["is_nullable"].ToString();
                            string combined = $"{name}|{type}|{nullable}";


                            values.Add(combined);
                        }
                    }
                }
            }
            return values;
        }

        public static void BuildResultsUsing(ref StringBuilder sb)
        {
            //sb.AppendLine("\tusing System.Collections.Generic;");
            sb.AppendLine("\tusing System;");
        }


        public static string AuditHeader;

        public static string GetFieldsAsString(string connectionString, string procName, List<SqlParameter> parameters)
        {
            string ret = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = $"SET FMTONLY ON EXEC {procName}";
                bool first = true;
                foreach (SqlParameter param in parameters)
                {

                    if (!first)
                    {
                        qry += ", ";
                    }
                    qry += " null";
                    first = false;
                }
                qry += " SET FMTONLY OFF";
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            ret += "|" + reader.GetName(i);
                        }
                    }
                }
            }
            return ret;
        }

        //public static List<string> GetResults(string connectionString, string procName, List<SqlParameter> parameters)
        public static List<SqlParameter> GetResults(string connectionString, string procName, List<SqlParameter> parameters)
        {
            //List<string> values = new List<string>();
            List<SqlParameter> values = new List<SqlParameter>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = $"SET FMTONLY ON EXEC {procName}";
                if (parameters != null)
                {
                    bool first = true;
                    foreach (SqlParameter param in parameters)
                    {

                        if (!first)
                        {
                            qry += ", ";
                        }
                        qry += " null";
                        first = false;
                    }
                }
                qry += " SET FMTONLY OFF";
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        string fields = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields += "|" + reader.GetName(i);
                        }
                        bool isAudit = procName == "spAuditLogUpsert";
                        //string auditHeader = "|GUID|ID|DateTimeCreated|Action|Source|TableGUID|ActionSystemUserGUID";
                        bool isAuditHeader = fields == AuditHeader;
                        while (isAuditHeader && !isAudit)
                        {
                            reader.NextResult();
                            fields = "";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                fields += "|" + reader.GetName(i);
                            }
                            isAuditHeader = fields == AuditHeader;
                        }
                        if (!isAuditHeader || (isAudit && isAuditHeader))
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                /*string name = reader.GetName(i);
                                string type = reader.GetDataTypeName(i).ToString();
                                //string nullable = reader["is_nullable"].ToString();
                                string nullable = "True";
                                string combined = $"{name}|{type}|{nullable}";*/

                                SqlParameter param = new SqlParameter()
                                {
                                    ParameterName = reader.GetName(i),
                                    SqlDbType = GeneratorHelper.ParamType(reader.GetDataTypeName(i).ToString())

                                };
                                //values.Add(combined);
                                values.Add(param);
                            }
                        }
                    }
                }
            }
            return values;
        }



        

        #endregion

    }
}
