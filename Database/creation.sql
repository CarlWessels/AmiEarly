	SET NOCOUNT ON

	IF OBJECT_ID('dbo.ActivitySchedule', 'U') IS NOT NULL 
		DROP TABLE dbo.ActivitySchedule; 

	IF OBJECT_ID('dbo.ActivityType', 'U') IS NOT NULL 
		DROP TABLE dbo.ActivityType; 

	IF OBJECT_ID('dbo.ActivitySchedule', 'U') IS NOT NULL 
		DROP TABLE dbo.ActivitySchedule; 

	IF OBJECT_ID('dbo.Appointment', 'U') IS NOT NULL 
		DROP TABLE dbo.Appointment; 

	IF OBJECT_ID('dbo.ServiceProvider', 'U') IS NOT NULL 
		DROP TABLE dbo.ServiceProvider; 

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

	IF OBJECT_ID('dbo.Permission', 'U') IS NOT NULL 
		DROP TABLE dbo.Permission; 

	IF OBJECT_ID('dbo.SystemUserGroupLine', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUserGroupLine; 

	IF OBJECT_ID('dbo.SystemUserGroup', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUserGroup; 


	IF OBJECT_ID('dbo.SystemUser', 'U') IS NOT NULL 
		DROP TABLE dbo.SystemUser; 


		

	CREATE TABLE SystemUser
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		Token VARBINARY(MAX) NULL,
		TokenExpires DATETIME NULL,
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
		TerminationDateTime DATETIME,
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
		TerminationDateTime DATETIME,
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
		TerminationDateTime DATETIME,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,

		Firstname VARCHAR(MAX),
		Surname VARCHAR(MAX),

		AccountGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Account(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)
	)
	GO

	CREATE TABLE ServiceProvider
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID() PRIMARY KEY,
		ID INT IDENTITY(1,1) NOT  NULL,
		DateTimeCreated DATETIME NOT NULL DEFAULT GETDATE(),
		IsDeleted BIT NOT NULL DEFAULT 0,

		ActiveDateTime DATETIME NOT NULL DEFAULT GETDATE(),
		TerminationDateTime DATETIME,
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

	CREATE TABLE ActivityType
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

		ActivityTypeGUID UNIQUEIDENTIFIER NOT NULL REFERENCES ActivityType(GUID),
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
	CREATE TABLE Permission
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
		TerminationDateTime DATETIME,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		ForSystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID),
		PermissionGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Permission(GUID),
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
		TerminationDateTime DATETIME,
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
		TerminationDateTime DATETIME,
		IsActiveForNow  AS		CASE 
									WHEN IsDeleted = CONVERT(BIT,0) THEN CONVERT(BIT,1 )
									ELSE	CASE	
													WHEN GETDATE() BETWEEN ActiveDateTime AND ISNULL(TerminationDateTime, '2099-01-01') THEN CONVERT(BIT,1)
													ELSE CONVERT(BIT,0)
											END
								END,
		PermissionGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Permission(GUID),
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
		TerminationDateTime DATETIME,
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

