using AppointmentLibrary.Parameters;
using AppointmentLibrary.ProcResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationClient
{
    public partial class AppointmentServiceClient
    {
        public AppointmentServiceClient(string username, string password)
        {
            service = new AppointmentService.AppointmentServiceClient();
            service.ClientCredentials.UserName.UserName = username;
            service.ClientCredentials.UserName.Password = password;
        }
        private static AppointmentService.AppointmentServiceClient service;

        public static AppointmentService.AppointmentServiceClient Service
        {
            get
            {
                return service;
            }
        }
		public static List<spAppointmentUpsertResult> AppointmentUpsert (Guid? gUID, bool? isDeleted, DateTime? startDateTime, TimeSpan? duration, DateTime? actualStartDateTime, DateTime? actualEndDateTime, Guid? customerGUID, Guid? storeGUID, Guid? serviceProviderGUID, Guid? systemUserGUID, bool? returnResults)
		{
			spAppointmentUpsertParameters p = new spAppointmentUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, StartDateTime = startDateTime
				, Duration = duration
				, ActualStartDateTime = actualStartDateTime
				, ActualEndDateTime = actualEndDateTime
				, CustomerGUID = customerGUID
				, StoreGUID = storeGUID
				, ServiceProviderGUID = serviceProviderGUID
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.AppointmentUpsert(parameters);
			List<spAppointmentUpsertResult> result = JsonConvert.DeserializeObject<List<spAppointmentUpsertResult>>(resultStr);
			return result;
		}
		public static List<spAppointmentGetResult> AppointmentGet (Guid? appointmentGUID)
		{
			spAppointmentGetParameters p = new spAppointmentGetParameters()
			{
				AppointmentGUID = appointmentGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.AppointmentGet(parameters);
			List<spAppointmentGetResult> result = JsonConvert.DeserializeObject<List<spAppointmentGetResult>>(resultStr);
			return result;
		}
		public static List<spServiceProviderUpsertResult> ServiceProviderUpsert (Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string firstname, string surname, Guid? accountGUID, Guid? systemUserGUID, bool? returnResults)
		{
			spServiceProviderUpsertParameters p = new spServiceProviderUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, ActiveDateTime = activeDateTime
				, TerminationDateTime = terminationDateTime
				, Firstname = firstname
				, Surname = surname
				, AccountGUID = accountGUID
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.ServiceProviderUpsert(parameters);
			List<spServiceProviderUpsertResult> result = JsonConvert.DeserializeObject<List<spServiceProviderUpsertResult>>(resultStr);
			return result;
		}
		public static List<spServiceProviderGetResult> ServiceProviderGet (Guid? serviceProviderGUID)
		{
			spServiceProviderGetParameters p = new spServiceProviderGetParameters()
			{
				ServiceProviderGUID = serviceProviderGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.ServiceProviderGet(parameters);
			List<spServiceProviderGetResult> result = JsonConvert.DeserializeObject<List<spServiceProviderGetResult>>(resultStr);
			return result;
		}
		public static List<spCustomerUpsertResult> CustomerUpsert (Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string firstname, string surname, Guid? accountGUID, Guid? systemUserGUID, bool? returnResults)
		{
			spCustomerUpsertParameters p = new spCustomerUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, ActiveDateTime = activeDateTime
				, TerminationDateTime = terminationDateTime
				, Firstname = firstname
				, Surname = surname
				, AccountGUID = accountGUID
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.CustomerUpsert(parameters);
			List<spCustomerUpsertResult> result = JsonConvert.DeserializeObject<List<spCustomerUpsertResult>>(resultStr);
			return result;
		}
		public static List<spCustomerGetResult> CustomerGet (Guid? customerGUID)
		{
			spCustomerGetParameters p = new spCustomerGetParameters()
			{
				CustomerGUID = customerGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.CustomerGet(parameters);
			List<spCustomerGetResult> result = JsonConvert.DeserializeObject<List<spCustomerGetResult>>(resultStr);
			return result;
		}
		public static List<spActivityScheduleUpsertResult> ActivityScheduleUpsert (Guid? gUID, bool? isDeleted, int? doW, TimeSpan? startTime, TimeSpan? endTime, Guid? activityTypeGUID, Guid? serviceProviderGUID, Guid? systemUserGUID, bool? returnResults)
		{
			spActivityScheduleUpsertParameters p = new spActivityScheduleUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, DoW = doW
				, StartTime = startTime
				, EndTime = endTime
				, ActivityTypeGUID = activityTypeGUID
				, ServiceProviderGUID = serviceProviderGUID
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.ActivityScheduleUpsert(parameters);
			List<spActivityScheduleUpsertResult> result = JsonConvert.DeserializeObject<List<spActivityScheduleUpsertResult>>(resultStr);
			return result;
		}
		public static List<spActivityScheduleGetResult> ActivityScheduleGet (Guid? activityScheduleGUID)
		{
			spActivityScheduleGetParameters p = new spActivityScheduleGetParameters()
			{
				ActivityScheduleGUID = activityScheduleGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.ActivityScheduleGet(parameters);
			List<spActivityScheduleGetResult> result = JsonConvert.DeserializeObject<List<spActivityScheduleGetResult>>(resultStr);
			return result;
		}
		public static List<spCreateUpsertResult> CreateUpsert (string tableName)
		{
			spCreateUpsertParameters p = new spCreateUpsertParameters()
			{
				TableName = tableName

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.CreateUpsert(parameters);
			List<spCreateUpsertResult> result = JsonConvert.DeserializeObject<List<spCreateUpsertResult>>(resultStr);
			return result;
		}
		public static List<spStoreUpsertResult> StoreUpsert (Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string storeName, Guid? accountGUID, Guid? systemUserGUID, bool? returnResults)
		{
			spStoreUpsertParameters p = new spStoreUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, ActiveDateTime = activeDateTime
				, TerminationDateTime = terminationDateTime
				, StoreName = storeName
				, AccountGUID = accountGUID
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.StoreUpsert(parameters);
			List<spStoreUpsertResult> result = JsonConvert.DeserializeObject<List<spStoreUpsertResult>>(resultStr);
			return result;
		}
		public static List<spStoreGetResult> StoreGet (Guid? storeGUID)
		{
			spStoreGetParameters p = new spStoreGetParameters()
			{
				StoreGUID = storeGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.StoreGet(parameters);
			List<spStoreGetResult> result = JsonConvert.DeserializeObject<List<spStoreGetResult>>(resultStr);
			return result;
		}
		public static List<spSystemUserUpsertResult> SystemUserUpsert (Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string username, string password, Guid? systemUserGUID, bool? returnResults)
		{
			spSystemUserUpsertParameters p = new spSystemUserUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, ActiveDateTime = activeDateTime
				, TerminationDateTime = terminationDateTime
				, Username = username
				, Password = password
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.SystemUserUpsert(parameters);
			List<spSystemUserUpsertResult> result = JsonConvert.DeserializeObject<List<spSystemUserUpsertResult>>(resultStr);
			return result;
		}
		public static List<spSystemUserGetResult> SystemUserGet (Guid? systemUserGUID)
		{
			spSystemUserGetParameters p = new spSystemUserGetParameters()
			{
				SystemUserGUID = systemUserGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.SystemUserGet(parameters);
			List<spSystemUserGetResult> result = JsonConvert.DeserializeObject<List<spSystemUserGetResult>>(resultStr);
			return result;
		}
		public static List<spLoginResult> Login (string userName, string password)
		{
			spLoginParameters p = new spLoginParameters()
			{
				UserName = userName
				, Password = password

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.Login(parameters);
			List<spLoginResult> result = JsonConvert.DeserializeObject<List<spLoginResult>>(resultStr);
			return result;
		}
		public static List<spAuditLogUpsertResult> AuditLogUpsert (Guid? gUID, string source, string tableName, string beforeSnapshot, string afterSnapshot, Guid? tableGUID, Guid? systemUserGUID, bool? returnResults)
		{
			spAuditLogUpsertParameters p = new spAuditLogUpsertParameters()
			{
				GUID = gUID
				, Source = source
				, TableName = tableName
				, BeforeSnapshot = beforeSnapshot
				, AfterSnapshot = afterSnapshot
				, TableGUID = tableGUID
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.AuditLogUpsert(parameters);
			List<spAuditLogUpsertResult> result = JsonConvert.DeserializeObject<List<spAuditLogUpsertResult>>(resultStr);
			return result;
		}
		public static List<spAccountUpsertResult> AccountUpsert (Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string accountName, Guid? systemUserGUID, bool? returnResults)
		{
			spAccountUpsertParameters p = new spAccountUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, ActiveDateTime = activeDateTime
				, TerminationDateTime = terminationDateTime
				, AccountName = accountName
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.AccountUpsert(parameters);
			List<spAccountUpsertResult> result = JsonConvert.DeserializeObject<List<spAccountUpsertResult>>(resultStr);
			return result;
		}
		public static List<spAccountGetResult> AccountGet (Guid? accountGUID)
		{
			spAccountGetParameters p = new spAccountGetParameters()
			{
				AccountGUID = accountGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.AccountGet(parameters);
			List<spAccountGetResult> result = JsonConvert.DeserializeObject<List<spAccountGetResult>>(resultStr);
			return result;
		}
		public static List<spActivityTypeUpsertResult> ActivityTypeUpsert (Guid? gUID, bool? isDeleted, string activityType, Guid? accountGUID, Guid? systemUserGUID, bool? returnResults)
		{
			spActivityTypeUpsertParameters p = new spActivityTypeUpsertParameters()
			{
				GUID = gUID
				, IsDeleted = isDeleted
				, ActivityType = activityType
				, AccountGUID = accountGUID
				, SystemUserGUID = systemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.ActivityTypeUpsert(parameters);
			List<spActivityTypeUpsertResult> result = JsonConvert.DeserializeObject<List<spActivityTypeUpsertResult>>(resultStr);
			return result;
		}
		public static List<spActivityTypeGetResult> ActivityTypeGet (Guid? activityTypeGUID)
		{
			spActivityTypeGetParameters p = new spActivityTypeGetParameters()
			{
				ActivityTypeGUID = activityTypeGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.ActivityTypeGet(parameters);
			List<spActivityTypeGetResult> result = JsonConvert.DeserializeObject<List<spActivityTypeGetResult>>(resultStr);
			return result;
		}
     }
}
