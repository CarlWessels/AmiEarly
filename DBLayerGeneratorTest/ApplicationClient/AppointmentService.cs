using AppointmentLibrary;
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

        public static spAccountUpsertResult AccountUpsert(Guid? guid, string name, DateTime activeDateTime, DateTime? terminationDateTime, bool isDeleted, Guid systemUserGUID)
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
            spAccountUpsertResult result = JsonConvert.DeserializeObject<spAccountUpsertResult>(resultStr);
            return result;
        }

        public static spAccountGetResult AccountGet(Guid guid)
        {
            spAccountGetParameters p = new spAccountGetParameters()
            {
                AccountGUID = guid
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Client.AccountGet(parameters);
            spAccountGetResult result = JsonConvert.DeserializeObject<spAccountGetResult>(resultStr);
            return result;
        }

    }
}
