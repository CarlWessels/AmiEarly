/*using AppointmentLibrary.Calls;
using AppointmentLibrary.Parameters;
using AppointmentLibrary.ProcResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace HostedService
{
    public class AppointmentService_old : IAppointmentService
    {
        public string ConnectionString { get; set; }

        public bool ReturnExceptionMessage
        {
            get
            {
                return bool.Parse(WebConfigurationManager.AppSettings["ReturnExceptionMessage"]);
            }
        }

        public AppointmentService_old()
        {
            this.ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
            //ReturnExceptionMessage = false;
        }


        public AppointmentService_old(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string SystemUserGet(string parameters)
        {
            try
            {
                spSystemUserGetParameters casted = JsonConvert.DeserializeObject<spSystemUserGetParameters>(parameters);
                List<spSystemUserGetResult> result = Calls.spSystemUserGetCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string SystemUserUpsert(string parameters)
        {
            try
            {
                spSystemUserUpsertParameters casted = JsonConvert.DeserializeObject<spSystemUserUpsertParameters>(parameters);
                List<spSystemUserUpsertResult> result = Calls.spSystemUserUpsertCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string AccountUpsert(string parameters)
        {
            try
            {
                spAccountUpsertParameters casted = JsonConvert.DeserializeObject<spAccountUpsertParameters>(parameters);
                List<spAccountUpsertResult> result = Calls.spAccountUpsertCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }

        }

        public string AccountGet(string parameters)
        {
            try
            {
                spAccountGetParameters casted = JsonConvert.DeserializeObject<spAccountGetParameters>(parameters);
                List<spAccountGetResult> result = Calls.spAccountGetCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string StoreUpsert(string parameters)
        {
            try
            {
                spStoreUpsertParameters casted = JsonConvert.DeserializeObject<spStoreUpsertParameters>(parameters);
                List<spStoreUpsertResult> result = Calls.spStoreUpsertCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string StoreGet(string parameters)
        {
            try
            {
                spStoreGetParameters casted = JsonConvert.DeserializeObject<spStoreGetParameters>(parameters);
                List<spStoreGetResult> result = Calls.spStoreGetCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string ServiceProvdiderUpsert(string parameters)
        {
            try
            {
                spServiceProviderUpsertParameters casted = JsonConvert.DeserializeObject<spServiceProviderUpsertParameters>(parameters);
                List<spServiceProviderUpsertResult> result = Calls.spServiceProviderUpsertCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string ServiceProviderGet(string parameters)
        {
            try
            {
                spServiceProviderGetParameters casted = JsonConvert.DeserializeObject<spServiceProviderGetParameters>(parameters);
                List<spServiceProviderGetResult> result = Calls.spServiceProviderGetCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string CustomerUpsert(string parameters)
        {
            try
            {
                spCustomerUpsertParameters casted = JsonConvert.DeserializeObject<spCustomerUpsertParameters>(parameters);
                List<spCustomerUpsertResult> result = Calls.spCustomerUpsertCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }

        }

        public string CustomerGet(string parameters)
        {
            try
            {
                spCustomerGetParameters casted = JsonConvert.DeserializeObject<spCustomerGetParameters>(parameters);
                List<spCustomerGetResult> result = Calls.spCustomerGetCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string AppointmentUpsert(string parameters)
        {
            try
            {
                spAppointmentUpsertParameters casted = JsonConvert.DeserializeObject<spAppointmentUpsertParameters>(parameters);
                List<spAppointmentUpsertResult> result = Calls.spAppointmentUpsertCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string AppointmentGet(string parameters)
        {
            try
            {
                spAppointmentGetParameters casted = JsonConvert.DeserializeObject<spAppointmentGetParameters>(parameters);
                List<spAppointmentGetResult> result = Calls.spAppointmentGetCall(casted, ConnectionString);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
        }

        public string ThrowError(string parameters)
        {
            try
            {
                int a = 1;
                a = a - 1;
                int i = 100 / a;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = HttpStatusCode.BadRequest;
                if (ReturnExceptionMessage)
                {
                    response.StatusDescription = ex.Message;
                    HttpContext.Current.Response.Write(ex.Message);
                }
                else
                {
                    response.StatusDescription = "Failed with transaction";
                    HttpContext.Current.Response.Write("Failed with transaction");
                }
                return null;
            }
            return "";
        }

        public string CreateUpsert(string tableName)
        {
            throw new NotImplementedException();
        }

        public string SystemUserUpsert(Guid gUID, bool isDeleted, DateTime activeDateTime, DateTime terminationDateTime, string username, string password, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string SystemUserGet(Guid systemUserGUID)
        {
            throw new NotImplementedException();
        }

        public string AccountUpsert(Guid gUID, bool isDeleted, DateTime activeDateTime, DateTime terminationDateTime, string accountName, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string AccountGet(Guid accountGUID)
        {
            throw new NotImplementedException();
        }

        public string ActivityTypeUpsert(Guid gUID, bool isDeleted, string activityType, Guid accountGUID, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string ActivityTypeGet(Guid activityTypeGUID)
        {
            throw new NotImplementedException();
        }

        public string AppointmentUpsert(Guid gUID, bool isDeleted, DateTime startDateTime, TimeSpan duration, DateTime actualStartDateTime, DateTime actualEndDateTime, Guid customerGUID, Guid storeGUID, Guid serviceProviderGUID, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string AppointmentGet(Guid appointmentGUID)
        {
            throw new NotImplementedException();
        }

        public string ServiceProviderUpsert(Guid gUID, bool isDeleted, DateTime activeDateTime, DateTime terminationDateTime, string firstname, string surname, Guid accountGUID, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string ServiceProviderGet(Guid serviceProviderGUID)
        {
            throw new NotImplementedException();
        }

        public string CustomerUpsert(Guid gUID, bool isDeleted, DateTime activeDateTime, DateTime terminationDateTime, string firstname, string surname, Guid accountGUID, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string CustomerGet(Guid customerGUID)
        {
            throw new NotImplementedException();
        }

        public string ActivityScheduleUpsert(Guid gUID, bool isDeleted, int doW, TimeSpan startTime, TimeSpan endTime, Guid activityTypeGUID, Guid serviceProviderGUID, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string ActivityScheduleGet(Guid activityScheduleGUID)
        {
            throw new NotImplementedException();
        }

        public string StoreUpsert(Guid gUID, bool isDeleted, DateTime activeDateTime, DateTime terminationDateTime, string storeName, Guid accountGUID, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }

        public string StoreGet(Guid storeGUID)
        {
            throw new NotImplementedException();
        }

        public string Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public string AuditLogUpsert(Guid gUID, string source, string tableName, string beforeSnapshot, string afterSnapshot, Guid tableGUID, Guid systemUserGUID, bool returnResults)
        {
            throw new NotImplementedException();
        }
    }
}
*/