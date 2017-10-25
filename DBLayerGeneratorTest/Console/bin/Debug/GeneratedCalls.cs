namespace AppointmentLibrary.Calls
{
	using System;
	using System.Data.SqlClient;
	using AppointmentLibrary.ProcResults;
	using AppointmentLibrary.Parameters;
	using System.Collections.Generic;
	public static class Calls
	{
			public static List<spAppointmentUpsertResult> spAppointmentUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? StartDateTime, TimeSpan? Duration, DateTime? ActualStartDateTime, DateTime? ActualEndDateTime, Guid? CustomerGUID, Guid? StoreGUID, Guid? ServiceProviderGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spAppointmentUpsertParameters parameters = new spAppointmentUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.StartDateTime = StartDateTime;
				parameters.Duration = Duration;
				parameters.ActualStartDateTime = ActualStartDateTime;
				parameters.ActualEndDateTime = ActualEndDateTime;
				parameters.CustomerGUID = CustomerGUID;
				parameters.StoreGUID = StoreGUID;
				parameters.ServiceProviderGUID = ServiceProviderGUID;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spAppointmentUpsertCall (parameters, connectionString);
			}
			public static List<spAppointmentUpsertResult> spAppointmentUpsertCall (spAppointmentUpsertParameters parameters, string connectionString)
			{
				List<spAppointmentUpsertResult> ret = new List<spAppointmentUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spAppointmentUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @StartDateTime = @StartDateTime, @Duration = @Duration, @ActualStartDateTime = @ActualStartDateTime, @ActualEndDateTime = @ActualEndDateTime, @CustomerGUID = @CustomerGUID, @StoreGUID = @StoreGUID, @ServiceProviderGUID = @ServiceProviderGUID, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@StartDateTime", parameters.StartDateTime == null ? (object)DBNull.Value :  parameters.StartDateTime));
						cmd.Parameters.Add(new SqlParameter("@Duration", parameters.Duration == null ? (object)DBNull.Value :  parameters.Duration));
						cmd.Parameters.Add(new SqlParameter("@ActualStartDateTime", parameters.ActualStartDateTime == null ? (object)DBNull.Value :  parameters.ActualStartDateTime));
						cmd.Parameters.Add(new SqlParameter("@ActualEndDateTime", parameters.ActualEndDateTime == null ? (object)DBNull.Value :  parameters.ActualEndDateTime));
						cmd.Parameters.Add(new SqlParameter("@CustomerGUID", parameters.CustomerGUID == null ? (object)DBNull.Value :  parameters.CustomerGUID));
						cmd.Parameters.Add(new SqlParameter("@StoreGUID", parameters.StoreGUID == null ? (object)DBNull.Value :  parameters.StoreGUID));
						cmd.Parameters.Add(new SqlParameter("@ServiceProviderGUID", parameters.ServiceProviderGUID == null ? (object)DBNull.Value :  parameters.ServiceProviderGUID));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spAppointmentUpsertResult res = new spAppointmentUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["StartDateTime"].ToString()))
								{
								    res.StartDateTime = DateTime.Parse(reader["StartDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["EndDateTime"].ToString()))
								{
								    res.EndDateTime = DateTime.Parse(reader["EndDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["Duration"].ToString()))
								{
								    res.Duration = TimeSpan.Parse(reader["Duration"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["ActualStartDateTime"].ToString()))
								{
								    res.ActualStartDateTime = DateTime.Parse(reader["ActualStartDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["ActualEndDateTime"].ToString()))
								{
								    res.ActualEndDateTime = DateTime.Parse(reader["ActualEndDateTime"].ToString());
								}
								res.CustomerGUID = new Guid(reader["CustomerGUID"].ToString());
								res.StoreGUID = new Guid(reader["StoreGUID"].ToString());
								res.ServiceProviderGUID = new Guid(reader["ServiceProviderGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spAppointmentGetResult> spAppointmentGetCall(Guid? AppointmentGUID, string connectionString)
			{
				spAppointmentGetParameters parameters = new spAppointmentGetParameters();
				parameters.AppointmentGUID = AppointmentGUID;

				return spAppointmentGetCall (parameters, connectionString);
			}
			public static List<spAppointmentGetResult> spAppointmentGetCall (spAppointmentGetParameters parameters, string connectionString)
			{
				List<spAppointmentGetResult> ret = new List<spAppointmentGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spAppointmentGet @AppointmentGUID = @AppointmentGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@AppointmentGUID", parameters.AppointmentGUID == null ? (object)DBNull.Value :  parameters.AppointmentGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spAppointmentGetResult res = new spAppointmentGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								res.ServiceProviderFirstname = reader["ServiceProviderFirstname"].ToString();
								res.ServiceProviderSurname = reader["ServiceProviderSurname"].ToString();
								res.CustomerFirstname = reader["CustomerFirstname"].ToString();
								res.CustomerSurname = reader["CustomerSurname"].ToString();
								if (!String.IsNullOrWhiteSpace(reader["StartDateTime"].ToString()))
								{
								    res.StartDateTime = DateTime.Parse(reader["StartDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["EndDateTime"].ToString()))
								{
								    res.EndDateTime = DateTime.Parse(reader["EndDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["Duration"].ToString()))
								{
								    res.Duration = TimeSpan.Parse(reader["Duration"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["ActualStartDateTime"].ToString()))
								{
								    res.ActualStartDateTime = DateTime.Parse(reader["ActualStartDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["ActualEndDateTime"].ToString()))
								{
								    res.ActualEndDateTime = DateTime.Parse(reader["ActualEndDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["ActualDuration"].ToString()))
								{
								    res.ActualDuration = TimeSpan.Parse(reader["ActualDuration"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DelayTime"].ToString()))
								{
								    res.DelayTime = int.Parse(reader["DelayTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["ExpectedDelay"].ToString()))
								{
								    res.ExpectedDelay = int.Parse(reader["ExpectedDelay"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["ExpectedStartDateTime"].ToString()))
								{
								    res.ExpectedStartDateTime = DateTime.Parse(reader["ExpectedStartDateTime"].ToString());
								}
								res.Colour = reader["Colour"].ToString();
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spServiceProviderUpsertResult> spServiceProviderUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string Firstname, string Surname, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spServiceProviderUpsertParameters parameters = new spServiceProviderUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.ActiveDateTime = ActiveDateTime;
				parameters.TerminationDateTime = TerminationDateTime;
				parameters.Firstname = Firstname;
				parameters.Surname = Surname;
				parameters.AccountGUID = AccountGUID;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spServiceProviderUpsertCall (parameters, connectionString);
			}
			public static List<spServiceProviderUpsertResult> spServiceProviderUpsertCall (spServiceProviderUpsertParameters parameters, string connectionString)
			{
				List<spServiceProviderUpsertResult> ret = new List<spServiceProviderUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spServiceProviderUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @ActiveDateTime = @ActiveDateTime, @TerminationDateTime = @TerminationDateTime, @Firstname = @Firstname, @Surname = @Surname, @AccountGUID = @AccountGUID, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@ActiveDateTime", parameters.ActiveDateTime == null ? (object)DBNull.Value :  parameters.ActiveDateTime));
						cmd.Parameters.Add(new SqlParameter("@TerminationDateTime", parameters.TerminationDateTime == null ? (object)DBNull.Value :  parameters.TerminationDateTime));
						cmd.Parameters.Add(new SqlParameter("@Firstname", parameters.Firstname == null ? (object)DBNull.Value :  parameters.Firstname));
						cmd.Parameters.Add(new SqlParameter("@Surname", parameters.Surname == null ? (object)DBNull.Value :  parameters.Surname));
						cmd.Parameters.Add(new SqlParameter("@AccountGUID", parameters.AccountGUID == null ? (object)DBNull.Value :  parameters.AccountGUID));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spServiceProviderUpsertResult res = new spServiceProviderUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.Firstname = reader["Firstname"].ToString();
								res.Surname = reader["Surname"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spServiceProviderGetResult> spServiceProviderGetCall(Guid? ServiceProviderGUID, string connectionString)
			{
				spServiceProviderGetParameters parameters = new spServiceProviderGetParameters();
				parameters.ServiceProviderGUID = ServiceProviderGUID;

				return spServiceProviderGetCall (parameters, connectionString);
			}
			public static List<spServiceProviderGetResult> spServiceProviderGetCall (spServiceProviderGetParameters parameters, string connectionString)
			{
				List<spServiceProviderGetResult> ret = new List<spServiceProviderGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spServiceProviderGet @ServiceProviderGUID = @ServiceProviderGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@ServiceProviderGUID", parameters.ServiceProviderGUID == null ? (object)DBNull.Value :  parameters.ServiceProviderGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spServiceProviderGetResult res = new spServiceProviderGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.Firstname = reader["Firstname"].ToString();
								res.Surname = reader["Surname"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spCustomerUpsertResult> spCustomerUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string Firstname, string Surname, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spCustomerUpsertParameters parameters = new spCustomerUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.ActiveDateTime = ActiveDateTime;
				parameters.TerminationDateTime = TerminationDateTime;
				parameters.Firstname = Firstname;
				parameters.Surname = Surname;
				parameters.AccountGUID = AccountGUID;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spCustomerUpsertCall (parameters, connectionString);
			}
			public static List<spCustomerUpsertResult> spCustomerUpsertCall (spCustomerUpsertParameters parameters, string connectionString)
			{
				List<spCustomerUpsertResult> ret = new List<spCustomerUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spCustomerUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @ActiveDateTime = @ActiveDateTime, @TerminationDateTime = @TerminationDateTime, @Firstname = @Firstname, @Surname = @Surname, @AccountGUID = @AccountGUID, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@ActiveDateTime", parameters.ActiveDateTime == null ? (object)DBNull.Value :  parameters.ActiveDateTime));
						cmd.Parameters.Add(new SqlParameter("@TerminationDateTime", parameters.TerminationDateTime == null ? (object)DBNull.Value :  parameters.TerminationDateTime));
						cmd.Parameters.Add(new SqlParameter("@Firstname", parameters.Firstname == null ? (object)DBNull.Value :  parameters.Firstname));
						cmd.Parameters.Add(new SqlParameter("@Surname", parameters.Surname == null ? (object)DBNull.Value :  parameters.Surname));
						cmd.Parameters.Add(new SqlParameter("@AccountGUID", parameters.AccountGUID == null ? (object)DBNull.Value :  parameters.AccountGUID));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spCustomerUpsertResult res = new spCustomerUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.Firstname = reader["Firstname"].ToString();
								res.Surname = reader["Surname"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spCustomerGetResult> spCustomerGetCall(Guid? CustomerGUID, string connectionString)
			{
				spCustomerGetParameters parameters = new spCustomerGetParameters();
				parameters.CustomerGUID = CustomerGUID;

				return spCustomerGetCall (parameters, connectionString);
			}
			public static List<spCustomerGetResult> spCustomerGetCall (spCustomerGetParameters parameters, string connectionString)
			{
				List<spCustomerGetResult> ret = new List<spCustomerGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spCustomerGet @CustomerGUID = @CustomerGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@CustomerGUID", parameters.CustomerGUID == null ? (object)DBNull.Value :  parameters.CustomerGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spCustomerGetResult res = new spCustomerGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.Firstname = reader["Firstname"].ToString();
								res.Surname = reader["Surname"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spActivityScheduleUpsertResult> spActivityScheduleUpsertCall(Guid? GUID, bool? IsDeleted, int? DoW, TimeSpan? StartTime, TimeSpan? EndTime, Guid? ActivityTypeGUID, Guid? ServiceProviderGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spActivityScheduleUpsertParameters parameters = new spActivityScheduleUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.DoW = DoW;
				parameters.StartTime = StartTime;
				parameters.EndTime = EndTime;
				parameters.ActivityTypeGUID = ActivityTypeGUID;
				parameters.ServiceProviderGUID = ServiceProviderGUID;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spActivityScheduleUpsertCall (parameters, connectionString);
			}
			public static List<spActivityScheduleUpsertResult> spActivityScheduleUpsertCall (spActivityScheduleUpsertParameters parameters, string connectionString)
			{
				List<spActivityScheduleUpsertResult> ret = new List<spActivityScheduleUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spActivityScheduleUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @DoW = @DoW, @StartTime = @StartTime, @EndTime = @EndTime, @ActivityTypeGUID = @ActivityTypeGUID, @ServiceProviderGUID = @ServiceProviderGUID, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@DoW", parameters.DoW == null ? (object)DBNull.Value :  parameters.DoW));
						cmd.Parameters.Add(new SqlParameter("@StartTime", parameters.StartTime == null ? (object)DBNull.Value :  parameters.StartTime));
						cmd.Parameters.Add(new SqlParameter("@EndTime", parameters.EndTime == null ? (object)DBNull.Value :  parameters.EndTime));
						cmd.Parameters.Add(new SqlParameter("@ActivityTypeGUID", parameters.ActivityTypeGUID == null ? (object)DBNull.Value :  parameters.ActivityTypeGUID));
						cmd.Parameters.Add(new SqlParameter("@ServiceProviderGUID", parameters.ServiceProviderGUID == null ? (object)DBNull.Value :  parameters.ServiceProviderGUID));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spActivityScheduleUpsertResult res = new spActivityScheduleUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["DoW"].ToString()))
								{
								    res.DoW = int.Parse(reader["DoW"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["StartTime"].ToString()))
								{
								    res.StartTime = TimeSpan.Parse(reader["StartTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["EndTime"].ToString()))
								{
								    res.EndTime = TimeSpan.Parse(reader["EndTime"].ToString());
								}
								res.ActivityTypeGUID = new Guid(reader["ActivityTypeGUID"].ToString());
								res.ServiceProviderGUID = new Guid(reader["ServiceProviderGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spActivityScheduleGetResult> spActivityScheduleGetCall(Guid? ActivityScheduleGUID, string connectionString)
			{
				spActivityScheduleGetParameters parameters = new spActivityScheduleGetParameters();
				parameters.ActivityScheduleGUID = ActivityScheduleGUID;

				return spActivityScheduleGetCall (parameters, connectionString);
			}
			public static List<spActivityScheduleGetResult> spActivityScheduleGetCall (spActivityScheduleGetParameters parameters, string connectionString)
			{
				List<spActivityScheduleGetResult> ret = new List<spActivityScheduleGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spActivityScheduleGet @ActivityScheduleGUID = @ActivityScheduleGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@ActivityScheduleGUID", parameters.ActivityScheduleGUID == null ? (object)DBNull.Value :  parameters.ActivityScheduleGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spActivityScheduleGetResult res = new spActivityScheduleGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["DoW"].ToString()))
								{
								    res.DoW = int.Parse(reader["DoW"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["StartTime"].ToString()))
								{
								    res.StartTime = TimeSpan.Parse(reader["StartTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["EndTime"].ToString()))
								{
								    res.EndTime = TimeSpan.Parse(reader["EndTime"].ToString());
								}
								res.ActivityTypeGUID = new Guid(reader["ActivityTypeGUID"].ToString());
								res.ServiceProviderGUID = new Guid(reader["ServiceProviderGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spCreateUpsertResult> spCreateUpsertCall(string TableName, string connectionString)
			{
				spCreateUpsertParameters parameters = new spCreateUpsertParameters();
				parameters.TableName = TableName;

				return spCreateUpsertCall (parameters, connectionString);
			}
			public static List<spCreateUpsertResult> spCreateUpsertCall (spCreateUpsertParameters parameters, string connectionString)
			{
				List<spCreateUpsertResult> ret = new List<spCreateUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spCreateUpsert @TableName = @TableName";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@TableName", parameters.TableName == null ? (object)DBNull.Value :  parameters.TableName));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spCreateUpsertResult res = new spCreateUpsertResult();
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spStoreUpsertResult> spStoreUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string StoreName, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spStoreUpsertParameters parameters = new spStoreUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.ActiveDateTime = ActiveDateTime;
				parameters.TerminationDateTime = TerminationDateTime;
				parameters.StoreName = StoreName;
				parameters.AccountGUID = AccountGUID;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spStoreUpsertCall (parameters, connectionString);
			}
			public static List<spStoreUpsertResult> spStoreUpsertCall (spStoreUpsertParameters parameters, string connectionString)
			{
				List<spStoreUpsertResult> ret = new List<spStoreUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spStoreUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @ActiveDateTime = @ActiveDateTime, @TerminationDateTime = @TerminationDateTime, @StoreName = @StoreName, @AccountGUID = @AccountGUID, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@ActiveDateTime", parameters.ActiveDateTime == null ? (object)DBNull.Value :  parameters.ActiveDateTime));
						cmd.Parameters.Add(new SqlParameter("@TerminationDateTime", parameters.TerminationDateTime == null ? (object)DBNull.Value :  parameters.TerminationDateTime));
						cmd.Parameters.Add(new SqlParameter("@StoreName", parameters.StoreName == null ? (object)DBNull.Value :  parameters.StoreName));
						cmd.Parameters.Add(new SqlParameter("@AccountGUID", parameters.AccountGUID == null ? (object)DBNull.Value :  parameters.AccountGUID));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spStoreUpsertResult res = new spStoreUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.StoreName = reader["StoreName"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spStoreGetResult> spStoreGetCall(Guid? StoreGUID, string connectionString)
			{
				spStoreGetParameters parameters = new spStoreGetParameters();
				parameters.StoreGUID = StoreGUID;

				return spStoreGetCall (parameters, connectionString);
			}
			public static List<spStoreGetResult> spStoreGetCall (spStoreGetParameters parameters, string connectionString)
			{
				List<spStoreGetResult> ret = new List<spStoreGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spStoreGet @StoreGUID = @StoreGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@StoreGUID", parameters.StoreGUID == null ? (object)DBNull.Value :  parameters.StoreGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spStoreGetResult res = new spStoreGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.StoreName = reader["StoreName"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spSystemUserUpsertResult> spSystemUserUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string Username, string Password, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spSystemUserUpsertParameters parameters = new spSystemUserUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.ActiveDateTime = ActiveDateTime;
				parameters.TerminationDateTime = TerminationDateTime;
				parameters.Username = Username;
				parameters.Password = Password;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spSystemUserUpsertCall (parameters, connectionString);
			}
			public static List<spSystemUserUpsertResult> spSystemUserUpsertCall (spSystemUserUpsertParameters parameters, string connectionString)
			{
				List<spSystemUserUpsertResult> ret = new List<spSystemUserUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spSystemUserUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @ActiveDateTime = @ActiveDateTime, @TerminationDateTime = @TerminationDateTime, @Username = @Username, @Password = @Password, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@ActiveDateTime", parameters.ActiveDateTime == null ? (object)DBNull.Value :  parameters.ActiveDateTime));
						cmd.Parameters.Add(new SqlParameter("@TerminationDateTime", parameters.TerminationDateTime == null ? (object)DBNull.Value :  parameters.TerminationDateTime));
						cmd.Parameters.Add(new SqlParameter("@Username", parameters.Username == null ? (object)DBNull.Value :  parameters.Username));
						cmd.Parameters.Add(new SqlParameter("@Password", parameters.Password == null ? (object)DBNull.Value :  parameters.Password));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spSystemUserUpsertResult res = new spSystemUserUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.Username = reader["Username"].ToString();
								res.PasswordHash = (byte[])(reader["PasswordHash"]);
								res.PasswordSalt = new Guid(reader["PasswordSalt"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spSystemUserGetResult> spSystemUserGetCall(Guid? SystemUserGUID, string connectionString)
			{
				spSystemUserGetParameters parameters = new spSystemUserGetParameters();
				parameters.SystemUserGUID = SystemUserGUID;

				return spSystemUserGetCall (parameters, connectionString);
			}
			public static List<spSystemUserGetResult> spSystemUserGetCall (spSystemUserGetParameters parameters, string connectionString)
			{
				List<spSystemUserGetResult> ret = new List<spSystemUserGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spSystemUserGet @SystemUserGUID = @SystemUserGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spSystemUserGetResult res = new spSystemUserGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.Username = reader["Username"].ToString();
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spLoginResult> spLoginCall(string UserName, string Password, string connectionString)
			{
				spLoginParameters parameters = new spLoginParameters();
				parameters.UserName = UserName;
				parameters.Password = Password;

				return spLoginCall (parameters, connectionString);
			}
			public static List<spLoginResult> spLoginCall (spLoginParameters parameters, string connectionString)
			{
				List<spLoginResult> ret = new List<spLoginResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spLogin @UserName = @UserName, @Password = @Password";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@UserName", parameters.UserName == null ? (object)DBNull.Value :  parameters.UserName));
						cmd.Parameters.Add(new SqlParameter("@Password", parameters.Password == null ? (object)DBNull.Value :  parameters.Password));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spLoginResult res = new spLoginResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.Username = reader["Username"].ToString();
								res.PasswordHash = (byte[])(reader["PasswordHash"]);
								res.PasswordSalt = new Guid(reader["PasswordSalt"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spAuditLogUpsertResult> spAuditLogUpsertCall(Guid? GUID, string Source, string TableName, string BeforeSnapshot, string AfterSnapshot, Guid? TableGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spAuditLogUpsertParameters parameters = new spAuditLogUpsertParameters();
				parameters.GUID = GUID;
				parameters.Source = Source;
				parameters.TableName = TableName;
				parameters.BeforeSnapshot = BeforeSnapshot;
				parameters.AfterSnapshot = AfterSnapshot;
				parameters.TableGUID = TableGUID;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spAuditLogUpsertCall (parameters, connectionString);
			}
			public static List<spAuditLogUpsertResult> spAuditLogUpsertCall (spAuditLogUpsertParameters parameters, string connectionString)
			{
				List<spAuditLogUpsertResult> ret = new List<spAuditLogUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spAuditLogUpsert @GUID = @GUID, @Source = @Source, @TableName = @TableName, @BeforeSnapshot = @BeforeSnapshot, @AfterSnapshot = @AfterSnapshot, @TableGUID = @TableGUID, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@Source", parameters.Source == null ? (object)DBNull.Value :  parameters.Source));
						cmd.Parameters.Add(new SqlParameter("@TableName", parameters.TableName == null ? (object)DBNull.Value :  parameters.TableName));
						cmd.Parameters.Add(new SqlParameter("@BeforeSnapshot", parameters.BeforeSnapshot == null ? (object)DBNull.Value :  parameters.BeforeSnapshot));
						cmd.Parameters.Add(new SqlParameter("@AfterSnapshot", parameters.AfterSnapshot == null ? (object)DBNull.Value :  parameters.AfterSnapshot));
						cmd.Parameters.Add(new SqlParameter("@TableGUID", parameters.TableGUID == null ? (object)DBNull.Value :  parameters.TableGUID));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spAuditLogUpsertResult res = new spAuditLogUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.Source = reader["Source"].ToString();
								res.TableGUID = new Guid(reader["TableGUID"].ToString());
								res.TableName = reader["TableName"].ToString();
								res.BeforeSnapshot = reader["BeforeSnapshot"].ToString();
								res.AfterSnapshot = reader["AfterSnapshot"].ToString();
								res.SystemUserGUID = new Guid(reader["SystemUserGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spAccountUpsertResult> spAccountUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string AccountName, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spAccountUpsertParameters parameters = new spAccountUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.ActiveDateTime = ActiveDateTime;
				parameters.TerminationDateTime = TerminationDateTime;
				parameters.AccountName = AccountName;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spAccountUpsertCall (parameters, connectionString);
			}
			public static List<spAccountUpsertResult> spAccountUpsertCall (spAccountUpsertParameters parameters, string connectionString)
			{
				List<spAccountUpsertResult> ret = new List<spAccountUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spAccountUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @ActiveDateTime = @ActiveDateTime, @TerminationDateTime = @TerminationDateTime, @AccountName = @AccountName, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@ActiveDateTime", parameters.ActiveDateTime == null ? (object)DBNull.Value :  parameters.ActiveDateTime));
						cmd.Parameters.Add(new SqlParameter("@TerminationDateTime", parameters.TerminationDateTime == null ? (object)DBNull.Value :  parameters.TerminationDateTime));
						cmd.Parameters.Add(new SqlParameter("@AccountName", parameters.AccountName == null ? (object)DBNull.Value :  parameters.AccountName));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spAccountUpsertResult res = new spAccountUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.AccountName = reader["AccountName"].ToString();
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spAccountGetResult> spAccountGetCall(Guid? AccountGUID, string connectionString)
			{
				spAccountGetParameters parameters = new spAccountGetParameters();
				parameters.AccountGUID = AccountGUID;

				return spAccountGetCall (parameters, connectionString);
			}
			public static List<spAccountGetResult> spAccountGetCall (spAccountGetParameters parameters, string connectionString)
			{
				List<spAccountGetResult> ret = new List<spAccountGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spAccountGet @AccountGUID = @AccountGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@AccountGUID", parameters.AccountGUID == null ? (object)DBNull.Value :  parameters.AccountGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spAccountGetResult res = new spAccountGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								if (!String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    res.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    res.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								res.IsActiveForNow = (bool)reader["IsActiveForNow"];
								res.AccountName = reader["AccountName"].ToString();
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spActivityTypeUpsertResult> spActivityTypeUpsertCall(Guid? GUID, bool? IsDeleted, string ActivityType, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spActivityTypeUpsertParameters parameters = new spActivityTypeUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.ActivityType = ActivityType;
				parameters.AccountGUID = AccountGUID;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spActivityTypeUpsertCall (parameters, connectionString);
			}
			public static List<spActivityTypeUpsertResult> spActivityTypeUpsertCall (spActivityTypeUpsertParameters parameters, string connectionString)
			{
				List<spActivityTypeUpsertResult> ret = new List<spActivityTypeUpsertResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spActivityTypeUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @ActivityType = @ActivityType, @AccountGUID = @AccountGUID, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@ActivityType", parameters.ActivityType == null ? (object)DBNull.Value :  parameters.ActivityType));
						cmd.Parameters.Add(new SqlParameter("@AccountGUID", parameters.AccountGUID == null ? (object)DBNull.Value :  parameters.AccountGUID));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spActivityTypeUpsertResult res = new spActivityTypeUpsertResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								res.ActivityType = reader["ActivityType"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
			public static List<spActivityTypeGetResult> spActivityTypeGetCall(Guid? ActivityTypeGUID, string connectionString)
			{
				spActivityTypeGetParameters parameters = new spActivityTypeGetParameters();
				parameters.ActivityTypeGUID = ActivityTypeGUID;

				return spActivityTypeGetCall (parameters, connectionString);
			}
			public static List<spActivityTypeGetResult> spActivityTypeGetCall (spActivityTypeGetParameters parameters, string connectionString)
			{
				List<spActivityTypeGetResult> ret = new List<spActivityTypeGetResult>();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spActivityTypeGet @ActivityTypeGUID = @ActivityTypeGUID";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@ActivityTypeGUID", parameters.ActivityTypeGUID == null ? (object)DBNull.Value :  parameters.ActivityTypeGUID));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
					            spActivityTypeGetResult res = new spActivityTypeGetResult();
								res.GUID = new Guid(reader["GUID"].ToString());
								if (!String.IsNullOrWhiteSpace(reader["ID"].ToString()))
								{
								    res.ID = int.Parse(reader["ID"].ToString());
								}
								if (!String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    res.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								res.IsDeleted = (bool)reader["IsDeleted"];
								res.ActivityType = reader["ActivityType"].ToString();
								res.AccountGUID = new Guid(reader["AccountGUID"].ToString());
								ret.Add(res);
				            }
				        }
				    }
				}
				return ret;
			}
	}
}
