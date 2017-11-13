using AppointmentLibrary.Parameters;
using AppointmentLibrary.ProcResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    public partial class MerchantServiceClient
    {
        public MerchantServiceClient(string username, string password)
        {
            service = new MerchantService.MerchantServiceClient();
            service.ClientCredentials.UserName.UserName = username;
            service.ClientCredentials.UserName.Password = password;
            LoginResult login = Login(username, password);
            Guid token = login.Token;
            Token = token;
        }
        private static MerchantService.MerchantServiceClient service;

        public Guid Token;
        public static MerchantService.MerchantServiceClient Service
        {
            get
            {
                return service;
            }
        }

        public LoginResult Login(string userName, string password)
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
    }
}
