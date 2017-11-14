using ApplicationClient.Enums;
using AppointmentLibrary.Calls;
using AppointmentLibrary.ProcResults;
using HostedService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ServiceLibrary
{
    public class BaseService
    {
        public BaseService()
        {
            this.ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
            //ReturnExceptionMessage = false;
        }

        public BaseService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }

        public bool ReturnExceptionMessage
        {
            get
            {
                return bool.Parse(WebConfigurationManager.AppSettings["ReturnExceptionMessage"]);
            }
        }

        public string RefreshTokenNoReturn(string parameters)
        {
            return AppointmentService.RefreshTokenNoReturn(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string Login(string parameters)
        {
            return AppointmentService.Login(parameters, ConnectionString, ReturnExceptionMessage);
        }
    }

    [ServiceContract]
    public interface IBaseService
    {

        [OperationContract]
        string Login(string parameters);

        [OperationContract]
        string RefreshTokenNoReturn(string parameters);
    }

    public class BaseAuthenticator : UserNamePasswordValidator
    {
        public string ConnectionString { get; set; }

        public BaseAuthenticator()
        {
            this.ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
        }
        public BaseAuthenticator(string connectionString)
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

            Guid permissionGUID = PermissionGUID();
            
            List<HasPermissionResult> hasPermission = Calls.spHasPermissionCall(loginResult.GUID, permissionGUID, ConnectionString);
            if (result.Count == 0 || !(bool)hasPermission.FirstOrDefault().HasPermission)
            {
                throw new FaultException("Permission denied");
            }

        }

        public virtual Guid PermissionGUID()
        {
            throw new NotImplementedException();
        }
    }
}
