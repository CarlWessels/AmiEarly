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
            List<SqlParameter> parameters = GeneratorHelper.GetParameters("spAuditLogUpsert", connectionString);
            //auditHeader = GetFieldsAsString(connectionString, "spAuditLogUpsert", parameters);

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
            GeneratorHelper.BuildNamespacePre($"{nameSpace}.ProcResults", ref resultsSb);
            GeneratorHelper.BuildNamespacePre($"{nameSpace}.Parameters", ref parametersSb);
            GeneratorHelper.BuildNamespacePre($"{nameSpace}.Calls", ref callsSb);

            ResultsGenerator.BuildResultsUsing(ref resultsSb);
            ResultsGenerator.BuildResultsUsing(ref parametersSb);
            CallsGenerator.BuildCallsUsing(ref callsSb, nameSpace);

            GeneratorHelper.BuildParametersPre(ref parametersSb);
            CallsGenerator.BuildCallClassPre(ref callsSb);

            List<string> sps = GeneratorHelper.GetSPs(connectionString);

            foreach (string sp in sps)
            {

                List<SqlParameter> parameters = GeneratorHelper.GetParameters(sp, connectionString);
                List<SqlParameter> values = ResultsGenerator.GetResults(connectionString, sp, parameters);
                //List<string> results = GetResults(connectionString, sp, parameters);
                ResultsGenerator.BuildResultClass(sp, values, ref resultsSb);
                ParametersGenerator.BuildParametersClass(sp, parameters, ref parametersSb);
                //BuildCallWithClass(sp, parameters, results, ref callsSb, 1);
                CallsGenerator.BuildCalls(sp, parameters, values, ref callsSb, 1);
                //File.WriteAllText(@"test.txt",resultsSb.ToString());
            }
            CallsGenerator.BuildCallClassPost(ref callsSb);

            GeneratorHelper.BuildNamespacePost(ref resultsSb);
            GeneratorHelper.BuildNamespacePost(ref parametersSb);
            GeneratorHelper.BuildNamespacePost(ref callsSb);

            StringBuilder viewSb = new StringBuilder();
            StringBuilder tableSb = new StringBuilder();
            List<POCO> views = ViewsGenerator.GetViews(connectionString);
            List<POCO> tables = TablesGenerator.GetTables(connectionString);

            GeneratorHelper.BuildNamespacePre($"{nameSpace}.Views", ref viewSb);
            GeneratorHelper.BuildNamespacePre($"{nameSpace}.Tables", ref tableSb);

            foreach (POCO vw in views)
            {
                GeneratorHelper.BuildPOCO(vw,connectionString, ref viewSb, 1);
            }

            foreach (POCO tbl in tables)
            {
                GeneratorHelper.BuildPOCO(tbl, connectionString, ref tableSb, 1, "tbl");
            }

            GeneratorHelper.BuildNamespacePost(ref viewSb);
            GeneratorHelper.BuildNamespacePost(ref tableSb);

            File.WriteAllText("GeneratedResults.cs", resultsSb.ToString());
            File.WriteAllText("GeneratedParameters.cs", parametersSb.ToString());
            File.WriteAllText("GeneratedCalls.cs", callsSb.ToString());
            File.WriteAllText("GeneratedViews.cs", viewSb.ToString());
            File.WriteAllText("GeneratedTables.cs", tableSb.ToString());
        }


    }

  

   

}
