using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using AppointmentLibrary.ProcResults;
using System.IdentityModel.Tokens;
using AppointmentLibrary.Calls;
using System.Web.Configuration;
using ApplicationClient.Enums;
using System.ServiceModel;

namespace HostedService
{
    public class ServiceAuthenticator : UserNamePasswordValidator
    {
        public string ConnectionString { get; set; }

        public ServiceAuthenticator()
        {
            this.ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
        }
        public ServiceAuthenticator(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public override void Validate(string userName, string password)
        {
            List<LoginResult> result = Calls.spLoginCall(userName, password, ConnectionString);
            if (result.Count == 0)
            {
                throw new FaultException("Invalid username or password");
            }
            LoginResult loginResult = result.FirstOrDefault();

            Guid permissionGUID = LUPermission.ServiceAccess.GUID();
            
            List<HasPermissionResult> hasPermission = Calls.spHasPermissionCall(loginResult.GUID, permissionGUID, ConnectionString);
            if (result.Count == 0 || !(bool)hasPermission.FirstOrDefault().HasPermission)
            {
                throw new FaultException("Permission denied");
            }

        }
    }
}