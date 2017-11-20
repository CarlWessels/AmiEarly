using HostedService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public class FlaggedService : AppointmentService
    {
        [IsCustomerService]
        [IsMerchantService]
        public override string CustomerGet(string parameters)
        {
            return base.CustomerGet(parameters);
        }

        [IsMerchantService]
        public override string CustomerGetAsList(string parameters)
        {
            return base.CustomerGetAsList(parameters);
        }

        [IsMerchantService]
        public override string SystemUserUpsert(string parameters)
        {
            return base.SystemUserUpsert(parameters);
        }

        [IsMerchantService]
        public override string ServiceProviderUpsert(string parameters)
        {
            return base.ServiceProviderUpsert(parameters);
        }

        [IsMerchantService]
        public override string AppointmentUpsert(string parameters)
        {
            return base.AppointmentUpsert(parameters);
        }

        [IsMerchantService]
        public override string AppointmentGet(string parameters)
        {
            return base.AppointmentGet(parameters);
        }

        [IsMerchantService]
        public override string CustomerUpsert(string parameters)
        {
            return base.CustomerUpsert(parameters);
        }

        [IsMerchantService]
        public override string CustomerAddressGet(string parameters)
        {
            return base.CustomerAddressGet(parameters);
        }

        [IsMerchantService]
        public override string SystemUserGet(string parameters)
        {
            return base.SystemUserGet(parameters);
        }

        [IsMerchantService]
        public override string SystemUserGetAsList(string parameters)
        {
            return base.SystemUserGetAsList(parameters);
        }
    }

    public class IsMerchantService : Attribute
    {

    }

    public class IsCustomerService : Attribute
    {

    }



    public class FlaggedServiceCreator
    {
        public string ClassName;
        public Type AttributeType;

        public FlaggedServiceCreator(string className, Type attributeType)
        {
            ClassName = className;
            AttributeType = attributeType;
        }



        public void Create()
        {
            StringBuilder clSB = new StringBuilder();
            StringBuilder inSB = new StringBuilder();
            MemberInfo[] methods = typeof(FlaggedService).GetMethods(); 
            foreach (MemberInfo member in methods.Where( m => m.CustomAttributes.Count() > 0))
            {

                //if (member.GetCustomAttributes(typeof(IsMerchantService), true).Length > 0) 
                if (member.GetCustomAttributes(AttributeType, true).Length > 0)
                {
                    clSB.AppendLine($"        public string {member.Name} (string parameters)");
                    clSB.AppendLine("        {");
                    clSB.AppendLine($"            return AppointmentService.{member.Name}(parameters, ConnectionString, ReturnExceptionMessage);");
                    clSB.AppendLine("        }");


                    inSB.AppendLine($"        [OperationContract]");
                    inSB.AppendLine($"        string {member.Name} (string parameters);");
                }
            }

            StringBuilder msSB = new StringBuilder();
            msSB.AppendLine("using HostedService;");
            msSB.AppendLine("using ServiceLibrary;");
            msSB.AppendLine("using System;");
            msSB.AppendLine("using System.Collections.Generic;");
            msSB.AppendLine("using System.Linq;");
            msSB.AppendLine("using System.Runtime.Serialization;");
            msSB.AppendLine("using System.ServiceModel;");
            msSB.AppendLine("using System.Web;");
            msSB.AppendLine("using System.Web.Configuration;");
            msSB.AppendLine($"namespace {ClassName}");
            msSB.AppendLine("{");
            msSB.AppendLine($"     public partial class {ClassName} : BaseService, I{ClassName}");
            msSB.AppendLine("     {");
            msSB.Append(clSB.ToString());
            msSB.AppendLine("     }");
            msSB.AppendLine("");
            msSB.AppendLine("     [ServiceContract]");
            msSB.AppendLine($"     public interface I{ClassName} : IBaseService");
            msSB.AppendLine("     {");
            msSB.Append(inSB.ToString());
            msSB.AppendLine("     }");
            msSB.AppendLine("}");

            File.WriteAllText($@"{ClassName}.cs", msSB.ToString());

            //Console.ReadKey();
        }

    }
}
