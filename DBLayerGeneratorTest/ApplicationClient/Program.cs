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
            Guid systemUserGUID = new Guid("5F6106EF-DBB7-E711-80C2-0003FF433AE0");
            List<spAccountUpsertResult>  results = AppointmentService.AccountUpsert(null, "The test account", DateTime.Now, null, false, systemUserGUID);
            spAccountUpsertResult result = results.FirstOrDefault();
            Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {result.IsDeleted.ToString()}");
            //Console.ReadKey();

            results = AppointmentService.AccountUpsert(result.GUID, "The test account", DateTime.Now, null, true, systemUserGUID);


            List<spAccountGetResult> accountGets = AppointmentService.AccountGet(result.GUID);
            foreach (spAccountGetResult ag in accountGets)
            {
                Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {ag.IsDeleted.ToString()}");
            }
            results = AppointmentService.AccountUpsert(result.GUID, "The test account", result.ActiveDateTime, null, false, systemUserGUID);


            spAccountGetResult accountGet = AppointmentService.AccountGet(result.GUID).FirstOrDefault();
            Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {accountGet.IsDeleted.ToString()}");


            List<spAppointmentGetResult> appGet = AppointmentService.AppointmentGet(null);

            foreach (spAppointmentGetResult res in appGet)
            {
                Console.WriteLine($"{res.StartDateTime.ToString()}\t{res.EndDateTime}\t{res.ExpectedDelay}");
            }
            

            Console.ReadKey();

        }

       
    }
}
