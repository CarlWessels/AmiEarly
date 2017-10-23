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
            spAccountUpsertResult result  = Calls.spAccountUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }
        public string AppointmentUpsert(string parameters)
        {
            spAppointmentUpsertParameters casted = JsonConvert.DeserializeObject <spAppointmentUpsertParameters>(parameters);
            spAppointmentUpsertResult result = Calls.spAppointmentUpsertCall(casted, ConnectionString);
            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        public string CustomerUpsert(string parameters)
        {
            spCustomerUpsertParameters casted = JsonConvert.DeserializeObject < spCustomerUpsertParameters>(parameters);
            spCustomerUpsertResult result = Calls.spCustomerUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;

        }

        public string ServiceProvdiderUpsert(string parameters)
        {
            spServiceProviderUpsertParameters casted = JsonConvert.DeserializeObject < spServiceProviderUpsertParameters>(parameters);
            spServiceProviderUpsertResult result = Calls.spServiceProviderUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        public string StoreUpsert(string parameters)
        {
            spStoreUpsertParameters casted = JsonConvert.DeserializeObject < spStoreUpsertParameters>(parameters);
            spStoreUpsertResult result = Calls.spStoreUpsertCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        public string AccountGet(string parameters)
        {
            spAccountGetParameters casted = JsonConvert.DeserializeObject<spAccountGetParameters>(parameters);
            spAccountGetResult result = Calls.spAccountGetCall(casted, ConnectionString);

            string json = JsonConvert.SerializeObject(result);
            return json;
        }
    }
}
