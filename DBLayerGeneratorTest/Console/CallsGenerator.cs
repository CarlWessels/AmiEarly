using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Console
{
    public static class CallsGenerator
    {
        #region Calls
        public static void BuildCallsUsing(ref StringBuilder sb, string namespce)
        {
            //sb.AppendLine("\tusing System.Collections.Generic;");
            sb.AppendLine("\tusing System;");
            sb.AppendLine("\tusing System.Data.SqlClient;");
            sb.AppendLine($"\tusing {namespce}.ProcResults;");
            sb.AppendLine($"\tusing {namespce}.Parameters;");
            sb.AppendLine($"\tusing System.Collections.Generic;");

        }



        public static void BuildCallClassPre(ref StringBuilder sb)
        {

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
            string returnName = procname.Substring(2, procname.Length-2) + "Result";
            string procnameCleaned = procname.Substring(2, procname.Length - 2);
            string indentTwo = GeneratorHelper.Indent(baseIndent + 2);
            string indentThree = GeneratorHelper.Indent(baseIndent + 3);
            sb.Append(String.Format(indentTwo + $"public static List<{returnName}> {callName}") + "(");
            StringBuilder parametersSb = new StringBuilder();
            bool first = true;
            foreach (SqlParameter param in parameters)
            {
                string type = GeneratorHelper.ParamType(param.SqlDbType);
                if (param.IsNullable && type != "string")
                {
                    type = type + "?";
                }
                string name = param.ParameterName;
                string nameStripped = name.Replace("@", "");
                string nameStrippedLower = nameStripped[0].ToString().ToLower() + nameStripped.Substring(1,nameStripped.Length -1);
                if (first)
                {
                }
                else
                {
                    sb.Append(", ");
                }
                sb.Append(String.Format("{0} {1}", type, nameStrippedLower));
                parametersSb.AppendLine(indentThree + $"parameters.{nameStripped} = {nameStrippedLower};");

                first = false;
            }
            if (parameters.Count != 0)
            {
                sb.Append(", ");
            }
            sb.AppendLine("string connectionString)");
            sb.AppendLine(indentTwo + "{");
            sb.AppendLine(indentThree + $"{procnameCleaned}Parameters parameters = new {procnameCleaned}Parameters();");
            sb.AppendLine(parametersSb.ToString());
            sb.AppendLine(indentThree + String.Format("return {0} (parameters, connectionString);", callName));
            sb.AppendLine(indentTwo + "}");
        }
        public static void BuildCallWithClass(string procname, List<SqlParameter> parameters, List<SqlParameter> results, ref StringBuilder sb, int baseIndent)
        {
            string indentTwo = GeneratorHelper.Indent(baseIndent + 2);
            string indentThree = GeneratorHelper.Indent(baseIndent + 3);
            string indentFour = GeneratorHelper.Indent(baseIndent + 4);
            string indentFive = GeneratorHelper.Indent(baseIndent + 5);
            string indentSix = GeneratorHelper.Indent(baseIndent + 6);
            string indentSeven = GeneratorHelper.Indent(baseIndent + 7);
            string procnameCleaned = procname.Substring(2, procname.Length - 2);
            string returnName = procname.Substring(2, procname.Length - 2) + "Result";
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


            sb.Append(String.Format(indentTwo + "public static List<{0}> {1}Call ", returnName, procname) + "(");
            sb.Append($"{procnameCleaned}Parameters parameters");

            sb.Append(String.Format(", string connectionString"));

            sb.AppendLine(")");
            sb.AppendLine(indentTwo + "{");
            sb.AppendLine(indentThree + String.Format("List<{0}> ret = new List<{0}>();", returnName));
            sb.AppendLine(indentThree + @"using (SqlConnection conn = new SqlConnection(connectionString))");
            sb.AppendLine(indentThree + @"{");
            sb.AppendLine(indentFour + @"conn.Open();");

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
            sb.AppendLine(indentFour + $"            {returnName} res = new {returnName}();");
            foreach (SqlParameter result in results)
            {

                /*List<string> split = result.Split('|').ToList();
                string name = split[0];
                string type = split[1];
                bool nullable = split[2] == "True" ? true : false;*/
                string name = result.ParameterName;
                if (String.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("Result field name not specified");
                }
                string type = GeneratorHelper.ParamType(result.SqlDbType);
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
                        resultLine = (String.Format(@"res.{0} = reader[""{0}""].ToString();", name));
                        break;
                    case "INT":
                        //resultLine = (String.Format(@"res.{0} = int.Parse(reader[""{0}""].ToString());", name));
                        {
                            resultLine = $@"if (!String.IsNullOrWhiteSpace(reader[""{name}""].ToString()))" + Environment.NewLine;
                            //resultLine += indentSeven + @"{" + Environment.NewLine;
                            //resultLine += indentSeven + $@"    res.{name} = null;" + Environment.NewLine;
                            //resultLine += indentSeven + @"}" + Environment.NewLine;
                            //resultLine += indentSeven + $@"else" + Environment.NewLine;
                            resultLine += indentSeven + @"{" + Environment.NewLine;
                            resultLine += indentSeven + $@"    res.{name} = int.Parse(reader[""{name}""].ToString());" + Environment.NewLine;
                            resultLine += indentSeven + @"}";
                        }
                        break;
                    case "BOOL":
                        resultLine = (String.Format(@"res.{0} = (bool)reader[""{0}""];", name));
                        break;
                    case "DECIMAL":
                        resultLine = (String.Format(@"res.{0} = decimal.Parse(reader[""{0}""].ToString());", name));
                        break;
                    case "GUID":
                        resultLine = (String.Format(@"res.{0} = new Guid(reader[""{0}""].ToString());", name));
                        break;
                    case "DATETIME":
                        //if (nullable)
                        {
                            resultLine = $@"if (!String.IsNullOrWhiteSpace(reader[""{name}""].ToString()))" + Environment.NewLine;
                            //resultLine += indentSeven + @"{" + Environment.NewLine;
                            //resultLine += indentSeven + $@"    res.{name} = null;" + Environment.NewLine;
                            //resultLine += indentSeven + @"}" + Environment.NewLine;
                            //resultLine += indentSeven + $@"else" + Environment.NewLine;
                            resultLine += indentSeven + @"{" + Environment.NewLine;
                            resultLine += indentSeven + $@"    res.{name} = DateTime.Parse(reader[""{name}""].ToString());" + Environment.NewLine;
                            resultLine += indentSeven + @"}";
                        }
                        /*else
                        {
                            resultLine = (String.Format(@"ret.{0} = DateTime.Parse(reader[""{0}""].ToString());", name));
                        }*/
                        break;
                    case "TIMESPAN":
                        //resultLine = (String.Format(@"res.{0} = TimeSpan.Parse(reader[""{0}""].ToString());", name));
                        resultLine = $@"if (!String.IsNullOrWhiteSpace(reader[""{name}""].ToString()))" + Environment.NewLine;
                        resultLine += indentSeven + @"{" + Environment.NewLine;
                        resultLine += indentSeven + $@"    res.{name} = TimeSpan.Parse(reader[""{name}""].ToString());" + Environment.NewLine;
                        resultLine += indentSeven + @"}";
                        break;
                    case "BYTE[]":
                        resultLine = (String.Format(@"res.{0} = (byte[])(reader[""{0}""]);", name));
                        break;
                    default:
                        throw new Exception("No type found");


                }

                sb.AppendLine(indentSeven + resultLine);
            }
            sb.AppendLine(indentSeven + "ret.Add(res);");
            sb.AppendLine(indentThree + @"            }");



            sb.AppendLine(indentThree + @"        }");
            sb.AppendLine(indentThree + @"    }");
            sb.AppendLine(indentThree + @"}");
            sb.AppendLine(indentThree + @"return ret;");

            sb.AppendLine(indentTwo + "}");
        }

        #endregion

    }
}
