using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using AppointmentLibrary.ProcResults;
using System.IdentityModel.Tokens;
using AppointmentLibrary.Calls;
using System.Web.Configuration;
using ApplicationClient.Enums;
using System.ServiceModel;
using ServiceLibrary;

namespace MerchantService
{
    public class MerchantAuthenticator : BaseAuthenticator
    {
        public override Guid PermissionGUID()
        {
            Guid permissionGUID = LUPermission.MerchantServiceAccess.GUID();
            return permissionGUID;
        }
        
    }
}