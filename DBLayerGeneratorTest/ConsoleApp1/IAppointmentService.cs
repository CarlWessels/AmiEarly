using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace AppointmentLibrary
{
    [ServiceContract]
    public interface IAppointmentService
    {
        [OperationContract]
        string AccountUpsert(string parameters);
        [OperationContract]
        string AppointmentUpsert(string parameters);
        [OperationContract]
        string StoreUpsert(string parameters);
        [OperationContract]
        string CustomerUpsert(string parameters);
        [OperationContract]
        string ServiceProvdiderUpsert(string parameters);
        [OperationContract]
        string AccountGet(string parameters);
        [OperationContract]
        string AppointmentGet(string parameters);
    }
}
