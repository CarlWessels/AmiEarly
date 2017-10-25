namespace AppointmentLibrary.Parameters
{
	using System;
	public interface IParameter
	{
	}
	public partial class spAppointmentUpsertParameters : IParameter
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
	public partial class spAppointmentGetParameters : IParameter
	{
		public Guid? AppointmentGUID { get; set;}
	}
	public partial class spServiceProviderUpsertParameters : IParameter
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
	public partial class spServiceProviderGetParameters : IParameter
	{
		public Guid? ServiceProviderGUID { get; set;}
	}
	public partial class spCustomerUpsertParameters : IParameter
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
	public partial class spCustomerGetParameters : IParameter
	{
		public Guid? CustomerGUID { get; set;}
	}
	public partial class spActivityScheduleUpsertParameters : IParameter
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
	public partial class spActivityScheduleGetParameters : IParameter
	{
		public Guid? ActivityScheduleGUID { get; set;}
	}
	public partial class spCreateUpsertParameters : IParameter
	{
		public string TableName { get; set;}
	}
	public partial class spStoreUpsertParameters : IParameter
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
	public partial class spStoreGetParameters : IParameter
	{
		public Guid? StoreGUID { get; set;}
	}
	public partial class spSystemUserUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? ActiveDateTime { get; set;}
		public DateTime? TerminationDateTime { get; set;}
		public string Username { get; set;}
		public string Password { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public partial class spSystemUserGetParameters : IParameter
	{
		public Guid? SystemUserGUID { get; set;}
	}
	public partial class spLoginParameters : IParameter
	{
		public string UserName { get; set;}
		public string Password { get; set;}
	}
	public partial class spAuditLogUpsertParameters : IParameter
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
	public partial class spAccountUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? ActiveDateTime { get; set;}
		public DateTime? TerminationDateTime { get; set;}
		public string AccountName { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public partial class spAccountGetParameters : IParameter
	{
		public Guid? AccountGUID { get; set;}
	}
	public partial class spActivityTypeUpsertParameters : IParameter
	{
		public Guid? GUID { get; set;}
		public bool? IsDeleted { get; set;}
		public string ActivityType { get; set;}
		public Guid? AccountGUID { get; set;}
		public Guid? SystemUserGUID { get; set;}
		public bool? ReturnResults { get; set;}
	}
	public partial class spActivityTypeGetParameters : IParameter
	{
		public Guid? ActivityTypeGUID { get; set;}
	}
}
