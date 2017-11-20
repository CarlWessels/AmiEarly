SET NOCOUNT ON
	DECLARE @SystemGUID UNIQUEIDENTIFIER
	SELECT @SystemGUID = (SELECT GUID FROM SystemUser WHERE Username = 'SYSTEM')
	DECLARE @CurDate DATETIME 

	EXEC spGenerateToken @SystemGUID

	DECLARE @Token uniqueIdentifier
	SELECT @Token = Token FROM dbo.SystemUser WHERE GUID = @SystemGUID
	
	SELECT @CurDate = GETDATE()

	
	declare @permissions table
	(
		Permission varchar(max),
		Parent varchar(max),
		Done bit not null default 0,
		ID int identity(1,1),
		guid uniqueidentifier
	)


	insert into @permissions
	(
		Permission,
		Parent
	)
	SELECT 'RootPermission', null
	union all
	select 'SystemPermissions', 'RootPermission'
	union all
	select 'CustomerPermissions', 'RootPermission'
	union all
	select 'MerchantPermissions', 'CustomerPermissions'
	
	union all
	select 'LUPermissionInsert', 'SystemPermissions'
	union all
	SELECT 'LUPermissionUpdate', 'SystemPermissions'
	UNION ALL
	SELECT 'LUPermissionGetAll', 'SystemPermissions'
	union all 
	select 'CrossAccountUpsert', 'SystemPermissions'
	union all 
	select 'CrossAccountGet', 'SystemPermissions'
	union all
	SELECT 'SystemUserPermissionInsert', 'SystemPermissions'
	UNION ALL
	SELECT 'SystemUserPermissionUpdate', 'SystemPermissions'
	UNION ALL
	select 'AccountInsert','SystemPermissions'
	UNION ALL
	select 'AccountUpdate','SystemPermissions'
	UNION ALL
	select 'AccountGetAll','SystemPermissions'
	UNION ALL
	select 'LUActivityTypeInsert','SystemPermissions'
	UNION ALL
	select 'LUActivityTypeUpdate','SystemPermissions'
	UNION ALL
	select 'LUActivityTypeGet','SystemPermissions'
	UNION ALL
	select 'LUActivityTypeGetAll','SystemPermissions'
	UNION ALL
	select 'LUAddressTypeInsert','SystemPermissions'
	UNION ALL
	select 'LUAddressTypeUpdate','SystemPermissions'
	UNION ALL
	select 'LUAddressTypeGet','SystemPermissions'
	UNION ALL
	select 'LUAddressTypeGetAll','SystemPermissions'
	UNION ALL
	select 'SystemUserGroupInsert','SystemPermissions'
	UNION ALL
	select 'SystemUserGroupUpdate','SystemPermissions'
	UNION ALL
	select 'SystemUserGroupGet','SystemPermissions'
	UNION ALL
	select 'SystemUserGroupGetAll','SystemPermissions'


	UNION ALL
	SELECT 'SystemUserUpsert', 'MerchantPermissions'
	UNION ALL
	SELECT 'MerchantServiceAccess', 'MerchantPermissions'
	union all 
	select 'CrossStoreUpsert', 'MerchantPermissions'
	union all 
	select 'CrossStoreGet', 'MerchantPermissions'
	union all
	select 'CustomerSearch', 'MerchantPermissions'
	UNION ALL
	SELECT 'SystemUserGetAll', 'MerchantPermissions'
	UNION ALL
	select 'AccountGet','MerchantPermissions'
	UNION ALL
	select 'ActivityScheduleInsert','MerchantPermissions'
	UNION ALL
	select 'ActivityScheduleUpdate','MerchantPermissions'
	UNION ALL
	select 'ActivityScheduleGet','MerchantPermissions'
	UNION ALL
	select 'ActivityScheduleGetAll','MerchantPermissions'
	UNION ALL
	select 'AppointmentInsert','MerchantPermissions'
	UNION ALL
	select 'AppointmentUpdate','MerchantPermissions'
	UNION ALL
	select 'AppointmentGetAll','MerchantPermissions'
	UNION ALL
	select 'ServiceProviderInsert','MerchantPermissions'
	UNION ALL
	select 'ServiceProviderUpdate','MerchantPermissions'
	UNION ALL
	select 'ServiceProviderGet','MerchantPermissions'
	UNION ALL
	select 'ServiceProviderGetAll','MerchantPermissions'
	UNION ALL
	select 'CustomerInsert','MerchantPermissions'
	UNION ALL
	select 'CustomerUpdate','MerchantPermissions'
	UNION ALL
	select 'CustomerGet','MerchantPermissions'
	UNION ALL
	select 'CustomerGetAll','MerchantPermissions'
	UNION ALL
	select 'CustomerAddressInsert','MerchantPermissions'
	UNION ALL
	select 'CustomerAddressUpdate','MerchantPermissions'
	UNION ALL
	select 'CustomerAddressGet','MerchantPermissions'
	UNION ALL
	select 'CustomerAddressGetAll','MerchantPermissions'
	UNION ALL
	select 'StoreInsert','MerchantPermissions'
	UNION ALL
	select 'StoreUpdate','MerchantPermissions'
	UNION ALL
	select 'StoreGet','MerchantPermissions'
	UNION ALL
	select 'StoreGetAll','MerchantPermissions'


	UNION all
	SELECT 'CustomerServiceAccess', 'CustomerPermissions'
	UNION all
	SELECT 'LUPermissionGet', 'CustomerPermissions'
	UNION all
	SELECT 'SystemUserPermissionGet', 'CustomerPermissions'
	UNION ALL
	SELECT 'SystemUserPermissionGetAll', 'MerchantPermissions'
	UNION ALL
	SELECT 'SystemUserGet', 'CustomerPermissions'
	UNION ALL
	select 'AppointmentGet','CustomerPermissions'



	declare @pi int 
	select @pi = min(id) from @permissions where done = 0

	while @pi is not null
	begin
	
		declare @permission varchar(max), 
				@parent varchar(max)


		select @permission = Permission,
				@parent = parent
		from @permissions 
		where id = @pI
	
		declare @parentGUId uniqueIdentifier 
		select @parentGUID = GUID from dbo.LUPermission where Permission = @parent 

		declare @Guids table 
		(
			Guid uniqueIdentifier
		)
		delete from @Guids

		insert into dbo.LUPermission (IsDeleted,Permission,ParentGUID,SystemUserGUID)
		output	 inserted.guid into @GUIDS (guid)
		select 0, @permission,@parentGUId, @SystemGUID
	
		declare @Guid uniqueIdentifier 
		select @Guid = guid from @Guids

		update @permissions 
		set done = 1 
			,guid = @Guid
		where id = @Pi

		select @pi = min(id) from @permissions where done = 0

	end



	declare @permSystemguid uniqueIdentifier 
	select @permSystemguid = guid from dbo.LUPermission where Permission = 'RootPermission'


	insert into dbo.SystemUserPermission(IsDeleted,ActiveDateTime,ForSystemUserGUID,PermissionGUID,SystemUserGUID)
	select 0, getDate(), @SystemGUID, @permSystemguid, @SystemGUID




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

			/*EXEC spLUPermissionUpsert NULL, 0, @InsertPermission, @Token, 0
			EXEC spLUPermissionUpsert NULL, 0, @UpdatePermission, @Token, 0
			EXEC spLUPermissionUpsert NULL, 0, @GetPermission, @Token, 0
			EXEC spLUPermissionUpsert NULL, 0, @GetAllPermission, @Token, 0*/

			DECLARE @InsertPGUID UNIQUEIDENTIFIER
			DECLARE @UpdatePGUID UNIQUEIDENTIFIER
			DECLARE @GetPGUID UNIQUEIDENTIFIER
			DECLARE @GetAllPGUID UNIQUEIDENTIFIER

			SELECT @InsertPGUID = (SELECT GUID FROM LUPermission WHERE Permission = @InsertPermission)
			SELECT @UpdatePGUID = (SELECT GUID FROM LUPermission WHERE Permission = @UpdatePermission)
			SELECT @GetPGUID = (SELECT GUID FROM LUPermission  WHERE Permission = @GetPermission)
			SELECT @GetAllPGUID = (SELECT GUID FROM LUPermission  WHERE Permission = @GetAllPermission)

		END

		
		

		UPDATE @Tables SET Done = 1 WHERE ID = @I 
		SELECT @I = MIN(ID) FROM @Tables WHERE Done = 0
	END	

	insert into LUAddressType (DateTimeCreated,IsDeleted,ActiveDateTime,TerminationDateTime,AddressType) select	@CurDate, 0, @CurDate, null, 'HOME'
	insert into LUAddressType (DateTimeCreated,IsDeleted,ActiveDateTime,TerminationDateTime,AddressType) select	@CurDate, 0, @CurDate, null, 'WORK'


