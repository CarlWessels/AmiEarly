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
    public class MerchantService : BaseServiceClass, IMerchantService
    {
        public MerchantService()
        {
            this.ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
            //ReturnExceptionMessage = false;
        }

        public MerchantService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

    }

    [ServiceContract]
    public interface IMerchantService : IBaseLoginInterface
    {
    }

    public class MerchantAuthenticator : ServiceAuthenticator
    {
        public override HasPermissionResult HasPermission(LoginResult loginResult)
        {
            Guid permissionGUID = LUPermission.MerchantServiceAccess.GUID();
            List<HasPermissionResult> hasPermission = Calls.spHasPermissionCall(loginResult.GUID, permissionGUID, ConnectionString);
            return hasPermission.FirstOrDefault();
        }
    }
}

