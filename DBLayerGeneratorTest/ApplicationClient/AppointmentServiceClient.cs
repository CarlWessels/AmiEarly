/*using AppointmentLibrary;
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
    public class AppointmentServiceClient_old
    {
        public AppointmentServiceClient_old(string username, string password)
        {
            /*service = new AppointmentService.AppointmentServiceClient();
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

        public static List<spAccountUpsertResult> AccountUpsert(Guid? guid, string name, DateTime activeDateTime, DateTime? terminationDateTime, bool isDeleted, Guid systemUserGUID, bool returnResults)
        {
            spAccountUpsertParameters p = new spAccountUpsertParameters()
            {
                GUID = guid,
                AccountName = name,
                IsDeleted = isDeleted,
                ActiveDateTime = activeDateTime,
                TerminationDateTime = terminationDateTime,
                SystemUserGUID = systemUserGUID,
                ReturnResults = returnResults
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.AccountUpsert(parameters);
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
            string resultStr = Service.AccountGet(parameters);
            List<spAccountGetResult> result = JsonConvert.DeserializeObject<List<spAccountGetResult>>(resultStr);
            return result;
        }

        public static List<spStoreUpsertResult> StoreUpsert(Guid guid, bool isDeleted, DateTime activeDateTime, DateTime? terminationDate, string storeName, Guid accountGUID, Guid systemUserGUID, bool returnResults)
        {
            spStoreUpsertParameters p = new spStoreUpsertParameters()
            {
                GUID = guid
                , IsDeleted = isDeleted
                , ActiveDateTime = activeDateTime
                , TerminationDateTime = terminationDate
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

        public static List<spStoreGetResult> StoreGet(Guid guid)
        {
            spStoreGetParameters p = new spStoreGetParameters()
            {
                StoreGUID = guid
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.StoreGet(parameters);
            List<spStoreGetResult> result = JsonConvert.DeserializeObject<List<spStoreGetResult>>(resultStr);
            return result;
        }

        public static async Task<List<spStoreGetResult>> StoreGetAsync(Guid? guid)
        {
            spStoreGetParameters p = new spStoreGetParameters()
            {
                StoreGUID = guid
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = await Service.StoreGetAsync(parameters);
            List<spStoreGetResult> result = JsonConvert.DeserializeObject<List<spStoreGetResult>>(resultStr);
            return result;
        }

        public static List<spAppointmentGetResult> AppointmentGet(Guid? guid)
        {
            spAppointmentGetParameters p = new spAppointmentGetParameters()
            {
                AppointmentGUID = guid
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.AppointmentGet(parameters);

            List<spAppointmentGetResult> result = JsonConvert.DeserializeObject<List<spAppointmentGetResult>>(resultStr);
            return result;

        }

        public static string ThrowError()
        {
            Service.ThrowError("test");
            return "";
        }

    }
}
*/