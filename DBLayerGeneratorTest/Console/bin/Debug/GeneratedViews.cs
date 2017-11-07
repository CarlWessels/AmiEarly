namespace AppointmentLibrary.Views
{
	public partial class vwAccount
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
	public partial class vwActivityType
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string ActivityType {get;set;}
		public string AccountGUID {get;set;}
	}
	public partial class vwServiceProvider
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
	public partial class vwCustomer
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
	public partial class vwActivitySchedule
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
	public partial class vwStore
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
	public partial class vwSystemUser
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
	}
	public partial class vwPermission
	{
		public string GUID {get;set;}
		public string ID {get;set;}
		public string DateTimeCreated {get;set;}
		public string IsDeleted {get;set;}
		public string Permission {get;set;}
		public string SystemUserGUID {get;set;}
	}
	public partial class vwSystemUserPermission
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
		public string Username {get;set;}
		public string Permission {get;set;}
	}
	public partial class vwSystemUserGroup
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
	public partial class vwSystemUserGroupPermission
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
	public partial class vwAppointment
	{
		public string GUID {get;set;}
		public string ServiceProviderFirstname {get;set;}
		public string ServiceProviderSurname {get;set;}
		public string CustomerFirstname {get;set;}
		public string CustomerSurname {get;set;}
		public string StartDateTime {get;set;}
		public string EndDateTime {get;set;}
		public string Duration {get;set;}
		public string ActualStartDateTime {get;set;}
		public string ActualEndDateTime {get;set;}
		public string ActualDuration {get;set;}
		public string DelayTime {get;set;}
		public string ExpectedDelay {get;set;}
		public string ExpectedStartDateTime {get;set;}
		public string Colour {get;set;}
	}
}
