using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;

namespace HostedService
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
}