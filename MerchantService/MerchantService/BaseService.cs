using ApplicationClient.Enums;
using AppointmentLibrary.Calls;
using AppointmentLibrary.ProcResults;
using HostedService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;

namespace MerchantService
{
    [ServiceContract]
    public interface IBaseLoginInterface
    {
        [OperationContract]
        string Login(string parameters);

        [OperationContract]
        string RefreshTokenNoReturn(string parameters);

    }

    public class BaseServiceClass : IBaseLoginInterface
    {
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

            Guid permissionGUID = LUPermission.MerchantServiceAccess.GUID();

            HasPermissionResult hasPermission = HasPermission(loginResult);
            if (!(bool)hasPermission.HasPermission)
            {
                throw new FaultException("Permission denied");
            }
        }

        public virtual HasPermissionResult HasPermission(LoginResult loginResult)
        {
            throw new NotImplementedException();
        }

    }
}