//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using AppointmentLibrary.Parameters;
using AppointmentLibrary.ProcResults;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationClient.Enums
{
     class EnumGUID : Attribute
     {
         public Guid Guid;
     
         public EnumGUID(string guid)
         {
             Guid = new Guid(guid);
         }
     }

     public static class EnumHelper
     {
         public static Guid GUID(this Enum e)
         {
             Type type = e.GetType();
     
             MemberInfo[] memInfo = type.GetMember(e.ToString());
     
             if (memInfo != null && memInfo.Length > 0)
             {
                 object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumGUID), false);
                 if (attrs != null && attrs.Length > 0)
                     return ((EnumGUID)attrs[0]).Guid;
             }
     
                throw new ArgumentException("Enum " + e.ToString() + " has no EnumGUID defined!"); 
         }
     }
    public enum LUActivityType
    {
        [EnumGUID("75026ffe-4ec5-e711-80c2-0003ff433ae0")]
        Consultations = 1,
        [EnumGUID("77026ffe-4ec5-e711-80c2-0003ff433ae0")]
        Rounds = 2,
     }
    public enum LUAddressType
    {
        [EnumGUID("53026ffe-4ec5-e711-80c2-0003ff433ae0")]
        HOME = 1,
        [EnumGUID("54026ffe-4ec5-e711-80c2-0003ff433ae0")]
        WORK = 2,
     }
    public enum LUPermission
    {
        [EnumGUID("9c016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUPermissionInsert = 1,
        [EnumGUID("9d016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUPermissionUpdate = 2,
        [EnumGUID("9e016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUPermissionGet = 3,
        [EnumGUID("9f016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUPermissionGetAll = 4,
        [EnumGUID("a0016ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserUpsert = 5,
        [EnumGUID("a1016ffe-4ec5-e711-80c2-0003ff433ae0")]
        ServiceAccess = 6,
        [EnumGUID("a2016ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserPermissionInsert = 7,
        [EnumGUID("a3016ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserPermissionUpdate = 8,
        [EnumGUID("a4016ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserPermissionGet = 9,
        [EnumGUID("a5016ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserPermissionGetAll = 10,
        [EnumGUID("a6016ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserGetAll = 11,
        [EnumGUID("a7016ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserGet = 12,
        [EnumGUID("b3016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AccountInsert = 13,
        [EnumGUID("b5016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AccountUpdate = 14,
        [EnumGUID("b7016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AccountGet = 15,
        [EnumGUID("b9016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AccountGetAll = 16,
        [EnumGUID("c3016ffe-4ec5-e711-80c2-0003ff433ae0")]
        ActivityScheduleInsert = 17,
        [EnumGUID("c5016ffe-4ec5-e711-80c2-0003ff433ae0")]
        ActivityScheduleUpdate = 18,
        [EnumGUID("c7016ffe-4ec5-e711-80c2-0003ff433ae0")]
        ActivityScheduleGet = 19,
        [EnumGUID("c9016ffe-4ec5-e711-80c2-0003ff433ae0")]
        ActivityScheduleGetAll = 20,
        [EnumGUID("d3016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUActivityTypeInsert = 21,
        [EnumGUID("d5016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUActivityTypeUpdate = 22,
        [EnumGUID("d7016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUActivityTypeGet = 23,
        [EnumGUID("d9016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUActivityTypeGetAll = 24,
        [EnumGUID("e3016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUAddressTypeInsert = 25,
        [EnumGUID("e5016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUAddressTypeUpdate = 26,
        [EnumGUID("e7016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUAddressTypeGet = 27,
        [EnumGUID("e9016ffe-4ec5-e711-80c2-0003ff433ae0")]
        LUAddressTypeGetAll = 28,
        [EnumGUID("f3016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AppointmentInsert = 29,
        [EnumGUID("f5016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AppointmentUpdate = 30,
        [EnumGUID("f7016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AppointmentGet = 31,
        [EnumGUID("f9016ffe-4ec5-e711-80c2-0003ff433ae0")]
        AppointmentGetAll = 32,
        [EnumGUID("03026ffe-4ec5-e711-80c2-0003ff433ae0")]
        ServiceProviderInsert = 33,
        [EnumGUID("05026ffe-4ec5-e711-80c2-0003ff433ae0")]
        ServiceProviderUpdate = 34,
        [EnumGUID("07026ffe-4ec5-e711-80c2-0003ff433ae0")]
        ServiceProviderGet = 35,
        [EnumGUID("09026ffe-4ec5-e711-80c2-0003ff433ae0")]
        ServiceProviderGetAll = 36,
        [EnumGUID("13026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerInsert = 37,
        [EnumGUID("15026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerUpdate = 38,
        [EnumGUID("17026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerGet = 39,
        [EnumGUID("19026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerGetAll = 40,
        [EnumGUID("23026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerAddressInsert = 41,
        [EnumGUID("25026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerAddressUpdate = 42,
        [EnumGUID("27026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerAddressGet = 43,
        [EnumGUID("29026ffe-4ec5-e711-80c2-0003ff433ae0")]
        CustomerAddressGetAll = 44,
        [EnumGUID("33026ffe-4ec5-e711-80c2-0003ff433ae0")]
        StoreInsert = 45,
        [EnumGUID("35026ffe-4ec5-e711-80c2-0003ff433ae0")]
        StoreUpdate = 46,
        [EnumGUID("37026ffe-4ec5-e711-80c2-0003ff433ae0")]
        StoreGet = 47,
        [EnumGUID("39026ffe-4ec5-e711-80c2-0003ff433ae0")]
        StoreGetAll = 48,
        [EnumGUID("43026ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserGroupInsert = 49,
        [EnumGUID("45026ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserGroupUpdate = 50,
        [EnumGUID("47026ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserGroupGet = 51,
        [EnumGUID("49026ffe-4ec5-e711-80c2-0003ff433ae0")]
        SystemUserGroupGetAll = 52,
     }
}
