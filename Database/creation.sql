	SET NOCOUNT ON
	use TestDb
	GO
	IF OBJECT_ID('dbo.ActivitySchedule', 'U') IS NOT NULL 
		DROP TABLE dbo.ActivitySchedule; 

	IF OBJECT_ID('dbo.LUActivityType', 'U') IS NOT NULL 
		DROP TABLE dbo.LUActivityType; 

	IF OBJECT_ID('dbo.ActivitySchedule', 'U') IS NOT NULL 
		DROP TABLE dbo.ActivitySchedule; 

	IF OBJECT_ID('dbo.Appointment', 'U') IS NOT NULL 
		DROP TABLE dbo.Appointment; 

	IF OBJECT_ID('dbo.ServiceProvider', 'U') IS NOT NULL 
		DROP TABLE dbo.ServiceProvider; 

	IF OBJECT_ID('dbo.CustomerAddress', 'U') IS NOT NULL 
		DROP TABLE dbo.CustomerAddress; 

	IF OBJECT_ID('dbo.Customer', 'U') IS NOT NULL 
		DROP TABLE dbo.Customer; 

	IF OBJECT_ID('dbo.ActivitySchedule', 'U') IS NOT NULL 
		DROP TABLE dbo.ActivitySchedule; 

	IF OBJECT_ID('dbo.Store', 'U') IS NOT NULL 
		DROP TABLE dbo.Store; 

	IF OBJECT_ID('dbo.Account', 'U') IS NOT NULL 
		DROP TABLE dbo.Account; 

	IF OBJECT_ID('dbo.AuditLog', 'U') IS NOT NULL 
		DROP TABLE dbo.AuditLog; 
	
	IF OBJECT_ID('dbo.SystemUserPermission', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUserPermission; 

	IF OBJECT_ID('dbo.SystemUserGroupPermission', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUserGroupPermission; 

	IF OBJECT_ID('dbo.LUPermission', 'U') IS NOT NULL 
		DROP TABLE dbo.LUPermission; 

	IF OBJECT_ID('dbo.SystemUserGroupLine', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUserGroupLine; 

	IF OBJECT_ID('dbo.SystemUserGroup', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUserGroup; 


	IF OBJECT_ID('dbo.SystemUser', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUser; 

	IF OBJECT_ID('dbo.Setting', 'U') IS NOT NULL 
		DROP TABLE dbo.Setting; 

	IF OBJECT_ID('dbo.LUAddressType', 'U') IS NOT NULL 
		DROP TABLE dbo.LUAddressType; 

	create table Setting
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		Setting VARCHAR(max),
		TestValue VARCHAR(max),
		ProductionValue VARCHAR(max),
		IsProduction bit not null default 1,
		Value as case when IsProduction = 1 then ProductionValue else TestValue end
	)
	go
    insert into Setting (Setting, TestValue, ProductionValue) select 'TokenExpiryMinutes', 10, 10
	Go

		

	CREATE TABLE SystemUser
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		Token uniqueIdentifier null,
		TokenExpires DATETIME NULL,
		TokenIsValid as case when getDate() > TokenExpires then 0 else 1 end,
		Username VARCHAR(MAX) NOT NULL,
		PasswordHash BINARY(64) NOT NULL,
		PasswordSalt UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()
	)
	DECLARE @Password VARCHAR(MAX) = 'PASSWORD'
	DECLARE @PasswordSalt UNIQUEIDENTIFIER = NEWID()

	INSERT INTO SystemUser(ActiveDateTime, Username, PasswordHash, PasswordSalt) SELECT		GETDATE(), 
																							'SYSTEM',
																							HASHBYTES('SHA2_512', @Password+CAST(@PasswordSalt AS NVARCHAR(36)))
																							, @PasswordSalt

	CREATE TABLE Account
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,

		AccountName VARCHAR(MAX) NOT NULL,
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)
	GO

	CREATE TABLE Store 
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,

		StoreName VARCHAR(MAX) NOT NULL,
	
		AccountGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Account(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)
	)
	GO

	CREATE TABLE Customer 
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,

		Firstname VARCHAR(MAX),
		Surname VARCHAR(MAX),
		EmailAddress varchar(max),
		IDNumber varchar(max),
		BirthDate date,
		CellphoneNumber varchar(max),
		

		AccountGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Account(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)
	)
	go
    

	create table LUAddressType
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		AddressType varchar(max)
		
	)

	create table CustomerAddress
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		Address1 varchar(max),
		Address2 varchar(max),
		Address3 varchar(max),
		Code varchar(max),
		Province nVarchar(max),
		

		CustomerGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Customer(GUID),
		AddressTypeGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.LUAddressType(GUID)
	)
	go 



	CREATE TABLE ServiceProvider
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		Firstname VARCHAR(MAX),
		Surname VARCHAr(MAX),
		AccountGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Account(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)
	)
	GO

	CREATE TABLE Appointment
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		StartDateTime DATETIME NOT NULL,
		EndDateTime AS DATEADD(SECOND, -1 ,StartDateTime + CONVERT(DATETIME,Duration)) ,
		Duration TIME,
		ActualStartDateTime DATETIME,
		ActualEndDateTime DATETIME,
		CustomerGUID UNIQUEIDENTIFIER NOT NULL REFERENCES Customer(GUID),
		StoreGUID UNIQUEIDENTIFIER NOT NULL REFERENCES Store(GUID),
		ServiceProviderGUID UNIQUEIDENTIFIER NOT NULL REFERENCES ServiceProvider (GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)
	)
	GO

	CREATE TABLE LUActivityType
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActivityType VARCHAR(MAX) NOT NULL,
		AccountGUID UNIQUEIDENTIFIER NOT NULL REFERENCES Account (GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)
	)
	GO

	CREATE TABLE ActivitySchedule
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		DoW INT NOT NULL,
		StartTime TIME NOT NULL,
		EndTime TIME NOT NULL,

		ActivityTypeGUID UNIQUEIDENTIFIER NOT NULL REFERENCES LUActivityType(GUID),
		ServiceProviderGUID UNIQUEIDENTIFIER NOT NULL REFERENCES ServiceProvider(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)

	GO
	CREATE TABLE AuditLog
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),

		--Action VARCHAR(MAX),
		Source VARCHAR(MAX),
		TableGUID UNIQUEIDENTIFIER,
		TableName VARCHAR(MAX),
		BeforeSnapshot XML NULL,
		AfterSnapshot XML NULL,
		

		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)

	GO
	CREATE TABLE LUPermission
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,
		Permission VARCHAR(MAX),

		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)

	GO
	CREATE TABLE SystemUserPermission
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,
		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		ForSystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID),
		PermissionGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.LUPermission(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)

	GO
	CREATE TABLE SystemUserGroup
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,
		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		Description VARCHAR(MAX) NOT NULL,
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)

	GO
	CREATE TABLE SystemUserGroupPermission
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,
		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		PermissionGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.LUPermission(GUID),
		SystemUserGroupGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUserGroup(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)


	GO
	CREATE TABLE SystemUserGroupLine
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,
		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME NULL,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		ForSystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID),
		SystemUserGroupGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUserGroup(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

	)

