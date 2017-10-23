namespace AppointmentLibrary.Tables
{
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
		public string AccountGUID {get;set;}
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
	}
	public partial class tblActivityType
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActivityType {get;set;}
		public string AccountGUID {get;set;}
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
	}
	public partial class tblSystemUser
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActiveDateTime {get;set;}
		public string TerminationDateTime {get;set;}
		public string IsActiveForNow {get;set;}
		public string Username {get;set;}
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
}
