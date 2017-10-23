namespace AppointmentLibrary
{
	public class vwAppointment
	{
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
