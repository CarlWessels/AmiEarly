ALTER PROCEDURE dbo.spCreateUpsert
(
	@TableName VARCHAR(MAX)
)
AS
BEGIN

	SET NOCOUNT ON
	
	DECLARE @EOL VARCHAR(MAX) = CHAR(13)+CHAR(10)
	DECLARE @Fields TABLE
	(
		Name VARCHAR(MAX)
		,Type VARCHAR(MAX)
		,Nullable BIT 
		,Computed BIT
		,ID INT IDENTITY(1,1) NOT NULL
		,Done BIT NOT NULL DEFAULT 0
	)

	INSERT INTO @Fields(Name, Type, Nullable, Computed)
	SELECT COLUMN_NAME, DATA_TYPE, CASE WHEN IS_NULLABLE = 'YES' THEN 1 ELSE 0 END, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA+'.'+TABLE_NAME),COLUMN_NAME,'IsComputed') 
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE table_name = @TableName
	/*
	SELECT *, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA+'.'+TABLE_NAME),COLUMN_NAME,'IsComputed') 
		AS IS_COMPUTED
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE table_name = 'Appointment'

	SELECT * FROM sys.computed_columns
	WHERE object_id = OBJECT_ID('Appointment')
	*/
	DECLARE @ProcName VARCHAR(MAX) = 'sp' + @TableName + 'Upsert'

	DECLARE @Result VARCHAR(MAX) = ''
	SET @Result = @Result + 'IF EXISTS(SELECT 1 FROM   INFORMATION_SCHEMA.ROUTINES WHERE  ROUTINE_NAME = ''' + @ProcName+ ''' AND SPECIFIC_SCHEMA = ''dbo'')' + @EOL
	SET @Result = @Result + 'BEGIN' + @EOL
    SET @Result = @Result + '	DROP PROCEDURE ' + @ProcName + @EOL
	SET @Result = @Result + 'END' + @EOL
	SET @Result = @Result + 'GO' + @EOL
	SET @Result = @Result + 'CREATE PROCEDURE sp' + @TableName + 'Upsert' + @EOL
	SET @Result = @Result + '(' + @EOL

	DECLARE @I INT 
	SELECT @I = (SELECT MIN(ID) FROM @Fields WHERE Done = 0)

	DECLARE @Name VARCHAR(MAX)
	DECLARE @Type VARCHAR(MAX)
	DECLARE @Nullable BIT
	DECLARE @Computed BIT 

	WHILE (@I IS NOT NULL)
	BEGIN
		SET @Name = ''	
		SET @Type = ''
		SET @Nullable = NULL
		SET @Computed = NULL
		SELECT @Name = Name 
				,@Type = Type
				,@Nullable = Nullable
				,@Computed = Computed
		FROM @Fields 
		WHERE ID = @I
	
		IF (@Computed = 0 AND @Name != 'ID' AND @Name != 'DateTimeCreated')
		BEGIN
			IF UPPER(@Type) = 'VARCHAR'
			BEGIN
				SET @Type = 'VARCHAR(MAX)'
			END
			SET @Result = @Result + '	@' + @Name + ' ' + @Type + CASE WHEN @Nullable = 1 THEN ' = NULL' ELSE '' END + ',' + @EOL
		END
	
		UPDATE @Fields SET Done = 1 WHERE ID = @I
		SELECT @I = (SELECT MIN(ID) FROM @Fields WHERE Done = 0)
	END
	SET @Result = @Result + '	@SystemUserGUID UNIQUEIDENTIFIER,' + @EOL
	SET @Result = @Result + '	@ReturnResults BIT = 0' + @EOL
	SET @Result = @Result + ')' + @EOL
	SET @Result = @Result + 'AS' + @EOL
	SET @Result = @Result + 'BEGIN' + @EOL
	SET @Result = @Result + '	DECLARE @BeforeXML NVARCHAR(MAX)' + @EOL
	SET @Result = @Result + '	DECLARE @AfterXML NVARCHAR(MAX)' + @EOL

	SET @Result = @Result + '	IF (@GUID IS NULL)' + @EOL
	SET @Result = @Result + '	BEGIN' + @EOL
	SET @Result = @Result + '		DECLARE @GUIDS TABLE (GUID UNIQUEIDENTIFIER)' + @EOL
	SET @Result = @Result + '		INSERT INTO ' + @TableName

	UPDATE @Fields SET Done = 0
	SELECT @I = (SELECT MIN(ID) FROM @Fields WHERE Done = 0)

	DECLARE @FieldNames VARCHAR(MAX) = '('
	DECLARE @Values VARCHAR(MAX) = '		SELECT ' + @EOL

	WHILE (@I IS NOT NULL)
	BEGIN
		SET @Name = ''	
		SET @Type = ''
		SET @Nullable = NULL
		SET @Computed = NULL
		SELECT @Name = Name 
				,@Type = Type
				,@Nullable = Nullable
				,@Computed = Computed
		FROM @Fields 
		WHERE ID = @I

		UPDATE @Fields SET Done = 1 WHERE ID = @I
		SELECT @I = (SELECT MIN(ID) FROM @Fields WHERE Done = 0)

		IF (@Computed = 0 AND @Name != 'GUID' AND @Name != 'ID' AND @Name != 'DateTimeCreated')
		BEGIN
			SET @FieldNames = @FieldNames + @Name
			SET @Values = @Values + '			' + @Name + ' = @' + @Name

			IF (@I IS NOT NULL)
			BEGIN
				SET @FieldNames = @FieldNames + ','
				SET @Values = @Values + ','
			END
			SET @Values = @Values + @EOL
		
		END
	END
	SET @Result = @Result + @FieldNames + ')' + @EOL
	SET @Result = @Result + '		OUTPUT inserted.GUID INTO @GUIDS (GUID)' + @EOL
	SET @Result = @Result + @Values + @EOL
	SET @Result = @Result + '		SELECT @GUID = GUID FROM @GUIDS' + @EOL

	IF (@TableName != 'AuditLog')
	BEGIN
		SET @Result = @Result + '		SET @AfterXML = (SELECT * from ' + @TableName + ' WHERE GUID = @GUID FOR XML AUTO)' + @EOL
		SET @Result = @Result + '		EXEC spAuditLogUpsert NULL, ''sp' + @TableName + 'Upsert'', ''' + @TableName + ''',@BeforeXML, @AfterXML, @GUID,  @SystemUserGUID,0' +  + @EOL
	END

	SET @Result = @Result + '	END' + @EOL
	SET @Result = @Result + '	ELSE' + @EOL
	SET @Result = @Result + '	BEGIN' + @EOL
	SET @Result = @Result + '		SET @BeforeXML = (SELECT * from ' + @TableName + ' WHERE GUID = @GUID FOR XML AUTO)' + @EOL
	--SET @Result = @Result + '		SELECT @BeforeXML' + @EOL
	SET @Result = @Result + '		UPDATE ' + @TableName + @EOL
	SET @Result = @Result + '		SET' + @EOL
	UPDATE @Fields SET Done = 0
	SELECT @I = (SELECT MIN(ID) FROM @Fields WHERE Done = 0)

	WHILE (@I IS NOT NULL)
	BEGIN
		SET @Name = ''	
		SET @Type = ''
		SET @Nullable = NULL
		SET @Computed = NULL
		SELECT @Name = Name 
				,@Type = Type
				,@Nullable = Nullable
				,@Computed = Computed
		FROM @Fields 
		WHERE ID = @I

		UPDATE @Fields SET Done = 1 WHERE ID = @I
		SELECT @I = (SELECT MIN(ID) FROM @Fields WHERE Done = 0)

		IF (@Computed = 0 AND @Name != 'GUID' AND @Name != 'ID' AND @Name != 'DateTimeCreated')
		BEGIN
			SET @Result = @Result + '			' + @Name + ' = @' + @Name
			IF (@I IS NOT NULL)
			BEGIN
				SET @Result = @Result + ','
			END
			SET @Result = @Result + @EOL
		END



	END
	SET @Result = @Result + '		WHERE GUID = @GUID' + @EOL
	SET @Result = @Result + '		SET @AfterXML = (SELECT * from ' + @TableName + ' WHERE GUID = @GUID FOR XML AUTO)' + @EOL
	IF (@TableName != 'AuditLog')
	BEGIN
		SET @Result = @Result + '		EXEC spAuditLogUpsert NULL, ''sp' + @TableName + 'Upsert'', ''' + @TableName + ''', @BeforeXML, @AfterXML, @GUID,  @SystemUserGUID, 0' +  + @EOL
	END

	SET @Result = @Result + '	END' + @EOL
	SET @Result = @Result + '	IF (@ReturnResults = 1)' + @EOL
	SET @Result = @Result + '	BEGIN' + @EOL
	SET @Result = @Result + '		SELECT *' + @EOL
	SET @Result = @Result + '		FROM ' + @TableName + @EOL
	SET @Result = @Result + '		WHERE GUID = @GUID' + @EOL
	SET @Result = @Result + '	END' + @EOL


	SET @Result = @Result + 'END' + @EOL
	SET @Result = @Result + 'GO' + @EOL


	PRINT @Result

END
GO
