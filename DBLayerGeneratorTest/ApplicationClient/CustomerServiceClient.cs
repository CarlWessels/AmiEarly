using AppointmentLibrary.Parameters;
using AppointmentLibrary.ProcResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationClient
{
    public class CustomerServiceClient
    {
        public CustomerServiceClient(string username, string password)
        {
            service = new CustomerService.CustomerServiceClient();
            service.ClientCredentials.UserName.UserName = username;
            service.ClientCredentials.UserName.Password = password;
            LoginResult login = Login(username, password);
            Guid token = login.Token;
            Token = token;
        }
        private static CustomerService.CustomerServiceClient service;

        public Guid Token;
        public static CustomerService.CustomerServiceClient Service
        {
            get
            {
                return service;
            }
        }

        public LoginResult Login(string userName, string password)
        {
            try
            {
                LoginParameters p = new LoginParameters()
                {
                    UserName = userName
                    ,
                    Password = password

                };

                string parameters = JsonConvert.SerializeObject(p);
                string resultStr = Service.Login(parameters);
                LoginResult result = JsonConvert.DeserializeObject<LoginResult>(resultStr);
                return result;
            }
            catch (FaultException fex)
            {
                throw fex.InnerException;
            }
        }
        public RefreshTokenNoReturnResult RefreshTokenNoReturn(Guid? systemUserGUID)
        {
            RefreshTokenNoReturnParameters p = new RefreshTokenNoReturnParameters()
            {
                SystemUserGUID = systemUserGUID

            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.RefreshTokenNoReturn(parameters);
            RefreshTokenNoReturnResult result = JsonConvert.DeserializeObject<RefreshTokenNoReturnResult>(resultStr);
            return result;
        }
    }

}
