using AppointmentLibrary;
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
    public class AppointmentService
    {
        private static AppointmentServiceClient.AppointmentServiceClient client;

        public static AppointmentServiceClient.AppointmentServiceClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new AppointmentServiceClient.AppointmentServiceClient();
                }
                return client;
            }
        }

        public static List<spAccountUpsertResult> AccountUpsert(Guid? guid, string name, DateTime activeDateTime, DateTime? terminationDateTime, bool isDeleted, Guid systemUserGUID)
        {
            spAccountUpsertParameters p = new spAccountUpsertParameters()
            {
                GUID = guid,
                AccountName = name,
                IsDeleted = isDeleted,
                ActiveDateTime = activeDateTime,
                TerminationDateTime = terminationDateTime,
                SystemUserGUID = systemUserGUID,
                ReturnResults = true
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Client.AccountUpsert(parameters);
            List<spAccountUpsertResult> result = JsonConvert.DeserializeObject<List<spAccountUpsertResult>>(resultStr);
            return result;
        }

        public static List<spAccountGetResult> AccountGet(Guid? guid)
        {
            spAccountGetParameters p = new spAccountGetParameters()
            {
                AccountGUID = guid
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Client.AccountGet(parameters);
            List<spAccountGetResult> result = JsonConvert.DeserializeObject<List<spAccountGetResult>>(resultStr);
            return result;
        }

        public static List<spAppointmentGetResult> AppointmentGet(Guid? guid)
        {
            spAppointmentGetParameters p = new spAppointmentGetParameters()
            {
                AppointmentGUID = guid
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Client.AppointmentGet(parameters);

            List<spAppointmentGetResult> result = JsonConvert.DeserializeObject<List<spAppointmentGetResult>>(resultStr);
            return result;

        }

    }
}
