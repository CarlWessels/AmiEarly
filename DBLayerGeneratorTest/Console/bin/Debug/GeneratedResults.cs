namespace AppointmentLibrary.ProcResults
{
	using System;
	public partial class spAccountUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string AccountName {get;set;}
	}
	public partial class spAccountGetResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string AccountName {get;set;}
	}
	public partial class spActivityTypeUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public string ActivityType {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spActivityTypeGetResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public string ActivityType {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spAppointmentUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime StartDateTime {get;set;}
		public DateTime EndDateTime {get;set;}
		public TimeSpan Duration {get;set;}
		public DateTime ActualStartDateTime {get;set;}
		public DateTime ActualEndDateTime {get;set;}
		public Guid CustomerGUID {get;set;}
		public Guid StoreGUID {get;set;}
		public Guid ServiceProviderGUID {get;set;}
	}
	public partial class spCreateUpsertResult
	{
	}
	public partial class spAppointmentGetResult
	{
		public Guid GUID {get;set;}
		public string ServiceProviderFirstname {get;set;}
		public string ServiceProviderSurname {get;set;}
		public string CustomerFirstname {get;set;}
		public string CustomerSurname {get;set;}
		public DateTime StartDateTime {get;set;}
		public DateTime EndDateTime {get;set;}
		public TimeSpan Duration {get;set;}
		public DateTime ActualStartDateTime {get;set;}
		public DateTime ActualEndDateTime {get;set;}
		public TimeSpan ActualDuration {get;set;}
		public int DelayTime {get;set;}
		public int ExpectedDelay {get;set;}
		public DateTime ExpectedStartDateTime {get;set;}
		public string Colour {get;set;}
	}
	public partial class spServiceProviderUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string Firstname {get;set;}
		public string Surname {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spServiceProviderGetResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string Firstname {get;set;}
		public string Surname {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spCustomerUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string Firstname {get;set;}
		public string Surname {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spCustomerGetResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string Firstname {get;set;}
		public string Surname {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spActivityScheduleUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public int DoW {get;set;}
		public TimeSpan StartTime {get;set;}
		public TimeSpan EndTime {get;set;}
		public Guid ActivityTypeGUID {get;set;}
		public Guid ServiceProviderGUID {get;set;}
	}
	public partial class spActivityScheduleGetResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public int DoW {get;set;}
		public TimeSpan StartTime {get;set;}
		public TimeSpan EndTime {get;set;}
		public Guid ActivityTypeGUID {get;set;}
		public Guid ServiceProviderGUID {get;set;}
	}
	public partial class spStoreUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string StoreName {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spStoreGetResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string StoreName {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public partial class spSystemUserUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string Username {get;set;}
	}
	public partial class spSystemUserGetResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public DateTime ActiveDateTime {get;set;}
		public DateTime TerminationDateTime {get;set;}
		public bool IsActiveForNow {get;set;}
		public string Username {get;set;}
	}
	public partial class spAuditLogUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public string Source {get;set;}
		public Guid TableGUID {get;set;}
		public string TableName {get;set;}
		public string BeforeSnapshot {get;set;}
		public string AfterSnapshot {get;set;}
		public Guid SystemUserGUID {get;set;}
	}
}
