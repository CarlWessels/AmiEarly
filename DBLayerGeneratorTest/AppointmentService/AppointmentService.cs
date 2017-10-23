using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AppointmentService
{
    
    public class AppointmentService : AppointmentLibrary.AppointmentService
    {
        public static string DefaultConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["connectionString"];
            }
        }

        public AppointmentService() : base(DefaultConnectionString)
        {
        }
        public AppointmentService(string connectionString) : base(connectionString)
        { }

    }
}
