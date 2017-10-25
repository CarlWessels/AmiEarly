using AppointmentLibrary;
using AppointmentLibrary.ProcResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppointmentServiceClient appointmentService = new AppointmentServiceClient("SYSTEM", "PASSWORD");
                List<spSystemUserGetResult> users = AppointmentServiceClient.SystemUserGet(null);

                spSystemUserGetResult systemUser = users.Where(u => u.Username == "SYSTEM").FirstOrDefault();
                Guid systemGUID = users.Where(u => u.Username == "SYSTEM").FirstOrDefault().GUID;


                Guid? testerGUID = null;
                spSystemUserGetResult tester = users.Where(u => u.Username == "TESTER").FirstOrDefault();
                if (tester == null)
                {
                    List<spSystemUserUpsertResult> user = AppointmentServiceClient.SystemUserUpsert(null, false, DateTime.Now,null,"TESTER", "PASSWORD", systemGUID, true);
                    testerGUID = user.FirstOrDefault().GUID;
                }

                List<spAccountUpsertResult>  results = AppointmentServiceClient.AccountUpsert(null, true, DateTime.Now, null, "The test account", testerGUID, true);
                spAccountUpsertResult result = results.FirstOrDefault();
                Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {result.IsDeleted.ToString()}");

                results = AppointmentServiceClient.AccountUpsert(result.GUID, false, DateTime.Now, null, "The test account", testerGUID, true);

                List<spAccountGetResult> accountGets = AppointmentServiceClient.AccountGet(result.GUID);
                foreach (spAccountGetResult ag in accountGets)
                {
                    Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {ag.IsDeleted.ToString()}");
                }
                AppointmentServiceClient.AccountUpsert(result.GUID, true, DateTime.Now, null, "The test account", testerGUID, true);



                spAccountGetResult accountGet = AppointmentServiceClient.AccountGet(result.GUID).FirstOrDefault();
                Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {accountGet.IsDeleted.ToString()}");


                List<spAppointmentGetResult> appGet = AppointmentServiceClient.AppointmentGet(null);

                foreach (spAppointmentGetResult res in appGet)
                {
                    Console.WriteLine($"{res.StartDateTime.ToString()}\t{res.EndDateTime}\t{res.ExpectedDelay}");
                }
            }
            catch (Exception ex)
            {

            }
            
            Console.ReadKey();

        }
       
    }
}
