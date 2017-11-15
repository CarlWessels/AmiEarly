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

	IF OBJECT_ID('dbo.Store', 'U') IS NOT NULL 
		DROP TABLE dbo.Store; 

	IF OBJECT_ID('dbo.Account', 'U') IS NOT NULL 
		DROP TABLE dbo.Account; 

	IF EXISTS ( (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'CustomerCatalog') ) 
		drop fulltext catalog CustomerCatalog;

	IF EXISTS ( (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'AccountCatalog') ) 
		drop fulltext catalog AccountCatalog;

	IF EXISTS ( (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'StoreCatalog') ) 
		drop fulltext catalog StoreCatalog;

	IF EXISTS ( (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'CustomerAddressCatalog') ) 
		drop fulltext catalog CustomerAddressCatalog;

	if EXISTS ( (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'SystemUserCatalog') ) 
		drop fulltext catalog SystemUserCatalog;

	if EXISTS ( (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'ServiceProviderCatalog') ) 
		drop fulltext catalog ServiceProviderCatalog;


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

	CREATE TABLE Account
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID(),
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

		AccountName VARCHAR(MAX) NOT NULL
		--SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

		CONSTRAINT PK_Account_GUID PRIMARY KEY (GUID)

	)
	go
	create fulltext catalog AccountCatalog as default
	go
	create fulltext index on Account(AccountName)
	key index PK_Account_GUID
	go
    
	declare @AccountGUID uniqueIdentifier
	declare @Guids table
	(guid uniqueIdentifier)
	delete from @Guids
	
	insert into dbo.Account	(IsDeleted,ActiveDateTime,TerminationDateTime,AccountName) 
	output Inserted.GUID into @Guids(guid)
	select 0, getDate(), null, 'SYSTEM ACCOUNT'

	select @AccountGUID = guid from @Guids


	CREATE TABLE Store 
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID(),
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
	
		AccountGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Account(GUID)
		--SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

		CONSTRAINT PK_Store_GUID PRIMARY KEY (GUID)
	)
	create fulltext catalog StoreCatalog as default
	create fulltext index on Store(StoreName)
	key index PK_Store_GUID


	declare @StoreGUID uniqueIdentifier
	delete from @Guids
	insert into dbo.Store	(IsDeleted,ActiveDateTime,TerminationDateTime,StoreName,AccountGUID) output Inserted.guid into @Guids(guid) select 0, getDate(), null, 'SYSTEM STORE', @AccountGUID
	select @StoreGUID = guid from @Guids

	CREATE TABLE SystemUser
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID(),
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
		PasswordSalt UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),

		AccountGUID UNIQUEIDENTIFIER NULL REFERENCES dbo.Account(GUID),
		StoreGUID UNIQUEIDENTIFIER NULL REFERENCES dbo.Store(GUID)

		CONSTRAINT PK_SystemUser_GUID PRIMARY KEY (GUID)
	)
	create fulltext catalog SystemUserCatalog as default
	create fulltext index on SystemUser(Username)
	key index PK_SystemUser_GUID

	DECLARE @Password VARCHAR(MAX) = 'PASSWORD'
	DECLARE @PasswordSalt UNIQUEIDENTIFIER = NEWID()

	INSERT INTO SystemUser(ActiveDateTime, Username, PasswordHash, PasswordSalt, AccountGUID, StoreGUID) SELECT		GETDATE(), 
																							'SYSTEM',
																							HASHBYTES('SHA2_512', @Password+CAST(@PasswordSalt AS NVARCHAR(36)))
																							, @PasswordSalt
																							, @AccountGUID
																							, @StoreGUID
	
	GO

	create TABLE Customer 
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID(),
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
		Combined as Surname + ', ' + Firstname,
		
		LinkedSystemUserGUID uniqueIdentifier not null references dbo.SystemUser(GUID),
		AccountGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Account(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

		CONSTRAINT PK_Customer_GUID PRIMARY KEY (GUID)
	)
	go
	create fulltext catalog CustomerCatalog as default
	go
	create fulltext index on Customer(Firstname, surname, EmailAddress, IDNumber, CellphoneNumber)
	key index PK_Customer_GUID
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
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID(),
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

		CONSTRAINT PK_CustomerAddress_GUID PRIMARY KEY (GUID)
	)
	go
	create fulltext catalog CustomerAddressCatalog as default
	go
	create fulltext index on CustomerAddress(Address1, Address2, Address3)
	key index PK_CustomerAddress_GUID
	go



	CREATE TABLE ServiceProvider
	(
		GUID UNIQUEIDENTIFIER NOT NULL DEFAULT  NEWSEQUENTIALID(),
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
		StoreGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Store(GUID),
		SystemUserGUID UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.SystemUser(GUID)

		CONSTRAINT PK_ServiceProvider_GUID PRIMARY KEY (GUID)
	)
		go
	create fulltext catalog ServiceProviderCatalog as default
	go
	create fulltext index on ServiceProvider(Firstname, Surname)
	key index PK_ServiceProvider_GUID
	go


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

