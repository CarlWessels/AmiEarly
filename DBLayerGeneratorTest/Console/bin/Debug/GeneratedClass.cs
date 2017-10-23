namespace AppointmentLibrary
{
	using System;
	public class spAccountGetResult
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
	public class spCreateUpsertResult
	{
	}
	public class spAccountUpsertResult
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
	public class spActivityTypeUpsertResult
	{
		public Guid GUID {get;set;}
		public int ID {get;set;}
		public DateTime DateTimeCreated {get;set;}
		public bool IsDeleted {get;set;}
		public string ActivityType {get;set;}
		public Guid AccountGUID {get;set;}
	}
	public class spAppointmentUpsertResult
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
	public class spServiceProviderUpsertResult
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
	public class spCustomerUpsertResult
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
	public class spActivityScheduleUpsertResult
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
	public class spStoreUpsertResult
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
	public class spSystemUserUpsertResult
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
	public class spAuditLogUpsertResult
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
