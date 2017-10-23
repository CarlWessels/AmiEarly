namespace AppointmentLibrary
{
	using System;
	public interface IParameter
	{
	}
	public class spAccountGetParameters : IParameter
	{
		public Guid? AccountGUID { get; set;}
	}
	public class spCreateUpsertParameters : IParameter
	{
		public string TableName { get; set;}
	}
	public class spAccountUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? ActiveDateTime { get; set;}
		public DateTime? TerminationDateTime { get; set;}
		public string AccountName { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spActivityTypeUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public string ActivityType { get; set;}
		public Guid? AccountGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spAppointmentUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? StartDateTime { get; set;}
		public TimeSpan? Duration { get; set;}
		public DateTime? ActualStartDateTime { get; set;}
		public DateTime? ActualEndDateTime { get; set;}
		public Guid? CustomerGUID { get; set;}
		public Guid? StoreGUID { get; set;}
		public Guid? ServiceProviderGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spServiceProviderUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? ActiveDateTime { get; set;}
		public DateTime? TerminationDateTime { get; set;}
		public string Firstname { get; set;}
		public string Surname { get; set;}
		public Guid? AccountGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spCustomerUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? ActiveDateTime { get; set;}
		public DateTime? TerminationDateTime { get; set;}
		public string Firstname { get; set;}
		public string Surname { get; set;}
		public Guid? AccountGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spActivityScheduleUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public int? DoW { get; set;}
		public TimeSpan? StartTime { get; set;}
		public TimeSpan? EndTime { get; set;}
		public Guid? ActivityTypeGUID { get; set;}
		public Guid? ServiceProviderGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spStoreUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? ActiveDateTime { get; set;}
		public DateTime? TerminationDateTime { get; set;}
		public string StoreName { get; set;}
		public Guid? AccountGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spSystemUserUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? ActiveDateTime { get; set;}
		public DateTime? TerminationDateTime { get; set;}
		public string Username { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public class spAuditLogUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public string Source { get; set;}
		public string TableName { get; set;}
		public string BeforeSnapshot { get; set;}
		public string AfterSnapshot { get; set;}
		public Guid? TableGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
}
