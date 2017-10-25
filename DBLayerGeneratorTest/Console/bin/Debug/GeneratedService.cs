using AppointmentLibrary.Calls;
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
        public class AppointmentService : IAppointmentService
        {
            public string ConnectionString { get; set; }

            public bool ReturnExceptionMessage
            {
                get
                {
                    return bool.Parse(WebConfigurationManager.AppSettings["ReturnExceptionMessage"]);
                }
            }

            public AppointmentService()
            {
                this.ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
                //ReturnExceptionMessage = false;
            }


            public AppointmentService(string connectionString)
            {
                this.ConnectionString = connectionString;
            }
			public string AppointmentUpsert(string parameters)
			{
				try
				{
				    spAppointmentUpsertParameters casted = JsonConvert.DeserializeObject<spAppointmentUpsertParameters> (parameters);
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
				    spAppointmentGetParameters casted = JsonConvert.DeserializeObject<spAppointmentGetParameters> (parameters);
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
			public string ServiceProviderUpsert(string parameters)
			{
				try
				{
				    spServiceProviderUpsertParameters casted = JsonConvert.DeserializeObject<spServiceProviderUpsertParameters> (parameters);
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
				    spServiceProviderGetParameters casted = JsonConvert.DeserializeObject<spServiceProviderGetParameters> (parameters);
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
				    spCustomerUpsertParameters casted = JsonConvert.DeserializeObject<spCustomerUpsertParameters> (parameters);
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
				    spCustomerGetParameters casted = JsonConvert.DeserializeObject<spCustomerGetParameters> (parameters);
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
			public string ActivityScheduleUpsert(string parameters)
			{
				try
				{
				    spActivityScheduleUpsertParameters casted = JsonConvert.DeserializeObject<spActivityScheduleUpsertParameters> (parameters);
				    List<spActivityScheduleUpsertResult> result = Calls.spActivityScheduleUpsertCall(casted, ConnectionString);
				
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
			public string ActivityScheduleGet(string parameters)
			{
				try
				{
				    spActivityScheduleGetParameters casted = JsonConvert.DeserializeObject<spActivityScheduleGetParameters> (parameters);
				    List<spActivityScheduleGetResult> result = Calls.spActivityScheduleGetCall(casted, ConnectionString);
				
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
			public string CreateUpsert(string parameters)
			{
				try
				{
				    spCreateUpsertParameters casted = JsonConvert.DeserializeObject<spCreateUpsertParameters> (parameters);
				    List<spCreateUpsertResult> result = Calls.spCreateUpsertCall(casted, ConnectionString);
				
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
				    spStoreUpsertParameters casted = JsonConvert.DeserializeObject<spStoreUpsertParameters> (parameters);
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
				    spStoreGetParameters casted = JsonConvert.DeserializeObject<spStoreGetParameters> (parameters);
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
			public string SystemUserUpsert(string parameters)
			{
				try
				{
				    spSystemUserUpsertParameters casted = JsonConvert.DeserializeObject<spSystemUserUpsertParameters> (parameters);
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
			public string SystemUserGet(string parameters)
			{
				try
				{
				    spSystemUserGetParameters casted = JsonConvert.DeserializeObject<spSystemUserGetParameters> (parameters);
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
			public string Login(string parameters)
			{
				try
				{
				    spLoginParameters casted = JsonConvert.DeserializeObject<spLoginParameters> (parameters);
				    List<spLoginResult> result = Calls.spLoginCall(casted, ConnectionString);
				
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
			public string AuditLogUpsert(string parameters)
			{
				try
				{
				    spAuditLogUpsertParameters casted = JsonConvert.DeserializeObject<spAuditLogUpsertParameters> (parameters);
				    List<spAuditLogUpsertResult> result = Calls.spAuditLogUpsertCall(casted, ConnectionString);
				
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
				    spAccountUpsertParameters casted = JsonConvert.DeserializeObject<spAccountUpsertParameters> (parameters);
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
				    spAccountGetParameters casted = JsonConvert.DeserializeObject<spAccountGetParameters> (parameters);
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
			public string ActivityTypeUpsert(string parameters)
			{
				try
				{
				    spActivityTypeUpsertParameters casted = JsonConvert.DeserializeObject<spActivityTypeUpsertParameters> (parameters);
				    List<spActivityTypeUpsertResult> result = Calls.spActivityTypeUpsertCall(casted, ConnectionString);
				
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
			public string ActivityTypeGet(string parameters)
			{
				try
				{
				    spActivityTypeGetParameters casted = JsonConvert.DeserializeObject<spActivityTypeGetParameters> (parameters);
				    List<spActivityTypeGetResult> result = Calls.spActivityTypeGetCall(casted, ConnectionString);
				
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
     }
}
