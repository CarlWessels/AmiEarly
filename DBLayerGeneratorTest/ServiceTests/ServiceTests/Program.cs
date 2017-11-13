using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            MerchantLogin("SYSTEM", "PASSWORD");

        }

        public static void MerchantLogin(string username, string password)
        {
            MerchantServiceClient service = new MerchantServiceClient(username, password);
        }

    }
}
