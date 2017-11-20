using ApplicationClient.Enums;
using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService
{
    public class CustomerAuthenticator : BaseAuthenticator
    {
        public override Guid PermissionGUID()
        {
            Guid permissionGUID = LUPermission.CustomerServiceAccess.GUID();
            return permissionGUID;
        }
    }
}