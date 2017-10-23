using AppointmentLibrary;
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
            Guid systemUserGUID = new Guid("605B4BCD-95B5-E711-80C2-0003FF433AE0");
            spAccountUpsertResult  result = AppointmentService.AccountUpsert(null, "The test account", DateTime.Now, null, false, systemUserGUID);
            Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {result.IsDeleted.ToString()}");
            //Console.ReadKey();

            result = AppointmentService.AccountUpsert(result.GUID, "The test account", DateTime.Now, null, true, systemUserGUID);


            spAccountGetResult accountGet = AppointmentService.AccountGet(result.GUID);
            Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {accountGet.IsDeleted.ToString()}");

            result = AppointmentService.AccountUpsert(result.GUID, "The test account", result.ActiveDateTime, null, false, systemUserGUID);

            accountGet = AppointmentService.AccountGet(result.GUID);
            Console.WriteLine($"{result.AccountName} = {result.GUID.ToString()} - Deleted = {accountGet.IsDeleted.ToString()}");

            Console.ReadKey();

        }

       
    }
}
