using ApplicationClient.Enums;
using AppointmentLibrary.Calls;
using AppointmentLibrary.ProcResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Configuration;

namespace HostedService
{
    public class CustomerService : BaseServiceClass, ICustomerService
    {
        public CustomerService()
        {
            this.ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
            //ReturnExceptionMessage = false;
        }

        public CustomerService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

    }

    [ServiceContract]
    public interface ICustomerService : IBaseLoginInterface
    {
    }

    public class CustomerAuthenticator : ServiceAuthenticator
    {
        public override HasPermissionResult HasPermission(LoginResult loginResult)
        {
            Guid permissionGUID = LUPermission.CustomerServiceAccess.GUID();
            List<HasPermissionResult> hasPermission = Calls.spHasPermissionCall(loginResult.GUID, permissionGUID, ConnectionString);
            return hasPermission.FirstOrDefault();
        }
    }
}

