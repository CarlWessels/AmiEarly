using AppointmentLibrary.Parameters;
using AppointmentLibrary.ProcResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentLibrary
{
    public class AppointmentService : IAppointmentService
    {
        public string ConnectionString { get; set; }
        public AppointmentService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public string AccountUpsert(string parameters)
        {
            spAccountUpsertParameters casted = JsonConvert.DeserializeObject<spAccountUpsertParameters>(parameters);
            List<spAccountUpsertResult> result = Calls.Calls.spAccountUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }
        public string AppointmentUpsert(string parameters)
        {
            spAppointmentUpsertParameters casted = JsonConvert.DeserializeObject<spAppointmentUpsertParameters>(parameters);
            List<spAppointmentUpsertResult> result = Calls.Calls.spAppointmentUpsertCall(casted, ConnectionString);
            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        public string CustomerUpsert(string parameters)
        {
            spCustomerUpsertParameters casted = JsonConvert.DeserializeObject<spCustomerUpsertParameters>(parameters);
            List<spCustomerUpsertResult> result = Calls.Calls.spCustomerUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;

        }

        public string ServiceProvdiderUpsert(string parameters)
        {
            spServiceProviderUpsertParameters casted = JsonConvert.DeserializeObject<spServiceProviderUpsertParameters>(parameters);
            List<spServiceProviderUpsertResult> result = Calls.Calls.spServiceProviderUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        public string StoreUpsert(string parameters)
        {
            spStoreUpsertParameters casted = JsonConvert.DeserializeObject<spStoreUpsertParameters>(parameters);
            List<spStoreUpsertResult> result = Calls.Calls.spStoreUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        public string AccountGet(string parameters)
        {
            spAccountGetParameters casted = JsonConvert.DeserializeObject<spAccountGetParameters>(parameters);
            List<spAccountGetResult> result = Calls.Calls.spAccountGetCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        public string AppointmentGet(string parameters)
        {
            spAppointmentGetParameters casted = JsonConvert.DeserializeObject<spAppointmentGetParameters>(parameters);
            List<spAppointmentGetResult> result = Calls.Calls.spAppointmentGetCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }
    }
}
