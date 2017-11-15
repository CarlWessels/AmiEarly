using HostedService;
using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;

namespace MerchantService
{
    public partial class MerchantService : BaseService, IMerchantService
    {
        public string SystemUserUpsert(string parameters)
        {
            return AppointmentService.SystemUserUpsert(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string ServiceProviderUpsert(string parameters)
        {
            return AppointmentService.ServiceProviderUpsert(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string AppointmentUpsert(string parameters)
        {
            return AppointmentService.AppointmentUpsert(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string AppointmentGet(string parameters)
        {
            return AppointmentService.AppointmentGet(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string CustomerUpsert(string parameters)
        {
            return AppointmentService.CustomerUpsert(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string CustomerGet(string parameters)
        {
            return AppointmentService.CustomerGet(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string CustomerGetAsList(string parameters)
        {
            return AppointmentService.CustomerGetAsList(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string SystemUserGet(string parameters)
        {
            return AppointmentService.SystemUserGet(parameters, ConnectionString, ReturnExceptionMessage);
        }

        public string SystemUserGetAsList(string parameters)
        {
            return AppointmentService.SystemUserGetAsList(parameters, ConnectionString, ReturnExceptionMessage);
        }
    }


    [ServiceContract]
    public interface IMerchantService : IBaseService
    {

        [OperationContract]
        string SystemUserUpsert(string parameters);

        [OperationContract]
        string CustomerUpsert(string parameters);

        [OperationContract]
        string ServiceProviderUpsert(string parameters);

        [OperationContract]
        string AppointmentUpsert(string parameters);

        [OperationContract]
        string AppointmentGet(string parameters);

        [OperationContract]
        string CustomerGet(string parameters);

        [OperationContract]
        string CustomerGetAsList(string parameters);

        [OperationContract]
        string SystemUserGet(string parameters);

        [OperationContract]
        string SystemUserGetAsList(string parameters);

    }


}