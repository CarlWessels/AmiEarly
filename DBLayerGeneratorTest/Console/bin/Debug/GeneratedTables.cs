namespace AppointmentLibrary.Tables
{
	public partial class tblSystemUser
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string Token {get;set;}
		public string TokenExpires {get;set;}
		public string TokenIsValid {get;set;}
		public string Username {get;set;}
		public string PasswordHash {get;set;}
		public string PasswordSalt {get;set;}
	}
	public partial class tblAccount
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string AccountName {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblStore
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string StoreName {get;set;}
		public string AccountGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblCustomer
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string Firstname {get;set;}
		public string Surname {get;set;}
		public string EmailAddress {get;set;}
		public string IDNumber {get;set;}
		public string BirthDate {get;set;}
		public string CellphoneNumber {get;set;}
		public string AccountGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblLUAddressType
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string AddressType {get;set;}
	}
	public partial class tblCustomerAddress
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string Address1 {get;set;}
		public string Address2 {get;set;}
		public string Address3 {get;set;}
		public string Code {get;set;}
		public string Province {get;set;}
		public string CustomerGUID {get;set;}
		public string AddressTypeGUID {get;set;}
	}
	public partial class tblServiceProvider
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string Firstname {get;set;}
		public string Surname {get;set;}
		public string AccountGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblAppointment
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string StartDateTime {get;set;}
		public string EndDateTime {get;set;}
		public string Duration {get;set;}
		public string ActualStartDateTime {get;set;}
		public string ActualEndDateTime {get;set;}
		public string CustomerGUID {get;set;}
		public string StoreGUID {get;set;}
		public string ServiceProviderGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblLUActivityType
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActivityType {get;set;}
		public string AccountGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblActivitySchedule
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string DoW {get;set;}
		public string StartTime {get;set;}
		public string EndTime {get;set;}
		public string ActivityTypeGUID {get;set;}
		public string ServiceProviderGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblAuditLog
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string Source {get;set;}
		public string TableGUID {get;set;}
		public string TableName {get;set;}
		public string BeforeSnapshot {get;set;}
		public string AfterSnapshot {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblLUPermission
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string Permission {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblSystemUserPermission
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string ForSystemUserGUID {get;set;}
		public string PermissionGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tbltLUActionDateTimeType
	{
		public string LUActionDateTimeTypeId {get;set;}
		public string Description {get;set;}
	}
	public partial class tblSystemUserGroup
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string Description {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblSystemUserGroupPermission
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string PermissionGUID {get;set;}
		public string SystemUserGroupGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tblSystemUserGroupLine
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string ForSystemUserGUID {get;set;}
		public string SystemUserGroupGUID {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class tbltCutOffDateAlgorithm
	{
		public string CutOffDateAlgorithmId {get;set;}
		public string ServiceID {get;set;}
		public string AllowSunday {get;set;}
		public string AllowSaturday {get;set;}
		public string AllowPublicHoliday {get;set;}
		public string BankPresendationHourAdd {get;set;}
		public string BankPresendationDayAdd {get;set;}
		public string CutOffHourAdd {get;set;}
		public string CutOffDayAdd {get;set;}
	}
	public partial class tblMyTestTable
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string MyTestField {get;set;}
	}
	public partial class tblSetting
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string Setting {get;set;}
		public string TestValue {get;set;}
		public string ProductionValue {get;set;}
		public string IsProduction {get;set;}
		public string Value {get;set;}
	}
}
