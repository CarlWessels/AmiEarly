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
            ResultsGenerator.AuditHeader = ResultsGenerator.GetFieldsAsString(connectionString, "spAuditLogUpsert", parameters);

            bool build = true;

            if (build)
            {
                DoBuild(connectionString);
            }
        }

        public static void DoBuild(string connectionString)
        {
            StringBuilder resultsSb = new StringBuilder();
            GeneratorHelper.AddHeader(resultsSb);
            StringBuilder parametersSb = new StringBuilder();
            GeneratorHelper.AddHeader(parametersSb);
            StringBuilder callsSb = new StringBuilder();
            GeneratorHelper.AddHeader(callsSb);
            StringBuilder serviceCalls = new StringBuilder();
            GeneratorHelper.AddHeader(serviceCalls);
            StringBuilder interfaceSb = new StringBuilder();
            GeneratorHelper.AddHeader(interfaceSb);
            StringBuilder serviceSb = new StringBuilder();
            GeneratorHelper.AddHeader(serviceSb);

            string nameSpace = "AppointmentLibrary";
            GeneratorHelper.BuildNamespacePre($"{nameSpace}.ProcResults", ref resultsSb);
            GeneratorHelper.BuildNamespacePre($"{nameSpace}.Parameters", ref parametersSb);
            GeneratorHelper.BuildNamespacePre($"{nameSpace}.Calls", ref callsSb);

            ResultsGenerator.BuildResultsUsing(ref resultsSb);
            ResultsGenerator.BuildResultsUsing(ref parametersSb);
            CallsGenerator.BuildCallsUsing(ref callsSb, nameSpace);
            ServiceGenerator.GenerateServiceCallsPre(serviceCalls);
            ServiceGenerator.GenerateInterfacePre(interfaceSb);
            ServiceGenerator.GenerateSerivcePre(serviceSb);
            GeneratorHelper.BuildParametersPre(ref parametersSb);
            CallsGenerator.BuildCallClassPre(ref callsSb);

            List<string> sps = GeneratorHelper.GetSPs(connectionString);

            foreach (string sp in sps)
            {

                List<SqlParameter> parameters = GeneratorHelper.GetParameters(sp, connectionString);
                List<SqlParameter> values = ResultsGenerator.GetResults(connectionString, sp, parameters);

                ResultsGenerator.BuildResultClass(sp, values, ref resultsSb);
                ParametersGenerator.BuildParametersClass(sp, parameters, ref parametersSb);
                CallsGenerator.BuildCalls(sp, parameters, values, ref callsSb, 1);
                
                ServiceGenerator.GenerateServiceCalls(sp, parameters, serviceCalls, 0);
                ServiceGenerator.GenerateInterface(sp, parameters, interfaceSb, 0);
                ServiceGenerator.GenerateService(sp, parameters, serviceSb, 0);
            }
            CallsGenerator.BuildCallClassPost(ref callsSb);
            ServiceGenerator.GenerateServiceCallsPost(serviceCalls);
            ServiceGenerator.GenerateInterfacePost(interfaceSb);
            ServiceGenerator.GenerateSerivcePost(serviceSb);
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
            File.WriteAllText("GeneratedServiceCalls.cs", serviceCalls.ToString());
            File.WriteAllText("GeneratedInterface.cs", interfaceSb.ToString());
            File.WriteAllText("GeneratedService.cs", serviceSb.ToString());
        }


    }

  

   

}
