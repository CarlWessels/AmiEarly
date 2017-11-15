using AppointmentLibrary;
using AppointmentLibrary.ProcResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

                DateTime now = DateTime.Now;
                now = now + new TimeSpan(-2, 0, 0); // server time difference
                DateTime appointmentstartTime = now + new TimeSpan(0, -30, 0);
                DateTime appointment2startTime = appointmentstartTime + new TimeSpan(0, 30, 0);
                DateTime appointment3startTime = appointment2startTime + new TimeSpan(0, 30, 0);

                DateTime appointmentActualstartTime = appointmentstartTime + new TimeSpan(0, 10, 0);
                DateTime appointmentActualEndTime = appointmentstartTime + new TimeSpan(0, 25, 0);

                //initialize the merchantservice with a username and password for a systemuser, this user will also need the MerchantServiceAccess permission 
                //to open the service. the merchant service, like the customer service, has limited functionality. for this demo, we only expose a couple of necesarry
                //functions. more will be added as needed
                MerchantServiceClient merchanttService = new MerchantServiceClient("SYSTEM", "PASSWORD");


                //each merchant will have his account guid as well as the store's guid stored in his appconfig
                //todo : do checks if the accountguid and storeguid matches the systemuser specified to the service
                Guid accountGUID = new Guid(ConfigurationManager.AppSettings["AccountGUID"]);
                Guid storeGUID = new Guid(ConfigurationManager.AppSettings["StoreGUID"]);


                // we create a new serviceprovider. for now, no checks are being done if the same provider already exists, we just add it blindly. later some more
                // business logic will need to be incorporated in order to check if this already exists
                ServiceProviderUpsertResult serviceProvider = merchanttService.ServiceProviderUpsert(null, false, now, null, "GREEN", "THUMB", accountGUID,storeGUID, true);

                // we add two customers. same as above, no checks are done


                List<SystemUserGetResult> systemUsers = merchanttService.SystemUserGetAsList(null, accountGUID, storeGUID);

                SystemUserGetResult user1 = systemUsers.Where(s => s.Username == "STEWART.RIDGWAY").FirstOrDefault();
                if (user1 == null)
                {
                    SystemUserUpsertResult res = merchanttService.SystemUserUpsert(null, false, now, null, "STEWART.RIDGWAY", "PASSWORD", accountGUID, storeGUID, true);
                    user1 = merchanttService.SystemUserGet(res.GUID, accountGUID, storeGUID);
                }

                SystemUserGetResult user2 = systemUsers.Where(s => s.Username == "CANDICE.RIDGWAY").FirstOrDefault();
                if (user2 == null)
                {
                    SystemUserUpsertResult res = merchanttService.SystemUserUpsert(null, false, now, null, "CANDICE.RIDGWAY", "PASSWORD", accountGUID, storeGUID, true);
                    user2 = merchanttService.SystemUserGet(res.GUID, accountGUID, storeGUID);
                }

                SystemUserGetResult user3 = systemUsers.Where(s => s.Username == "CARL.WESSELS").FirstOrDefault();
                if (user3 == null)
                {
                    SystemUserUpsertResult res = merchanttService.SystemUserUpsert(null, false, now, null, "CARL.WESSELS", "PASSWORD", accountGUID, storeGUID, true);
                    user3 = merchanttService.SystemUserGet(res.GUID, accountGUID, storeGUID);
                }

                List<CustomerGetResult> customers = merchanttService.CustomerGetAsList(null, accountGUID);
                CustomerGetResult customer1 = customers.Where(c => c.Firstname == "STEWART" && c.Surname == "RIDGWAY").FirstOrDefault();
                if (customer1 == null)
                {
                    CustomerUpsertResult res = merchanttService.CustomerUpsert(null, false, now, null, "STEWART", "RIDGWAY", "stewart@gmail.com", "123456789", new DateTime(1983, 1, 1), "0821234556", accountGUID, user1.GUID, true);
                    customer1 = merchanttService.CustomerGet(res.GUID, accountGUID);
                }

                CustomerGetResult customer2 = customers.Where(c => c.Firstname == "CANDICE" && c.Surname == "RIDGWAY").FirstOrDefault();
                if (customer2 == null)
                {
                    CustomerUpsertResult res = merchanttService.CustomerUpsert(null, false, now, null, "CANDICE", "RIDGWAY", "candice@gmail.com", "987654321", new DateTime(1983, 1, 1), "0821234556", accountGUID, user1.GUID, true);
                    customer2 = merchanttService.CustomerGet(res.GUID, accountGUID);
                }

                CustomerGetResult customer3 = customers.Where(c => c.Firstname == "CARL" && c.Surname == "WESSELS").FirstOrDefault();
                if (customer3 == null)
                {
                    CustomerUpsertResult res = merchanttService.CustomerUpsert(null, false, now, null, "CARL", "WESSELS", "carl@gmail.com", "7411258893", new DateTime(1983, 1, 1), "0821234556", accountGUID, user1.GUID, true);
                    customer3 = merchanttService.CustomerGet(res.GUID, accountGUID);
                }

                //we create two appointments, the second starts 30 minutes later than the first, so there should be no delays
                AppointmentUpsertResult appointment = merchanttService.AppointmentUpsert(null, false, appointmentstartTime, new TimeSpan(0,0,30,0,0), null, null, customer1.GUID, storeGUID, serviceProvider.GUID, true);
                AppointmentUpsertResult appointment2 = merchanttService.AppointmentUpsert(null, false, appointment2startTime, new TimeSpan(0, 0, 30, 0, 0), null, null, customer2.GUID, storeGUID, serviceProvider.GUID, true);
                AppointmentUpsertResult appointment3 = merchanttService.AppointmentUpsert(null, false, appointment3startTime, new TimeSpan(0, 0, 30, 0, 0), null, null, customer3.GUID, storeGUID, serviceProvider.GUID, true);


                // the first customer arrives 10 minutes late, and the appointment starts
                appointment = merchanttService.AppointmentUpsert(appointment.GUID, false, appointment.StartDateTime, appointment.Duration, appointmentActualstartTime, null, customer1.GUID, storeGUID, serviceProvider.GUID, true);


                //because the first appointment is delayed, the second's predicted start time will also be delayed with 10 minutes
                AppointmentGetResult afterStart = merchanttService.AppointmentGet(appointment2.GUID);

                
                appointment = merchanttService.AppointmentUpsert(appointment.GUID, false, appointment.StartDateTime, appointment.Duration, appointmentActualstartTime, appointmentActualEndTime, customer1.GUID, storeGUID, serviceProvider.GUID, true);
                //the first appointment ends in time, and the predicted delay is rolled back. the second appointment is still on time
                AppointmentGetResult afterEnd = merchanttService.AppointmentGet(appointment2.GUID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            Console.ReadKey();

        }
       
    }
}
