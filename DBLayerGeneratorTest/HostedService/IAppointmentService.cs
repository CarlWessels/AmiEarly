/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HostedService
{
    [ServiceContract]
    public interface IAppointmentService
    {
        [OperationContract]
        string AccountUpsert(string parameters);
        [OperationContract]
        string AccountGet(string parameters);
        [OperationContract]

        string SystemUserGet(string parameters);
        [OperationContract]
        string SystemUserUpsert(string parameters);

        [OperationContract]
        string StoreUpsert(string parameters);
        [OperationContract]
        string StoreGet(string parameters);
        [OperationContract]

        string ServiceProvdiderUpsert(string parameters);
        [OperationContract]
        string ServiceProviderGet(string parameters);
        [OperationContract]

        string CustomerUpsert(string parameters);
        [OperationContract]
        string CustomerGet(string parameters);
        [OperationContract]

        string AppointmentGet(string parameters);
        [OperationContract]
        string AppointmentUpsert(string parameters);

        [OperationContract]
        string ThrowError(string parameters);
    }
}
*/