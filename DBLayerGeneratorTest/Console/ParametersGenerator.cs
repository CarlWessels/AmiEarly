using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace Console
{
    public static class ParametersGenerator
    {
        #region Parameters
        public static void BuildParametersClass(string procName, string connectionString, ref StringBuilder sb)
        {
            List<SqlParameter> parameters = GeneratorHelper.GetParameters(procName, connectionString);
            BuildParametersClass(procName, parameters, ref sb);
        }

        public static void BuildParametersClass(string procName, List<SqlParameter> parameters, ref StringBuilder sb)
        {
            string procNameCleaned = procName.Substring(2, procName.Length - 2);
            sb.AppendLine($"\tpublic partial class {procNameCleaned}Parameters : IParameter");
            sb.AppendLine("\t{");
            foreach (SqlParameter param in parameters)
            {
                string paramType = GeneratorHelper.ParamType(param.SqlDbType);

                string nullable = param.IsNullable && param.SqlDbType != SqlDbType.VarChar && param.SqlDbType != SqlDbType.VarBinary ? "?" : "";
                string name = param.ParameterName.Replace("@", "");
                string line = String.Format($"\t\tpublic {paramType}{nullable} {name}");
                sb.AppendLine(line + " { get; set;}");
            }
            sb.AppendLine("\t}");
        }

        #endregion

    }
}
