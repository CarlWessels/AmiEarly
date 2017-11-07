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



                List<LoginResult> loginResults = AppointmentServiceClient.Login("SYSTEM", "PASSWORD");
                LoginResult login = loginResults.FirstOrDefault();
                byte[] token = login.Token;

                List<SystemUserGetResult> users = AppointmentServiceClient.SystemUserGet(null, token);


                Guid? testerGUID = null;
                SystemUserGetResult tester = users.Where(u => u.Username == "TESTER").FirstOrDefault();
                if (tester == null)
                {
                    List<SystemUserUpsertResult> user = AppointmentServiceClient.SystemUserUpsert(null, false, DateTime.Now,null,"TESTER", "PASSWORD", token, true);
                    testerGUID = user.FirstOrDefault().GUID;
                }
                else
                {
                    testerGUID = tester.GUID;
                }

/*                List<PermissionGetResult> permissions = AppointmentServiceClient.PermissionGet(null, systemGUID);
                Guid accountInsertPermissionGUID = permissions.Where(p => p.Permission == "AccountInsert").FirstOrDefault().GUID;
                Guid accountUpdatePermissionGUID = permissions.Where(p => p.Permission == "AccountUpdate").FirstOrDefault().GUID;

                AppointmentServiceClient.SystemUserPermissionUpsert(null, false, DateTime.Now, null, testerGUID, accountInsertPermissionGUID, systemGUID, true);
                AppointmentServiceClient.SystemUserPermissionUpsert(null, false, DateTime.Now, null, testerGUID, accountUpdatePermissionGUID, systemGUID, true);

                List<AccountUpsertResult>  results = AppointmentServiceClient.AccountUpsert(null, true, DateTime.Now, null, "The test account", testerGUID, true);
                AccountUpsertResult result = results.FirstOrDefault();
                Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {result.IsDeleted.ToString()}");

                results = AppointmentServiceClient.AccountUpsert(result.GUID, false, DateTime.Now, null, "The test account", testerGUID, true);

                List<AccountGetResult> accountGets = AppointmentServiceClient.AccountGet(result.GUID, systemGUID);
                foreach (AccountGetResult ag in accountGets)
                {
                    Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {ag.IsDeleted.ToString()}");
                }
                AppointmentServiceClient.AccountUpsert(result.GUID, true, DateTime.Now, null, "The test account", testerGUID, true);



                AccountGetResult accountGet = AppointmentServiceClient.AccountGet(result.GUID, systemGUID).FirstOrDefault();
                Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {accountGet.IsDeleted.ToString()}");


                List<AppointmentGetResult> appGet = AppointmentServiceClient.AppointmentGet(null, systemGUID);

                foreach (AppointmentGetResult res in appGet)
                {
                    Console.WriteLine($"{res.StartDateTime.ToString()}\t{res.EndDateTime}\t{res.ExpectedDelay}");
                }
                */
            }
            catch (Exception ex)
            {

            }
            
            Console.ReadKey();

        }
       
    }
}
