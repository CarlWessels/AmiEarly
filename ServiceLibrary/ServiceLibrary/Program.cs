using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    class Program
    {
        public static void Main()
        {
            FlaggedServiceCreator merchantCreator = new FlaggedServiceCreator("MerchantService", typeof(IsMerchantService));
            merchantCreator.Create();

            FlaggedServiceCreator customerCreator = new FlaggedServiceCreator("CustomerService", typeof(IsCustomerService));
            customerCreator.Create();
        }
    }
}
