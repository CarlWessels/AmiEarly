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
            //try
            {
                AppointmentServiceClient merchanttService = new AppointmentServiceClient("TESTMERCHANT", "TESTMERCHANT");




                /*List<SystemUserGetResult> users = appointmentService.SystemUserGet(null);


                Guid? testerGUID = null;
                SystemUserGetResult tester = users.Where(u => u.Username == "TESTER").FirstOrDefault();
                if (tester == null)
                {
                    List<SystemUserUpsertResult> user = appointmentService.SystemUserUpsert(null, false, DateTime.Now,null,"TEaSTER", "PASSWORD", true);
                    testerGUID = user.FirstOrDefault().GUID;
                }
                else
                {
                    testerGUID = tester.GUID;
                }

                List<LUPermissionGetResult> permissions = appointmentService.LUPermissionGet(null);
                Guid accountInsertPermissionGUID = permissions.Where(p => p.Permission == "AccountInsert").FirstOrDefault().GUID;
                Guid accountUpdatePermissionGUID = permissions.Where(p => p.Permission == "AccountUpdate").FirstOrDefault().GUID;

                appointmentService.SystemUserPermissionUpsert(null, false, DateTime.Now, null, testerGUID, accountInsertPermissionGUID, true);
                appointmentService.SystemUserPermissionUpsert(null, false, DateTime.Now, null, testerGUID, accountUpdatePermissionGUID, true);


                List<AppointmentGetResult> appGet = appointmentService.AppointmentGet(null);

                foreach (AppointmentGetResult res in appGet)
                {
                    Console.WriteLine($"{res.StartDateTime.ToString()}\t{res.EndDateTime}\t{res.ExpectedDelay}");
                }*/

            }
            /*catch (Exception ex)
            {

            }*/
            
            Console.ReadKey();

        }
       
    }
}
