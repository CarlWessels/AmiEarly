SET NOCOUNT ON
	DECLARE @SystemGUID UNIQUEIDENTIFIER
	SELECT @SystemGUID = (SELECT GUID FROM SystemUser WHERE Username = 'SYSTEM')
	DECLARE @CurDate DATETIME 

	EXEC spGenerateToken @SystemGUID

	DECLARE @Token uniqueIdentifier
	SELECT @Token = Token FROM dbo.SystemUser WHERE GUID = @SystemGUID
	
	SELECT @CurDate = GETDATE()

	INSERT INTO dbo.LUPermission
	(
	    Permission,
	    SystemUserGUID
	)
	select n.*
	from
	(

		SELECT Permission = 'LUPermissionInsert', UserGUID = @SystemGUID
		UNION ALL
		SELECT 'LUPermissionUpdate', @SystemGUID
		UNION ALL
		SELECT 'LUPermissionGet', @SystemGUID
		UNION ALL
		SELECT 'LUPermissionGetAll', @SystemGUID
		UNION ALL
		SELECT 'SystemUserUpsert', @SystemGUID
		UNION ALL
		SELECT 'MerchantServiceAccess', @SystemGUID
		UNION ALL
		SELECT 'CustomerServiceAccess', @SystemGUID
		union all 
		select 'CrossAccountUpsert', @SystemGUID
		union all 
		select 'CrossStoreUpsert', @SystemGUID
		union all 
		select 'CrossAccountGet', @SystemGUID
		union all 
		select 'CrossStoreGet', @SystemGUID
		union all
		select 'CustomerSearch', @SystemGUID
	) n
	left join dbo.LUPermission o on n.Permission = o.Permission 
	where o.GUID is null
	
	insert INTO dbo.LUPermission
	(
	    Permission,
	    SystemUserGUID
	)
	SELECT 'SystemUserPermissionInsert', @SystemGUID
	UNION ALL
	SELECT 'SystemUserPermissionUpdate', @SystemGUID
	UNION ALL
	SELECT 'SystemUserPermissionGet', @SystemGUID
	UNION ALL
	SELECT 'SystemUserPermissionGetAll', @SystemGUID
	UNION ALL
	SELECT 'SystemUserGetAll', @SystemGUID
	UNION ALL
	SELECT 'SystemUserGet', @SystemGUID


	INSERT INTO dbo.SystemUserPermission
	(
	    ForSystemUserGUID,
	    PermissionGUID,
	    SystemUserGUID
	)
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'LUPermissionInsert') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'LUPermissionUpdate') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'LUPermissionGet') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'LUPermissionGetAll') , @SystemGUID
	
	
	insert INTO dbo.SystemUserPermission
	(
	    ForSystemUserGUID,
	    PermissionGUID,
	    SystemUserGUID
	)
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'SystemUserPermissionInsert') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'SystemUserPermissionUpdate') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'SystemUserPermissionGet') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'SystemUserPermissionGetAll') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'SystemUserGetAll') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'SystemUserGet') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.LUPermission WHERE Permission = 'MerchantServiceAccess') , @SystemGUID



	DECLARE @Tables TABLE
	(
		TableName VARCHAR(MAX),
		CreateUpsert BIT,
		CreateToXML BIT,
		CreatePermission BIT,

		ID INT IDENTITY(1,1) NOT NULL,
		Done BIT NOT NULL DEFAULT 0

	)

	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Account',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'ActivitySchedule',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'LUActivityType',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'LUAddressType',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Appointment',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'ServiceProvider',0,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Customer',0,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'CustomerAddress',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Store',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'LUPermission',1,1,0
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'SystemUserGroup',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'SystemUserGroupPermission',1,1,0

	DECLARE @I INT
	SELECT @I = MIN(ID) FROM @Tables WHERE Done = 0

	WHILE @I IS NOT NULL
	begin
		DECLARE @TableName VARCHAR(MAX)
				,@CreateUpsert BIT 
				,@CreateToXML BIT 
				,@CreatePermission BIT 


		--select @TableName
		SET @CreateUpsert = NULL
		SET @TableName = NULL

		SELECT @TableName = TableName,
				@CreateUpsert = CreateUpsert
				,@CreateToXML = CreateToXML
				,@CreatePermission = CreatePermission
		FROM @Tables 
		WHERE ID = @I

		IF (@CreateUpsert = 1)
		BEGIN 
			EXEC dbo.spCreateUpsert @TableName
		END
		
		IF (@CreateToXML= 1 )
		BEGIN
			EXEC spCreateToXml @TableName
		END
		
		IF (@CreatePermission = 1 )
		BEGIN
			DECLARE @InsertPermission VARCHAR(MAX) = (SELECT @TableName + 'Insert')
			DECLARE @UpdatePermission VARCHAR(MAX) = (SELECT @TableName + 'Update')
			DECLARE @GetPermission VARCHAR(MAX) = (SELECT @TableName + 'Get')
			DECLARE @GetAllPermission VARCHAR(MAX) = (SELECT @TableName + 'GetAll')

			EXEC spLUPermissionUpsert NULL, 0, @InsertPermission, @Token, 0
			EXEC spLUPermissionUpsert NULL, 0, @UpdatePermission, @Token, 0
			EXEC spLUPermissionUpsert NULL, 0, @GetPermission, @Token, 0
			EXEC spLUPermissionUpsert NULL, 0, @GetAllPermission, @Token, 0

			DECLARE @InsertPGUID UNIQUEIDENTIFIER
			DECLARE @UpdatePGUID UNIQUEIDENTIFIER
			DECLARE @GetPGUID UNIQUEIDENTIFIER
			DECLARE @GetAllPGUID UNIQUEIDENTIFIER

			SELECT @InsertPGUID = (SELECT GUID FROM LUPermission WHERE Permission = @InsertPermission)
			SELECT @UpdatePGUID = (SELECT GUID FROM LUPermission WHERE Permission = @UpdatePermission)
			SELECT @GetPGUID = (SELECT GUID FROM LUPermission  WHERE Permission = @GetPermission)
			SELECT @GetAllPGUID = (SELECT GUID FROM LUPermission  WHERE Permission = @GetAllPermission)

			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @InsertPGUID, @Token, 0
			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @UpdatePGUID, @Token,0
			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @GetPGUID, @Token,0
			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @GetAllPGUID, @Token,0
		END

		
		

		UPDATE @Tables SET Done = 1 WHERE ID = @I 
		SELECT @I = MIN(ID) FROM @Tables WHERE Done = 0
	END	

	insert into LUAddressType (DateTimeCreated,IsDeleted,ActiveDateTime,TerminationDateTime,AddressType) select	@CurDate, 0, @CurDate, null, 'HOME'
	insert into LUAddressType (DateTimeCreated,IsDeleted,ActiveDateTime,TerminationDateTime,AddressType) select	@CurDate, 0, @CurDate, null, 'WORK'


