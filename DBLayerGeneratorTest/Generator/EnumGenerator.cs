using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class EnumGenerator
    {
        public static List<POCO> GetTables(string connectionString)
        {
            List<POCO> ret = new List<POCO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = "SELECT * FROM sys.tables WHERE is_ms_shipped = 0 And name like 'LU%'";

                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = $"{reader["name"].ToString()}";
                            POCO vw = new POCO
                            {
                                Name = name
                            };
                            ret.Add(vw);
                        }
                    }
                }
            }
            return ret;

        }

        public static void EnumsPre(ref StringBuilder sb)
        {
            sb.AppendLine("using AppointmentLibrary.Parameters;");
            sb.AppendLine("using AppointmentLibrary.ProcResults;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Reflection;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("");
            sb.AppendLine("namespace ApplicationClient.Enums");
            sb.AppendLine("{");
            sb.AppendLine("     class EnumGUID : Attribute");
            sb.AppendLine("     {");
            sb.AppendLine("         public Guid Guid;");
            sb.AppendLine("     ");
            sb.AppendLine("         public EnumGUID(string guid)");
            sb.AppendLine("         {");
            sb.AppendLine("             Guid = new Guid(guid);");
            sb.AppendLine("         }");
            sb.AppendLine("     }");
            sb.AppendLine("");
            sb.AppendLine("     public static class EnumHelper");
            sb.AppendLine("     {");
            sb.AppendLine("         public static Guid GUID(this Enum e)");
            sb.AppendLine("         {");
            sb.AppendLine("             Type type = e.GetType();");
            sb.AppendLine("     ");
            sb.AppendLine("             MemberInfo[] memInfo = type.GetMember(e.ToString());");
            sb.AppendLine("     ");
            sb.AppendLine("             if (memInfo != null && memInfo.Length > 0)");
            sb.AppendLine("             {");
            sb.AppendLine("                 object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumGUID), false);");
            sb.AppendLine("                 if (attrs != null && attrs.Length > 0)");
            sb.AppendLine("                     return ((EnumGUID)attrs[0]).Guid;");
            sb.AppendLine("             }");
            sb.AppendLine("     ");
            sb.AppendLine(@"                throw new ArgumentException(""Enum "" + e.ToString() + "" has no EnumGUID defined!""); ");
            sb.AppendLine("         }");
            sb.AppendLine("     }");
        }

        public static void EnumsPost(ref StringBuilder sb)
        {
            sb.AppendLine("}");
        }

        public static void BuildEnums(string connectionString, ref StringBuilder sb)
        {
            List<POCO> tables = GetTables(connectionString);
            foreach (POCO table in tables)
            {
                BuildEnum(table.Name, connectionString, ref sb);
            }
        }

        public static void BuildEnum(string tableName, string connectionString, ref StringBuilder sb)
        {
            Dictionary<string, Guid> values = GetValues(tableName, connectionString);
            sb.AppendLine($"    public enum {tableName}");
            sb.AppendLine("    {");
            int progress = 0;
            foreach (var key in values)
            {
                bool last = progress == values.Count ? true : false;
                progress++;
                sb.AppendLine($@"        [EnumGUID(""{key.Value.ToString()}"")]");
                sb.Append($"        {key.Key} = {progress.ToString()}");
                if (last)
                {
                    sb.AppendLine();
                }
                else
                {
                    sb.AppendLine(",");
                }
            }
            sb.AppendLine("     }");
            /*
            enum Project
            {
                [EnumGUID("2ED3164-BB48-499B-86C4-A2B1114BF1")]
                Cleanup = 1,
                [EnumGUID("39D31D4-28EC-4832-827B-A11129EB2")]
                Maintenance = 2
                // and so forth, notice the integer value isn't supposed to be used, 
                // it's merely there because not assigning any value is a performance overhead.
            }
            */
        }

        public static Dictionary<string, Guid> GetValues(string tableName, string connectionString)
        {
            Dictionary<string, Guid> ret = new Dictionary<string, Guid>();
            string fieldName = tableName.Substring(2, tableName.Length - 2);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = $"SELECT GUID, {fieldName} FROM {tableName}";

                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid guid = new Guid(reader["GUID"].ToString());
                            string val = reader[fieldName].ToString();
                            ret.Add(val, guid);
                        }
                    }
                }
            }
            return ret;
        }
                        
    }
}
