using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=tcp:carlwessels.database.windows.net,1433;Initial Catalog=TestDb;Persist Security Info=False;User ID=carlwessels;Password=p@551234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            List<SqlParameter> parameters = GetParameters("spAuditLogUpsert", connectionString);
            auditHeader = GetFieldsAsString(connectionString, "spAuditLogUpsert", parameters);

            bool build = true;

            if (build)
            {
                DoBuild(connectionString);
            }
        }

        public static void DoBuild(string connectionString)
        {
            StringBuilder resultsSb = new StringBuilder();
            StringBuilder parametersSb = new StringBuilder();
            StringBuilder callsSb = new StringBuilder();
            string nameSpace = "AppointmentLibrary";
            BuildNamespacePre(nameSpace, ref resultsSb);
            BuildNamespacePre(nameSpace, ref parametersSb);
            BuildNamespacePre(nameSpace, ref callsSb);

            BuildResultsUsing(ref resultsSb);
            BuildResultsUsing(ref parametersSb);
            BuildCallsUsing(ref callsSb);

            BuildParametersPre(ref parametersSb);
            BuildCallClassPre(ref callsSb);

            List<string> sps = GetSPs(connectionString);

            foreach (string sp in sps)
            {

                List<SqlParameter> parameters = GetParameters(sp, connectionString);
                List<SqlParameter> values = GetResults(connectionString, sp, parameters);
                //List<string> results = GetResults(connectionString, sp, parameters);
                BuildResultClass(sp, values, ref resultsSb);
                BuildParametersClass(sp, parameters, ref parametersSb);
                //BuildCallWithClass(sp, parameters, results, ref callsSb, 1);
                BuildCalls(sp, parameters, values, ref callsSb, 1);
                //File.WriteAllText(@"test.txt",resultsSb.ToString());
            }
            BuildCallClassPost(ref callsSb);

            BuildNamespacePost(ref resultsSb);
            BuildNamespacePost(ref parametersSb);
            BuildNamespacePost(ref callsSb);

            StringBuilder vwClass = new StringBuilder();
            List<View> views = GetViews(connectionString);

            BuildNamespacePre(nameSpace, ref vwClass);

            foreach (View vw in views)
            {
                BuildView(vw,connectionString, ref vwClass, 1);
            }

            BuildNamespacePost(ref vwClass);

            File.WriteAllText("GeneratedClass.cs", resultsSb.ToString());
            File.WriteAllText("GeneratedParameters.cs", parametersSb.ToString());
            File.WriteAllText("GeneratedCalls.cs", callsSb.ToString());
            File.WriteAllText("GeneratedViews.cs", vwClass.ToString());
        }

        #region Namespaces
        public static void BuildNamespacePre(string nameSpace, ref StringBuilder sb)
        {
            sb.AppendLine(String.Format("namespace {0}", nameSpace));
            sb.AppendLine("{");
        }

        public static void BuildNamespacePost(ref StringBuilder sb)
        {
            sb.AppendLine("}");
        }

        #endregion

        #region Calls
        public static void BuildCallsUsing(ref StringBuilder sb)
        {
            //sb.AppendLine("\tusing System.Collections.Generic;");
            sb.AppendLine("\tusing System.Data.SqlClient;");
        }

        public static void BuildResultsUsing(ref StringBuilder sb)
        {
            //sb.AppendLine("\tusing System.Collections.Generic;");
            sb.AppendLine("\tusing System;");
        }


        public static void BuildParametersPre(ref StringBuilder sb)
        {
            sb.AppendLine("\tpublic interface IParameter");
            sb.AppendLine("\t{");
            sb.AppendLine("\t}");
        }
        public static void BuildCallClassPre(ref StringBuilder sb)
        {
            sb.AppendLine("using System;");
            sb.AppendLine("\tpublic static class Calls");
            sb.AppendLine("\t{");
        }

        public static void BuildCallClassPost(ref StringBuilder sb)
        {
            sb.AppendLine("\t}");
        }

        public static void BuildCalls(string procName, List<SqlParameter> parameters, List<SqlParameter> results, ref StringBuilder sb, int baseIndent)
        {
            BuildCallWithParameters(procName, parameters, ref sb, baseIndent);
            BuildCallWithClass(procName, parameters, results, ref sb, baseIndent);
        }
        public static void BuildCallWithParameters(string procname, List<SqlParameter> parameters, ref StringBuilder sb, int baseIndent)
        {
            string callName = String.Format("{0}Call", procname);
            string parameterName = procname + "Parameters";
            string returnName = procname + "Result";

            string indentTwo = Indent(baseIndent + 2);
            string indentThree = Indent(baseIndent + 3);
            sb.Append(String.Format(indentTwo + $"public static {returnName} {callName}") + "(");
            StringBuilder parametersSb = new StringBuilder();
            bool first = true;
            foreach (SqlParameter param in parameters)
            {
                string type = ParamType(param.SqlDbType);
                if (param.IsNullable && type != "string")
                {
                    type = type + "?";
                }
                string name = param.ParameterName;
                string nameStripped = name.Replace("@", "");
                if (first)
                {
                }
                else
                {
                    sb.Append(", ");
                }
                sb.Append(String.Format("{0} {1}", type, nameStripped));
                parametersSb.AppendLine(indentThree + String.Format("parameters.{0} = {0};", nameStripped));

                first = false;
            }
            sb.AppendLine(", string connectionString)");
            sb.AppendLine(indentTwo + "{");
            sb.AppendLine(indentThree + String.Format("{0} parameters = new {0}();", parameterName));
            sb.AppendLine(parametersSb.ToString());
            sb.AppendLine(indentThree + String.Format("return {0} (parameters, connectionString);", callName));
            sb.AppendLine(indentTwo + "}");
        }
        public static void BuildCallWithClass(string procname, List<SqlParameter> parameters, List<SqlParameter> results, ref StringBuilder sb, int baseIndent)
        {
            string indentTwo = Indent(baseIndent + 2);
            string indentThree = Indent(baseIndent + 3);
            string indentFour = Indent(baseIndent + 4);
            string indentFive = Indent(baseIndent + 5);
            string indentSeven = Indent(baseIndent + 7);

            string returnName = procname + "Result";
            StringBuilder callLine = new StringBuilder(String.Format(String.Format(@"string qry = ""EXEC {0} ", procname)));
            bool first = true;
            foreach (SqlParameter param in parameters)
            {
                if (first)
                {
                }
                else
                {
                    callLine.Append(", ");
                }
                callLine.Append(String.Format($"{param.ParameterName} = {param.ParameterName}"));
                first = false;
            }

            callLine.AppendLine(@""";");


            sb.Append(String.Format(indentTwo + "public static {0} {1}Call ", returnName, procname) + "(");
            sb.Append(String.Format("{0}Parameters parameters", procname));

            sb.Append(String.Format(", string connectionString"));

            sb.AppendLine(")");
            sb.AppendLine(indentTwo + "{");
            sb.AppendLine(indentThree + String.Format("{0} ret = new {0}();", returnName));
            sb.AppendLine(indentThree + @"using (SqlConnection conn = new SqlConnection(connectionString))");
            sb.AppendLine(indentThree + @"{");
            sb.AppendLine(indentThree + @"conn.Open();");

            sb.AppendLine(indentFour + callLine.ToString());

            sb.AppendLine(indentFour + @"using (SqlCommand cmd = new SqlCommand(qry, conn))");
            sb.AppendLine(indentFour + @"{");
            foreach (SqlParameter param in parameters)
            {
                string paramName = param.ParameterName;
                string paramNameStripped = paramName.Replace("@", "");
                string parameterLine;
                if (param.IsNullable)
                {
                    parameterLine = @"cmd.Parameters.Add(new SqlParameter(""{0}"", parameters.{1} == null ? (object)DBNull.Value :  parameters.{1}));";
                }
                else
                {
                    parameterLine = @"cmd.Parameters.Add(new SqlParameter(""{0}"", parameters.{1}));";
                }
                sb.AppendLine(indentFive + string.Format(parameterLine, paramName, paramNameStripped));
            }

            sb.AppendLine(indentThree + @"        using (SqlDataReader reader = cmd.ExecuteReader())");
            sb.AppendLine(indentThree + @"        {");
            sb.AppendLine(indentThree + @"            while (reader.Read())");
            sb.AppendLine(indentThree + @"            { ");
            foreach (SqlParameter result in results)
            {
                /*List<string> split = result.Split('|').ToList();
                string name = split[0];
                string type = split[1];
                bool nullable = split[2] == "True" ? true : false;*/
                string name = result.ParameterName;
                string type = ParamType(result.SqlDbType);
                bool nullable = result.IsNullable;
                string size = "";


                if (type.Contains("("))
                {
                    int start = type.IndexOf("(") + 1;
                    int stop = type.IndexOf(")");
                    int length = stop - start;
                    size = type.Substring(start, length);
                }

                string typeStripped = size == "" ? type : type.Replace(size, "").Replace("(", "").Replace(")", "");
                string resultLine = "";
                switch (typeStripped.ToUpper())
                {
                    case "STRING":
                        resultLine = (String.Format(@"ret.{0} = reader[""{0}""].ToString();", name));
                        break;
                    case "INT":
                        resultLine = (String.Format(@"ret.{0} = int.Parse(reader[""{0}""].ToString());", name));
                        break;
                    case "BOOL":
                        resultLine = (String.Format(@"ret.{0} = (bool)reader[""{0}""];", name));
                        break;
                    case "DECIMAL":
                        resultLine = (String.Format(@"ret.{0} = decimal.Parse(reader[""{0}""].ToString());", name));
                        break;
                    case "GUID":
                        resultLine = (String.Format(@"ret.{0} = new Guid(reader[""{0}""].ToString());", name));
                        break;
                    case "DATETIME":
                        //if (nullable)
                        {
                            resultLine = $@"if (String.IsNullOrWhiteSpace(reader[""{name}""].ToString()))" + Environment.NewLine;
                            resultLine += indentSeven + @"{" + Environment.NewLine;
                            resultLine += indentSeven + $@"    //ret.{name} = null;" + Environment.NewLine;
                            resultLine += indentSeven + @"}" + Environment.NewLine;
                            resultLine += indentSeven + $@"else" + Environment.NewLine;
                            resultLine += indentSeven + @"{" + Environment.NewLine;
                            resultLine += indentSeven + $@"    ret.{name} = DateTime.Parse(reader[""{name}""].ToString());" + Environment.NewLine;
                            resultLine += indentSeven + @"}";
                        }
                        /*else
                        {
                            resultLine = (String.Format(@"ret.{0} = DateTime.Parse(reader[""{0}""].ToString());", name));
                        }*/
                        break;
                    case "TIMESPAN":
                        resultLine = (String.Format(@"ret.{0} = TimeSpan.Parse(reader[""{0}""].ToString());", name));
                        break;

                    default:
                        throw new Exception("No type found");


                }
                sb.AppendLine(indentSeven + resultLine);


            }
            sb.AppendLine(indentThree + @"            }");
            sb.AppendLine(indentThree + @"        }");
            sb.AppendLine(indentThree + @"    }");
            sb.AppendLine(indentThree + @"}");
            sb.AppendLine(indentThree + @"return ret;");

            sb.AppendLine(indentTwo + "}");
        }

        #endregion

        #region Resuls
        public static void BuildResultClass(string procName, List<SqlParameter> values, ref StringBuilder sb)
        {
            sb.AppendLine(String.Format("\tpublic class {0}Result", procName));
            sb.AppendLine("\t{");
            foreach (SqlParameter value in values)
            {
                /*List<string> split = value.Split('|').ToList();
                string name = split[0];
                string type = split[1];
                //bool nullable = split[2] == "True" ? true : false;
                string size = "";*/
                string name = value.ParameterName;
                string type = ParamType(value.SqlDbType);
                string size = "";


                if (type.Contains("("))
                {
                    int start = type.IndexOf("(") + 1;
                    int stop = type.IndexOf(")");
                    int length = stop - start;
                    size = type.Substring(start, length);
                }

                string typeStripped = size == "" ? type : type.Replace(size, "").Replace("(", "").Replace(")", "");
                /*string varType = "";
                switch (typeStripped.ToUpper())
                {
                    case "VARCHAR":
                    case "NVARCHAR":
                        varType = "string";
                        break;
                    case "INT":
                        varType = "int";
                        break;
                    case "BIT":
                        varType = "bool";
                        break;
                    case "MONEY":
                        varType = "decimal";
                        break;
                    case "UNIQUEIDENTIFIER":
                        varType = "Guid";
                        break;
                    case "DATETIME":
                        varType = "DateTime";
                        break;
                    case "TIME":
                        varType = "TimeSpan";
                        break;

                    default:
                        throw new Exception("No type found");

                }*/
                /*if (nullable && varType != "string")
                {
                    varType = varType + "?";
                }*/
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

        public static List<SqlParameter> GetViewOutput(string connectionString, View view)
        {
            List<SqlParameter> values = new List<SqlParameter>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = $"SET FMTONLY ON SELECT * FROM {view.Name} SET FMTONLY OFF";
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

       private static string auditHeader;

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
                       bool isAuditHeader = fields == auditHeader;
                       while (isAuditHeader && !isAudit)
                       {
                           reader.NextResult();
                           fields = "";
                           for (int i = 0; i < reader.FieldCount; i++)
                           {
                               fields += "|" + reader.GetName(i);
                           }
                           isAuditHeader = fields == auditHeader;
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
                                    SqlDbType = ParamType(reader.GetDataTypeName(i).ToString())

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



        public static SqlDbType ParamType(string name)
        {
            SqlDbType ret = new SqlDbType();
            switch (name.ToUpper())
            {
                case "VARCHAR":
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
                default:
                    throw new Exception("No type found");

            }
            return ret;
        }

        #endregion

        #region Parameters
        public static void BuildParametersClass(string procName, string connectionString, ref StringBuilder sb)
        {
            List<SqlParameter> parameters = GetParameters(procName, connectionString);
            BuildParametersClass(procName, parameters, ref sb);
        }

        public static void BuildParametersClass(string procName, List<SqlParameter> parameters, ref StringBuilder sb)
        {
            sb.AppendLine(string.Format("\tpublic class {0}Parameters : IParameter", procName));
            sb.AppendLine("\t{");
            foreach (SqlParameter param in parameters)
            {
                string paramType = ParamType(param.SqlDbType);

                string nullable = param.IsNullable && param.SqlDbType != SqlDbType.VarChar ? "?" : "";
                string name = param.ParameterName.Replace("@", "");
                string line = String.Format($"\t\tpublic {paramType}{nullable} {name}");
                sb.AppendLine(line + " { get; set;}");
            }
            sb.AppendLine("\t}");
        }

        #endregion


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
                                default:
                                    throw new Exception("No type found");
                            }

                            SqlParameter param = new SqlParameter(paramName, sqlDbType);
                            param.Size = size;
                            param.IsNullable = nullable;
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
                    paramType = "DateTime";
                    break;
                case System.Data.SqlDbType.Time:
                    paramType = "TimeSpan";
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

        public static List<View> GetViews(string connectionString)
        {
            List<View> ret = new List<View>();
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
                            View vw = new View();
                            vw.Name = name;
                            ret.Add(vw);
                        }
                    }
                }
            }
            return ret;
        }

        public static void BuildView(View view, string connectionString, ref StringBuilder sb, int baseIdnt)
        {
            string baseIndent = Indent(baseIdnt);
            string indentOne = Indent(baseIdnt + 1);
            string indentTwo = Indent(baseIdnt + 2);
            string indentThree = Indent(baseIdnt + 3);
            string indentFour = Indent(baseIdnt + 4);
            string indentFive = Indent(baseIdnt + 5);
            string indentSeven = Indent(baseIdnt + 7);

            sb.AppendLine(baseIndent + $"public class {view.Name}");
            sb.AppendLine(baseIndent + "{");
            

            List <SqlParameter> parameters = GetViewOutput(connectionString, view);
            foreach (SqlParameter param in parameters)
            {
                sb.AppendLine(indentOne + $"public string {param.ParameterName}" + " {get;set;}");
            }

            sb.AppendLine(baseIndent + "}");
        }
    }

    public class View
    {
        public string Name { get; set; }

        public List<SqlParameter> Fields { get; set; }
    }


   

}
