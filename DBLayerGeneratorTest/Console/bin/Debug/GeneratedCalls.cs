namespace AppointmentLibrary
{
	using System.Data.SqlClient;
using System;
	public static class Calls
	{
			public static spAccountGetResult spAccountGetCall(Guid? AccountGUID, string connectionString)
			{
				spAccountGetParameters parameters = new spAccountGetParameters();
				parameters.AccountGUID = AccountGUID;

				return spAccountGetCall (parameters, connectionString);
			}
			public static spAccountGetResult spAccountGetCall (spAccountGetParameters parameters, string connectionString)
			{
				spAccountGetResult ret = new spAccountGetResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								if (String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    //ret.ActiveDateTime = null;
								}
								else
								{
								    ret.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    //ret.TerminationDateTime = null;
								}
								else
								{
								    ret.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								ret.IsActiveForNow = (bool)reader["IsActiveForNow"];
								ret.AccountName = reader["AccountName"].ToString();
				            }
				        }
				    }
				}
				return ret;
			}
			public static spCreateUpsertResult spCreateUpsertCall(string TableName, string connectionString)
			{
				spCreateUpsertParameters parameters = new spCreateUpsertParameters();
				parameters.TableName = TableName;

				return spCreateUpsertCall (parameters, connectionString);
			}
			public static spCreateUpsertResult spCreateUpsertCall (spCreateUpsertParameters parameters, string connectionString)
			{
				spCreateUpsertResult ret = new spCreateUpsertResult();
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
				            }
				        }
				    }
				}
				return ret;
			}
			public static spAccountUpsertResult spAccountUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string AccountName, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spAccountUpsertResult spAccountUpsertCall (spAccountUpsertParameters parameters, string connectionString)
			{
				spAccountUpsertResult ret = new spAccountUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								if (String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    //ret.ActiveDateTime = null;
								}
								else
								{
								    ret.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    //ret.TerminationDateTime = null;
								}
								else
								{
								    ret.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								ret.IsActiveForNow = (bool)reader["IsActiveForNow"];
								ret.AccountName = reader["AccountName"].ToString();
				            }
				        }
				    }
				}
				return ret;
			}
			public static spActivityTypeUpsertResult spActivityTypeUpsertCall(Guid? GUID, bool? IsDeleted, string ActivityType, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spActivityTypeUpsertResult spActivityTypeUpsertCall (spActivityTypeUpsertParameters parameters, string connectionString)
			{
				spActivityTypeUpsertResult ret = new spActivityTypeUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								ret.ActivityType = reader["ActivityType"].ToString();
								ret.AccountGUID = new Guid(reader["AccountGUID"].ToString());
				            }
				        }
				    }
				}
				return ret;
			}
			public static spAppointmentUpsertResult spAppointmentUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? StartDateTime, TimeSpan? Duration, DateTime? ActualStartDateTime, DateTime? ActualEndDateTime, Guid? CustomerGUID, Guid? StoreGUID, Guid? ServiceProviderGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spAppointmentUpsertResult spAppointmentUpsertCall (spAppointmentUpsertParameters parameters, string connectionString)
			{
				spAppointmentUpsertResult ret = new spAppointmentUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								if (String.IsNullOrWhiteSpace(reader["StartDateTime"].ToString()))
								{
								    //ret.StartDateTime = null;
								}
								else
								{
								    ret.StartDateTime = DateTime.Parse(reader["StartDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["EndDateTime"].ToString()))
								{
								    //ret.EndDateTime = null;
								}
								else
								{
								    ret.EndDateTime = DateTime.Parse(reader["EndDateTime"].ToString());
								}
								ret.Duration = TimeSpan.Parse(reader["Duration"].ToString());
								if (String.IsNullOrWhiteSpace(reader["ActualStartDateTime"].ToString()))
								{
								    //ret.ActualStartDateTime = null;
								}
								else
								{
								    ret.ActualStartDateTime = DateTime.Parse(reader["ActualStartDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["ActualEndDateTime"].ToString()))
								{
								    //ret.ActualEndDateTime = null;
								}
								else
								{
								    ret.ActualEndDateTime = DateTime.Parse(reader["ActualEndDateTime"].ToString());
								}
								ret.CustomerGUID = new Guid(reader["CustomerGUID"].ToString());
								ret.StoreGUID = new Guid(reader["StoreGUID"].ToString());
								ret.ServiceProviderGUID = new Guid(reader["ServiceProviderGUID"].ToString());
				            }
				        }
				    }
				}
				return ret;
			}
			public static spServiceProviderUpsertResult spServiceProviderUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string Firstname, string Surname, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spServiceProviderUpsertResult spServiceProviderUpsertCall (spServiceProviderUpsertParameters parameters, string connectionString)
			{
				spServiceProviderUpsertResult ret = new spServiceProviderUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								if (String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    //ret.ActiveDateTime = null;
								}
								else
								{
								    ret.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    //ret.TerminationDateTime = null;
								}
								else
								{
								    ret.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								ret.IsActiveForNow = (bool)reader["IsActiveForNow"];
								ret.Firstname = reader["Firstname"].ToString();
								ret.Surname = reader["Surname"].ToString();
								ret.AccountGUID = new Guid(reader["AccountGUID"].ToString());
				            }
				        }
				    }
				}
				return ret;
			}
			public static spCustomerUpsertResult spCustomerUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string Firstname, string Surname, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spCustomerUpsertResult spCustomerUpsertCall (spCustomerUpsertParameters parameters, string connectionString)
			{
				spCustomerUpsertResult ret = new spCustomerUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								if (String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    //ret.ActiveDateTime = null;
								}
								else
								{
								    ret.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    //ret.TerminationDateTime = null;
								}
								else
								{
								    ret.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								ret.IsActiveForNow = (bool)reader["IsActiveForNow"];
								ret.Firstname = reader["Firstname"].ToString();
								ret.Surname = reader["Surname"].ToString();
								ret.AccountGUID = new Guid(reader["AccountGUID"].ToString());
				            }
				        }
				    }
				}
				return ret;
			}
			public static spActivityScheduleUpsertResult spActivityScheduleUpsertCall(Guid? GUID, bool? IsDeleted, int? DoW, TimeSpan? StartTime, TimeSpan? EndTime, Guid? ActivityTypeGUID, Guid? ServiceProviderGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spActivityScheduleUpsertResult spActivityScheduleUpsertCall (spActivityScheduleUpsertParameters parameters, string connectionString)
			{
				spActivityScheduleUpsertResult ret = new spActivityScheduleUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								ret.DoW = int.Parse(reader["DoW"].ToString());
								ret.StartTime = TimeSpan.Parse(reader["StartTime"].ToString());
								ret.EndTime = TimeSpan.Parse(reader["EndTime"].ToString());
								ret.ActivityTypeGUID = new Guid(reader["ActivityTypeGUID"].ToString());
								ret.ServiceProviderGUID = new Guid(reader["ServiceProviderGUID"].ToString());
				            }
				        }
				    }
				}
				return ret;
			}
			public static spStoreUpsertResult spStoreUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string StoreName, Guid? AccountGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spStoreUpsertResult spStoreUpsertCall (spStoreUpsertParameters parameters, string connectionString)
			{
				spStoreUpsertResult ret = new spStoreUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								if (String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    //ret.ActiveDateTime = null;
								}
								else
								{
								    ret.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    //ret.TerminationDateTime = null;
								}
								else
								{
								    ret.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								ret.IsActiveForNow = (bool)reader["IsActiveForNow"];
								ret.StoreName = reader["StoreName"].ToString();
								ret.AccountGUID = new Guid(reader["AccountGUID"].ToString());
				            }
				        }
				    }
				}
				return ret;
			}
			public static spSystemUserUpsertResult spSystemUserUpsertCall(Guid? GUID, bool? IsDeleted, DateTime? ActiveDateTime, DateTime? TerminationDateTime, string Username, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
			{
				spSystemUserUpsertParameters parameters = new spSystemUserUpsertParameters();
				parameters.GUID = GUID;
				parameters.IsDeleted = IsDeleted;
				parameters.ActiveDateTime = ActiveDateTime;
				parameters.TerminationDateTime = TerminationDateTime;
				parameters.Username = Username;
				parameters.SystemUserGUID = SystemUserGUID;
				parameters.ReturnResults = ReturnResults;

				return spSystemUserUpsertCall (parameters, connectionString);
			}
			public static spSystemUserUpsertResult spSystemUserUpsertCall (spSystemUserUpsertParameters parameters, string connectionString)
			{
				spSystemUserUpsertResult ret = new spSystemUserUpsertResult();
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
				conn.Open();
					string qry = "EXEC spSystemUserUpsert @GUID = @GUID, @IsDeleted = @IsDeleted, @ActiveDateTime = @ActiveDateTime, @TerminationDateTime = @TerminationDateTime, @Username = @Username, @SystemUserGUID = @SystemUserGUID, @ReturnResults = @ReturnResults";

					using (SqlCommand cmd = new SqlCommand(qry, conn))
					{
						cmd.Parameters.Add(new SqlParameter("@GUID", parameters.GUID == null ? (object)DBNull.Value :  parameters.GUID));
						cmd.Parameters.Add(new SqlParameter("@IsDeleted", parameters.IsDeleted == null ? (object)DBNull.Value :  parameters.IsDeleted));
						cmd.Parameters.Add(new SqlParameter("@ActiveDateTime", parameters.ActiveDateTime == null ? (object)DBNull.Value :  parameters.ActiveDateTime));
						cmd.Parameters.Add(new SqlParameter("@TerminationDateTime", parameters.TerminationDateTime == null ? (object)DBNull.Value :  parameters.TerminationDateTime));
						cmd.Parameters.Add(new SqlParameter("@Username", parameters.Username == null ? (object)DBNull.Value :  parameters.Username));
						cmd.Parameters.Add(new SqlParameter("@SystemUserGUID", parameters.SystemUserGUID == null ? (object)DBNull.Value :  parameters.SystemUserGUID));
						cmd.Parameters.Add(new SqlParameter("@ReturnResults", parameters.ReturnResults == null ? (object)DBNull.Value :  parameters.ReturnResults));
				        using (SqlDataReader reader = cmd.ExecuteReader())
				        {
				            while (reader.Read())
				            { 
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.IsDeleted = (bool)reader["IsDeleted"];
								if (String.IsNullOrWhiteSpace(reader["ActiveDateTime"].ToString()))
								{
								    //ret.ActiveDateTime = null;
								}
								else
								{
								    ret.ActiveDateTime = DateTime.Parse(reader["ActiveDateTime"].ToString());
								}
								if (String.IsNullOrWhiteSpace(reader["TerminationDateTime"].ToString()))
								{
								    //ret.TerminationDateTime = null;
								}
								else
								{
								    ret.TerminationDateTime = DateTime.Parse(reader["TerminationDateTime"].ToString());
								}
								ret.IsActiveForNow = (bool)reader["IsActiveForNow"];
								ret.Username = reader["Username"].ToString();
				            }
				        }
				    }
				}
				return ret;
			}
			public static spAuditLogUpsertResult spAuditLogUpsertCall(Guid? GUID, string Source, string TableName, string BeforeSnapshot, string AfterSnapshot, Guid? TableGUID, Guid? SystemUserGUID, bool? ReturnResults, string connectionString)
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
			public static spAuditLogUpsertResult spAuditLogUpsertCall (spAuditLogUpsertParameters parameters, string connectionString)
			{
				spAuditLogUpsertResult ret = new spAuditLogUpsertResult();
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
								ret.GUID = new Guid(reader["GUID"].ToString());
								ret.ID = int.Parse(reader["ID"].ToString());
								if (String.IsNullOrWhiteSpace(reader["DateTimeCreated"].ToString()))
								{
								    //ret.DateTimeCreated = null;
								}
								else
								{
								    ret.DateTimeCreated = DateTime.Parse(reader["DateTimeCreated"].ToString());
								}
								ret.Source = reader["Source"].ToString();
								ret.TableGUID = new Guid(reader["TableGUID"].ToString());
								ret.TableName = reader["TableName"].ToString();
								ret.BeforeSnapshot = reader["BeforeSnapshot"].ToString();
								ret.AfterSnapshot = reader["AfterSnapshot"].ToString();
								ret.SystemUserGUID = new Guid(reader["SystemUserGUID"].ToString());
				            }
				        }
				    }
				}
				return ret;
			}
	}
}
