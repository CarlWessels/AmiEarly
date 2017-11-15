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
    public class MerchantServiceClient
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

        public SystemUserUpsertResult SystemUserUpsert(Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string username, string password, Guid accountGUID, Guid storeGUID, bool? returnResults)
        {
            SystemUserUpsertParameters p = new SystemUserUpsertParameters()
            {
                Token = Token
                ,GUID = gUID
                ,IsDeleted = isDeleted
                ,ActiveDateTime = activeDateTime
                ,TerminationDateTime = terminationDateTime
                ,Username = username
                ,Password = password
                ,AccountGUID = accountGUID
                ,StoreGUID = storeGUID
                ,ReturnResults = returnResults
            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.SystemUserUpsert(parameters);
            SystemUserUpsertResult result = JsonConvert.DeserializeObject<SystemUserUpsertResult>(resultStr);
            return result;
        }
        public CustomerUpsertResult CustomerUpsert (Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string firstname, string surname, string emailAddress, string iDNumber, DateTime? birthDate, string cellphoneNumber, Guid? accountGUID, Guid linkedSystemUserGUID, bool? returnResults)
		{
			CustomerUpsertParameters p = new CustomerUpsertParameters()
			{
				Token = Token,
				GUID = gUID
				, IsDeleted = isDeleted
				, ActiveDateTime = activeDateTime
				, TerminationDateTime = terminationDateTime
				, Firstname = firstname
				, Surname = surname
				, EmailAddress = emailAddress
				, IDNumber = iDNumber
				, BirthDate = birthDate
				, CellphoneNumber = cellphoneNumber
				, AccountGUID = accountGUID
                , LinkedSystemUserGUID = linkedSystemUserGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.CustomerUpsert(parameters);
			CustomerUpsertResult result = JsonConvert.DeserializeObject<CustomerUpsertResult>(resultStr);
			return result;
		}

        public ServiceProviderUpsertResult ServiceProviderUpsert (Guid? gUID, bool? isDeleted, DateTime? activeDateTime, DateTime? terminationDateTime, string firstname, string surname, Guid? accountGUID, Guid storeGUID, bool? returnResults)
		{
			ServiceProviderUpsertParameters p = new ServiceProviderUpsertParameters()
			{
				Token = Token,
				GUID = gUID
				, IsDeleted = isDeleted
				, ActiveDateTime = activeDateTime
				, TerminationDateTime = terminationDateTime
				, Firstname = firstname
				, Surname = surname
				, AccountGUID = accountGUID
                , StoreGUID = storeGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.ServiceProviderUpsert(parameters);
			ServiceProviderUpsertResult result = JsonConvert.DeserializeObject<ServiceProviderUpsertResult>(resultStr);
			return result;
		}

        public AppointmentUpsertResult AppointmentUpsert (Guid? gUID, bool? isDeleted, DateTime? startDateTime, TimeSpan? duration, DateTime? actualStartDateTime, DateTime? actualEndDateTime, Guid? customerGUID, Guid? storeGUID, Guid? serviceProviderGUID, bool? returnResults)
		{
			AppointmentUpsertParameters p = new AppointmentUpsertParameters()
			{
				Token = Token,
				GUID = gUID
				, IsDeleted = isDeleted
				, StartDateTime = startDateTime
				, Duration = duration
				, ActualStartDateTime = actualStartDateTime
				, ActualEndDateTime = actualEndDateTime
				, CustomerGUID = customerGUID
				, StoreGUID = storeGUID
				, ServiceProviderGUID = serviceProviderGUID
				, ReturnResults = returnResults

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.AppointmentUpsert(parameters);
			AppointmentUpsertResult result = JsonConvert.DeserializeObject<AppointmentUpsertResult>(resultStr);
			return result;
		}


        /*public List<AppointmentGetResult> AppointmentGetAsList  (Guid? appointmentGUID)
		{
			AppointmentGetParameters p = new AppointmentGetParameters()
			{
				Token = Token,
				AppointmentGUID = appointmentGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.AppointmentGetAsList(parameters);
			List<AppointmentGetResult> result = JsonConvert.DeserializeObject<List<AppointmentGetResult>>(resultStr);
			return result;
		}*/
        public AppointmentGetResult AppointmentGet(Guid? appointmentGUID)
        {
            AppointmentGetParameters p = new AppointmentGetParameters()
            {
                Token = Token,
                AppointmentGUID = appointmentGUID

            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.AppointmentGet(parameters);
            AppointmentGetResult result = JsonConvert.DeserializeObject<AppointmentGetResult>(resultStr);
            return result;
        }

        public List<CustomerGetResult> CustomerGetAsList(Guid? customerGUID, Guid? accountGUID)
        {
            CustomerGetParameters p = new CustomerGetParameters()
            {
                Token = Token,
                CustomerGUID = customerGUID
                ,AccountGUID = accountGUID

            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.CustomerGetAsList(parameters);
            List<CustomerGetResult> result = JsonConvert.DeserializeObject<List<CustomerGetResult>>(resultStr);
            return result;
        }

        public CustomerGetResult CustomerGet(Guid? customerGUID, Guid? accountGUID)
        {
            CustomerGetParameters p = new CustomerGetParameters()
            {
                Token = Token,
                CustomerGUID = customerGUID
                ,
                AccountGUID = accountGUID

            };

            string parameters = JsonConvert.SerializeObject(p);
            string resultStr = Service.CustomerGet(parameters);
            CustomerGetResult result = JsonConvert.DeserializeObject<CustomerGetResult>(resultStr);
            return result;
        }

        public List<SystemUserGetResult> SystemUserGetAsList  (Guid? forSystemUserGUID, Guid? accountGUID, Guid? storeGUID)
		{
			SystemUserGetParameters p = new SystemUserGetParameters()
			{
				Token = Token,
				ForSystemUserGUID = forSystemUserGUID
				, AccountGUID = accountGUID
				, StoreGUID = storeGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.SystemUserGetAsList(parameters);
			List<SystemUserGetResult> result = JsonConvert.DeserializeObject<List<SystemUserGetResult>>(resultStr);
			return result;
		}
		public SystemUserGetResult SystemUserGet (Guid? forSystemUserGUID, Guid? accountGUID, Guid? storeGUID)
		{
			SystemUserGetParameters p = new SystemUserGetParameters()
			{
				Token = Token,
				ForSystemUserGUID = forSystemUserGUID
				, AccountGUID = accountGUID
				, StoreGUID = storeGUID

			};
			
			string parameters = JsonConvert.SerializeObject(p);
			string resultStr = Service.SystemUserGet(parameters);
			SystemUserGetResult result = JsonConvert.DeserializeObject<SystemUserGetResult>(resultStr);
			return result;
		}
    }
}
