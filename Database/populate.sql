SET NOCOUNT ON
	DECLARE @SystemGUID UNIQUEIDENTIFIER
	SELECT @SystemGUID = (SELECT GUID FROM SystemUser WHERE Username = 'SYSTEM')
	DECLARE @CurDate DATETIME 

	EXEC spGenerateToken @SystemGUID

	DECLARE @Token VARBINARY(MAX)
	SELECT @Token = Token FROM dbo.SystemUser WHERE GUID = @SystemGUID
	
	SELECT @CurDate = GETDATE()

	INSERT INTO dbo.Permission
	(
	    Permission,
	    SystemUserGUID
	)
	SELECT 'PermissionInsert', @SystemGUID
	UNION ALL
	SELECT 'PermissionUpdate', @SystemGUID
	UNION ALL
	SELECT 'PermissionGet', @SystemGUID
	UNION ALL
	SELECT 'PermissionGetAll', @SystemGUID
	UNION ALL
	SELECT 'SystemUserUpsert', @SystemGUID


	INSERT INTO dbo.Permission
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
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'PermissionInsert') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'PermissionUpdate') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'PermissionGet') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'PermissionGetAll') , @SystemGUID

	INSERT INTO dbo.SystemUserPermission
	(
	    ForSystemUserGUID,
	    PermissionGUID,
	    SystemUserGUID
	)
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'SystemUserPermissionInsert') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'SystemUserPermissionUpdate') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'SystemUserPermissionGet') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'SystemUserPermissionGetAll') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'SystemUserGetAll') , @SystemGUID
	UNION ALL
	SELECT @SystemGUID, (SELECT GUID FROM dbo.Permission WHERE Permission = 'SystemUserGet') , @SystemGUID



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
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'ActivityType',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Appointment',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'ServiceProvider',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Customer',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Store',1,1,1
	INSERT INTO @Tables	(TableName,CreateUpsert, CreateToXML, CreatePermission) SELECT 'Permission',1,1,0
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

			EXEC spPermissionUpsert NULL, 0, @InsertPermission, @Token, 0
			EXEC spPermissionUpsert NULL, 0, @UpdatePermission, @Token, 0
			EXEC spPermissionUpsert NULL, 0, @GetPermission, @Token, 0
			EXEC spPermissionUpsert NULL, 0, @GetAllPermission, @Token, 0

			DECLARE @InsertPGUID UNIQUEIDENTIFIER
			DECLARE @UpdatePGUID UNIQUEIDENTIFIER
			DECLARE @GetPGUID UNIQUEIDENTIFIER
			DECLARE @GetAllPGUID UNIQUEIDENTIFIER

			SELECT @InsertPGUID = (SELECT GUID FROM Permission WHERE Permission = @InsertPermission)
			SELECT @UpdatePGUID = (SELECT GUID FROM Permission WHERE Permission = @UpdatePermission)
			SELECT @GetPGUID = (SELECT GUID FROM Permission WHERE Permission = @GetPermission)
			SELECT @GetAllPGUID = (SELECT GUID FROM Permission WHERE Permission = @GetAllPermission)

			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @InsertPGUID, @Token, 0
			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @UpdatePGUID, @Token,0
			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @GetPGUID, @Token,0
			EXEC spSystemUserPermissionUpsert NULL, 0, @CurDate, NULL, @SystemGUID, @GetAllPGUID, @Token,0
		END

		
		

		UPDATE @Tables SET Done = 1 WHERE ID = @I 
		SELECT @I = MIN(ID) FROM @Tables WHERE Done = 0
	END	

	DECLARE @PermissionTypes TABLE
	(
		PermissionType VARCHAR(MAX)
	)

	



	