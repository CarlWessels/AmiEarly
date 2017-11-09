using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public static class GeneratorHelper
    {
        public static void BuildPOCO(POCO poco, string connectionString, ref StringBuilder sb, int baseIdnt)
        {
            BuildPOCO(poco, connectionString, ref sb, baseIdnt, "");
        }

        public static void BuildParametersPre(ref StringBuilder sb)
        {
            sb.AppendLine("\tpublic interface IParameter");
            sb.AppendLine("\t{");
            sb.AppendLine("\t}");
        }

        public static void BuildPOCO(POCO poco, string connectionString, ref StringBuilder sb, int baseIdnt, string prefix)
        {
            string baseIndent = Indent(baseIdnt);
            string indentOne = Indent(baseIdnt + 1);
            string indentTwo = Indent(baseIdnt + 2);
            string indentThree = Indent(baseIdnt + 3);
            string indentFour = Indent(baseIdnt + 4);
            string indentFive = Indent(baseIdnt + 5);
            string indentSeven = Indent(baseIdnt + 7);

            sb.AppendLine(baseIndent + $"public partial class {prefix}{poco.Name}");
            sb.AppendLine(baseIndent + "{");


            List<SqlParameter> parameters = GetPOCOOutput(connectionString, poco);
            foreach (SqlParameter param in parameters)
            {
                sb.AppendLine(indentOne + $"public string {param.ParameterName}" + " {get;set;}");
            }

            sb.AppendLine(baseIndent + "}");
        }

        public static void BuildNamespacePre(string nameSpace, ref StringBuilder sb)
        {
            sb.AppendLine(String.Format("namespace {0}", nameSpace));
            sb.AppendLine("{");
        }

        public static void BuildNamespacePost(ref StringBuilder sb)
        {
            sb.AppendLine("}");
        }

        public static List<SqlParameter> GetPOCOOutput(string connectionString, POCO poco)
        {
            List<SqlParameter> values = new List<SqlParameter>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = $"SET FMTONLY ON SELECT * FROM {poco.Name} SET FMTONLY OFF";
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            SqlDbType type = ParamType(reader.GetDataTypeName(i));
                            SqlParameter param = new SqlParameter()
                            {
                                ParameterName = name,
                                SqlDbType = type
                            };
                            values.Add(param);
                        }
                    }
                }
            }
            return values;
        }


        public static List<string> GetSPs(string connectionString)
        {
            List<string> ret = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = @"SELECT * FROM fnGetStoredProcedures()";
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["SPECIFIC_Name"].ToString();
                            ret.Add(name);
                        }
                    }
                }
            }
            return ret;
        }

        public static SqlDbType ParamType(string name)
        {
            SqlDbType ret = new SqlDbType();
            switch (name.ToUpper())
            {
                case "VARCHAR":
                case "NTEXT":
                case "XML":
                case "NVARCHAR":
                    ret = SqlDbType.NVarChar;
                    break;
                case "INT":
                    ret = SqlDbType.Int;
                    break;
                case "BIT":
                    ret = SqlDbType.Bit;
                    break;
                case "MONEY":
                    ret = SqlDbType.Money;
                    break;
                case "UNIQUEIDENTIFIER":
                    ret = SqlDbType.UniqueIdentifier;
                    break;
                case "DATETIME":
                    ret = SqlDbType.DateTime;
                    break;
                case "TIME":
                    ret = SqlDbType.Time;
                    break;
                case "BINARY":
                case "VARBINARY":
                    ret = SqlDbType.Binary;
                    break;
                case "DATE":
                    ret = SqlDbType.Date;
                    break;
                default:
                    throw new Exception("No type found");

            }
            return ret;
        }

        public static List<SqlParameter> GetParameters(string procName, string connectionString)
        {
            List<SqlParameter> ret = new List<SqlParameter>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = String.Format(@"SELECT * FROM dbo.fnGetStoredProcedureParameters('{0}')", procName);

                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string paramName = reader["Parameter_Name"].ToString();
                            string type = reader["Type"].ToString();
                            string length = reader["Length"].ToString();
                            bool nullable = reader["Nullable"].ToString() == "True" ? true : false;
                            bool isOutput = reader["IsOutput"].ToString() == "True" ? true : false;
                            int size = int.Parse(length);

                            System.Data.SqlDbType sqlDbType = new System.Data.SqlDbType();
                            switch (type.ToUpper())
                            {
                                case "INT":
                                    sqlDbType = System.Data.SqlDbType.Int;
                                    break;
                                case "NVARCHAR":
                                case "VARCHAR":
                                    sqlDbType = System.Data.SqlDbType.VarChar;
                                    break;
                                case "MONEY":
                                    sqlDbType = System.Data.SqlDbType.Money;
                                    break;
                                case "BIT":
                                    sqlDbType = System.Data.SqlDbType.Bit;
                                    break;
                                case "UNIQUEIDENTIFIER":
                                    sqlDbType = SqlDbType.UniqueIdentifier;
                                    break;
                                case "DATETIME":
                                    sqlDbType = SqlDbType.DateTime;
                                    break;
                                case "TIME":
                                    sqlDbType = SqlDbType.Time;
                                    break;
                                case "VARBINARY":
                                    sqlDbType = SqlDbType.VarBinary;
                                    break;
                                case "DATE":
                                    sqlDbType = SqlDbType.Date;
                                    break;
                                default:
                                    throw new Exception("No type found");
                            }

                            SqlParameter param = new SqlParameter(paramName, sqlDbType);
                            param.Size = size;
                            param.IsNullable = nullable;
                            param.Direction = isOutput ? ParameterDirection.InputOutput : ParameterDirection.Input;
                            ret.Add(param);
                        }
                    }
                }
            }
            return ret;
        }

        public static string ParamType(System.Data.SqlDbType type)
        {
            string paramType = "";
            switch (type)
            {
                case System.Data.SqlDbType.Int:
                    paramType = "int";
                    break;
                case System.Data.SqlDbType.NVarChar:
                case System.Data.SqlDbType.VarChar:
                    paramType = "string";
                    break;
                case System.Data.SqlDbType.Bit:
                    paramType = "bool";
                    break;
                case System.Data.SqlDbType.Money:
                    paramType = "decimal";
                    break;
                case System.Data.SqlDbType.UniqueIdentifier:
                    paramType = "Guid";
                    break;
                case System.Data.SqlDbType.DateTime:
                case SqlDbType.Date:
                    paramType = "DateTime";
                    break;
                case System.Data.SqlDbType.Time:
                    paramType = "TimeSpan";
                    break;
                case System.Data.SqlDbType.Binary:
                case System.Data.SqlDbType.VarBinary:
                    paramType = "Byte[]";
                    break;
                default:
                    throw new Exception("No type found");
            }
            
            return paramType;
        }

        public static string Indent(int amount)
        {
            string ret = "";
            for (int i = 0; i < amount; i++)
            {
                ret = ret + "\t";
            }

            return ret;
        }

        public static void AddHeader(StringBuilder sb)
        {
            sb.AppendLine("//------------------------------------------------------------------------------");
            sb.AppendLine("// <auto-generated>");
            sb.AppendLine("//     This code was generated by a tool.");
            sb.AppendLine("//");
            sb.AppendLine("//     Changes to this file may cause incorrect behavior and will be lost if");
            sb.AppendLine("//     the code is regenerated.");
            sb.AppendLine("// </auto-generated>");
            sb.AppendLine("//------------------------------------------------------------------------------");
        }

    }

    public class POCO
    {
        public string Name { get; set; }

        public List<SqlParameter> Fields { get; set; }
    }

    

    


}
