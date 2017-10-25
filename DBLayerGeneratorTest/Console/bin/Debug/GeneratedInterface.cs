using System;
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
		string AppointmentUpsert (string parameters);
		[OperationContract]
		string AppointmentGet (string parameters);
		[OperationContract]
		string ServiceProviderUpsert (string parameters);
		[OperationContract]
		string ServiceProviderGet (string parameters);
		[OperationContract]
		string CustomerUpsert (string parameters);
		[OperationContract]
		string CustomerGet (string parameters);
		[OperationContract]
		string ActivityScheduleUpsert (string parameters);
		[OperationContract]
		string ActivityScheduleGet (string parameters);
		[OperationContract]
		string CreateUpsert (string parameters);
		[OperationContract]
		string StoreUpsert (string parameters);
		[OperationContract]
		string StoreGet (string parameters);
		[OperationContract]
		string SystemUserUpsert (string parameters);
		[OperationContract]
		string SystemUserGet (string parameters);
		[OperationContract]
		string Login (string parameters);
		[OperationContract]
		string AuditLogUpsert (string parameters);
		[OperationContract]
		string AccountUpsert (string parameters);
		[OperationContract]
		string AccountGet (string parameters);
		[OperationContract]
		string ActivityTypeUpsert (string parameters);
		[OperationContract]
		string ActivityTypeGet (string parameters);
    }
}
