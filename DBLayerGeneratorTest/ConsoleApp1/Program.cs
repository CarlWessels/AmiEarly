using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string connectionString = @"Server=tcp:carlwessels.database.windows.net,1433;Initial Catalog=TestDb;Persist Security Info=False;User ID=carlwessels;Password=p@551234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            TestCall(connectionString);*/
        }
        /*
        public static void TestCall(string connectionString)
        {

            Guid systemUserGUID = new Guid();
            spAccountUpsertResult accountResult = Calls.spAccountUpsertCall(null, false, DateTime.Now,null,"Test account 123", systemUserGUID, true , connectionString);

            spStoreUpsertResult storeResult = Calls.spStoreUpsertCall(null, false, DateTime.Now, null, "Store 123", accountResult.GUID, systemUserGUID, true, connectionString);

            spCustomerUpsertResult customerResult = Calls.spCustomerUpsertCall(null, false, DateTime.Now, null, "Pieter", "Uys", accountResult.GUID, systemUserGUID, true, connectionString);

            spServiceProviderUpsertResult serviceProviderResults = Calls.spServiceProviderUpsertCall(null, false, DateTime.Now, null, "Doctor", "Greenthumb", accountResult.GUID, systemUserGUID, true, connectionString);

            spAppointmentUpsertResult appointmentResult = Calls.spAppointmentUpsertCall(null, false, new DateTime(2018,01,01), new TimeSpan(0, 30, 0), null, null, customerResult.GUID, storeResult.GUID, serviceProviderResults.GUID, systemUserGUID, true, connectionString);

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    */
    }
}
