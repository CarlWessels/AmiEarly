using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public static class ServiceGenerator
    {
        public static void GenerateServiceCalls(string procName, List<SqlParameter> parameters, StringBuilder sb, int baseIndent)
        {
            string indentOne = GeneratorHelper.Indent(baseIndent + 1);
            string indentTwo = GeneratorHelper.Indent(baseIndent + 2);
            string indentThree = GeneratorHelper.Indent(baseIndent + 3);
            string indentFour = GeneratorHelper.Indent(baseIndent + 4);
            string indentFive = GeneratorHelper.Indent(baseIndent + 5);
            string indentSix = GeneratorHelper.Indent(baseIndent + 6);
            string indentSeven = GeneratorHelper.Indent(baseIndent + 7);

            bool upsert = false;
            bool getter = false;
            if (procName.Substring(procName.Length - 6, 6) == "Upsert")
            {
                upsert = true;
            }
            if (procName.Substring(procName.Length - 3, 3) == "Get")
            {
                getter = true;
            }

            string tableName = procName.Substring(2, procName.Length - 2);
            string resultName = $"{procName.Substring(2, procName.Length-2)}Result";
            tableName = tableName.Replace("Get", "");
            tableName = tableName.Replace("Upsert", "");

            string callName = "";
            if (upsert)
            {
                callName = $"{tableName}Upsert";
            }
            else if (getter)
            {
                callName = $"{tableName}Get";
            }
            else
            {
                //throw new NotImplementedException();
                callName = procName.Replace("sp", "");
            }

            sb.Append(indentTwo + $"public static List<{procName}Result> {callName} (");

            StringBuilder parameterList = new StringBuilder();

            bool first = true;
            foreach(SqlParameter param in parameters)
            {

                parameterList.Append(indentFour);
                if (!first)
                {
                    sb.Append(", ");
                    parameterList.Append(", ");
                }
                string paramNameClean = param.ParameterName.Replace("@", "");
                string parameterNameLowered = param.ParameterName.Replace("@", "");
                parameterNameLowered = parameterNameLowered[0].ToString().ToLower() + parameterNameLowered.Substring(1, parameterNameLowered.Length - 1);

                string parameterType = GeneratorHelper.ParamType(param.SqlDbType);
                if (param.SqlDbType != System.Data.SqlDbType.VarChar && param.IsNullable)
                {
                    parameterType += "?";
                }


                sb.Append($"{parameterType} {parameterNameLowered}");
                
                parameterList.AppendLine($"{paramNameClean} = {parameterNameLowered}");

                first = false;
            }
            string parametersName = $"{procName}Parameters";
            sb.AppendLine(")");
            sb.AppendLine(indentTwo + @"{");
            sb.AppendLine(indentThree + $"{parametersName} p = new {parametersName}()");
            sb.AppendLine(indentThree + "{");
            sb.AppendLine($"{parameterList.ToString()}");
            sb.AppendLine(indentThree + "};");
            sb.AppendLine(indentThree);
            sb.AppendLine(indentThree + "string parameters = JsonConvert.SerializeObject(p);");
            sb.AppendLine(indentThree + $"string resultStr = Service.{callName}(parameters);");
            sb.AppendLine(indentThree + $"List<{resultName}> result = JsonConvert.DeserializeObject<List<{resultName}>>(resultStr);");
            sb.AppendLine(indentThree + "return result;");


            sb.AppendLine(indentTwo + @"}");
        }

        public static void GenerateServiceCallsPre(StringBuilder sb)
        {
            sb.AppendLine("using AppointmentLibrary.Parameters;");
            sb.AppendLine("using AppointmentLibrary.ProcResults;");
            sb.AppendLine("using Newtonsoft.Json;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("");
            sb.AppendLine("namespace ApplicationClient");
            sb.AppendLine("{");
            sb.AppendLine("    public partial class AppointmentServiceClient");
            sb.AppendLine("    {");
            sb.AppendLine("        public AppointmentServiceClient(string username, string password)");
            sb.AppendLine("        {");
            sb.AppendLine("            service = new AppointmentService.AppointmentServiceClient();");
            sb.AppendLine("            service.ClientCredentials.UserName.UserName = username;");
            sb.AppendLine("            service.ClientCredentials.UserName.Password = password;");
            sb.AppendLine("        }");
            sb.AppendLine("        private static AppointmentService.AppointmentServiceClient service;");
            sb.AppendLine("");
            sb.AppendLine("        public static AppointmentService.AppointmentServiceClient Service");
            sb.AppendLine("        {");
            sb.AppendLine("            get");
            sb.AppendLine("            {");
            sb.AppendLine("                return service;");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
        }

        public static void GenerateServiceCallsPost(StringBuilder sb)
        {
            sb.AppendLine("     }");
            sb.AppendLine("}");
        }

        public static void GenerateInterfacePre(StringBuilder sb)
        {
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Runtime.Serialization;");
            sb.AppendLine("using System.ServiceModel;");
            sb.AppendLine("using System.ServiceModel.Web;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("");
            sb.AppendLine("namespace HostedService");
            sb.AppendLine("{");
            sb.AppendLine("    [ServiceContract]");
            sb.AppendLine("    public interface IAppointmentService");
            sb.AppendLine("    {");
        }

        public static void GenerateInterfacePost(StringBuilder sb)
        {
            sb.AppendLine("    }");
            sb.AppendLine("}");
        }

        public static void GenerateSerivcePre(StringBuilder sb)
        {
            sb.AppendLine("using AppointmentLibrary.Calls;");
            sb.AppendLine("using AppointmentLibrary.Parameters;");
            sb.AppendLine("using AppointmentLibrary.ProcResults;");
            sb.AppendLine("using Newtonsoft.Json;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Net;");
            sb.AppendLine("using System.ServiceModel.Web;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("using System.Web;");
            sb.AppendLine("using System.Web.Configuration;");
            sb.AppendLine("");
            sb.AppendLine("namespace HostedService");
            sb.AppendLine("    {");
            sb.AppendLine("        public class AppointmentService : IAppointmentService");
            sb.AppendLine("        {");
            sb.AppendLine("            public string ConnectionString { get; set; }");
            sb.AppendLine("");
            sb.AppendLine("            public bool ReturnExceptionMessage");
            sb.AppendLine("            {");
            sb.AppendLine("                get");
            sb.AppendLine("                {");
            sb.AppendLine(@"                    return bool.Parse(WebConfigurationManager.AppSettings[""ReturnExceptionMessage""]);");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            public AppointmentService()");
            sb.AppendLine("            {");
            sb.AppendLine(@"                this.ConnectionString = WebConfigurationManager.AppSettings[""ConnectionString""];");
            sb.AppendLine("                //ReturnExceptionMessage = false;");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("            public AppointmentService(string connectionString)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ConnectionString = connectionString;");
            sb.AppendLine("            }");
        }

        public static void GenerateSerivcePost(StringBuilder sb)
        {
            sb.AppendLine("     }");
            sb.AppendLine("}");
        }

        public static void GenerateService (string procName, List<SqlParameter> parameters, StringBuilder sb, int baseIndent)
        {
            string indentOne = GeneratorHelper.Indent(baseIndent + 1);
            string indentTwo = GeneratorHelper.Indent(baseIndent + 2);
            string indentThree = GeneratorHelper.Indent(baseIndent + 3);
            string indentFour = GeneratorHelper.Indent(baseIndent + 4);
            string indentFive = GeneratorHelper.Indent(baseIndent + 5);
            string indentSix = GeneratorHelper.Indent(baseIndent + 6);
            string indentSeven = GeneratorHelper.Indent(baseIndent + 7);

            string paramName = $"{procName}Parameters";
            string resultName = $"{procName.Substring(2, procName.Length-2)}Result";
            string callName = "";
            string tableName = procName.Substring(2, procName.Length - 2);
            tableName = tableName.Replace("Get", "");
            tableName = tableName.Replace("Upsert", "");

            bool upsert = false;
            bool getter = false;
            if (procName.Substring(procName.Length - 6, 6) == "Upsert")
            {
                upsert = true;
            }
            if (procName.Substring(procName.Length - 3, 3) == "Get")
            {
                getter = true;
            }

            if (upsert)
            {
                callName = $"{tableName}Upsert";
            }
            else if (getter)
            {
                callName = $"{tableName}Get";
            }
            else
            {
                //throw new NotImplementedException();
                callName = procName.Replace("sp", "");
            }

            sb.AppendLine(indentThree + $"public string {callName}(string parameters)");
            sb.AppendLine(indentThree + "{");

            sb.AppendLine(indentFour + "try");
            sb.AppendLine(indentFour + "{");
            sb.AppendLine(indentFour + $"    {paramName} casted = JsonConvert.DeserializeObject<{paramName }> (parameters);");
            sb.AppendLine(indentFour + $"    List<{resultName }> result = Calls.{procName}Call(casted, ConnectionString);");
            sb.AppendLine(indentFour + "");
            sb.AppendLine(indentFour + "    string json = JsonConvert.SerializeObject(result);");
            sb.AppendLine(indentFour + "    return json;");
            sb.AppendLine(indentFour + "}");
            sb.AppendLine(indentFour + "catch (Exception ex)");
            sb.AppendLine(indentFour + "{");
            sb.AppendLine(indentFour + "    OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;");
            sb.AppendLine(indentFour + "    response.StatusCode = HttpStatusCode.BadRequest;");
            sb.AppendLine(indentFour + "    if (ReturnExceptionMessage)");
            sb.AppendLine(indentFour + "    {");
            sb.AppendLine(indentFour + "        response.StatusDescription = ex.Message;");
            sb.AppendLine(indentFour + "        HttpContext.Current.Response.Write(ex.Message);");
            sb.AppendLine(indentFour + "    }");
            sb.AppendLine(indentFour + "    else");
            sb.AppendLine(indentFour + "    {");
            sb.AppendLine(indentFour + @"        response.StatusDescription = ""Failed with transaction"";");
            sb.AppendLine(indentFour + @"        HttpContext.Current.Response.Write(""Failed with transaction"");");
            sb.AppendLine(indentFour + "    }");
            sb.AppendLine(indentFour + "    return null;");
            sb.AppendLine(indentFour + "}");
            sb.AppendLine(indentThree + "}");


        }

        public static void GenerateInterface(string procName, List<SqlParameter> parameters, StringBuilder sb, int baseIndent)
        {
            string indentOne = GeneratorHelper.Indent(baseIndent + 1);
            string indentTwo = GeneratorHelper.Indent(baseIndent + 2);
            string indentThree = GeneratorHelper.Indent(baseIndent + 3);
            string indentFour = GeneratorHelper.Indent(baseIndent + 4);
            string indentFive = GeneratorHelper.Indent(baseIndent + 5);
            string indentSix = GeneratorHelper.Indent(baseIndent + 6);
            string indentSeven = GeneratorHelper.Indent(baseIndent + 7);

            bool upsert = false;
            bool getter = false;
            if (procName.Substring(procName.Length - 6, 6) == "Upsert")
            {
                upsert = true;
            }
            if (procName.Substring(procName.Length - 3, 3) == "Get")
            {
                getter = true;
            }

            string tableName = procName.Substring(2, procName.Length - 2);
            string resultName = $"{procName}Result";
            tableName = tableName.Replace("Get", "");
            tableName = tableName.Replace("Upsert", "");

            string callName = "";
            if (upsert)
            {
                callName = $"{tableName}Upsert";
            }
            else if (getter)
            {
                callName = $"{tableName}Get";
            }
            else
            {
                //throw new NotImplementedException();
                callName = procName.Replace("sp", "");
            }

            sb.AppendLine(indentTwo +  "[OperationContract]");
            sb.AppendLine(indentTwo + $"string {callName} (string parameters);");
            /*bool first = true;
            foreach (SqlParameter param in parameters)
            {
                if (!first)
                {
                    sb.Append(", ");
                }
                string paramNameClean = param.ParameterName.Replace("@", "");
                string parameterNameLowered = param.ParameterName.Replace("@", "");
                parameterNameLowered = parameterNameLowered[0].ToString().ToLower() + parameterNameLowered.Substring(1, parameterNameLowered.Length - 1);

                sb.Append($"{GeneratorHelper.ParamType(param.SqlDbType)} {parameterNameLowered}");

                first = false;
            }
            sb.AppendLine(");");*/
        }
    }
    
}
