ALTER PROC spCreateToXml
(
	@TableName VARCHAR(MAx)
)
AS
BEGIN
	DECLARE @Result VARCHAR(MAX) = ''
	DECLARE @EOL VARCHAR(MAX) = CHAR(13)+CHAR(10)
	DECLARE @ProcName VARCHAR(MAX)
	SET @ProcName = 'sp' + @TableName + 'ToXML'

	SET @Result = @Result + 'IF EXISTS(SELECT 1 FROM   INFORMATION_SCHEMA.ROUTINES WHERE  ROUTINE_NAME = ''' + @ProcName+ ''' AND SPECIFIC_SCHEMA = ''dbo'')' + @EOL
	SET @Result = @Result + 'BEGIN' + @EOL
    SET @Result = @Result + '	DROP PROCEDURE ' + @ProcName + @EOL
	SET @Result = @Result + 'END' + @EOL
	SET @Result = @Result + 'GO' + @EOL


	SET @Result = @Result + 'CREATE PROCEDURE sp' + @TableName + 'ToXML' + @EOL
	SET @Result = @Result + '(' + @EOL
	SET @Result = @Result + '	@GUIDS VARCHAR(MAX),' + @EOL
	SET @Result = @Result + '	@Token VARBINARY(MAX)' + @EOL
	SET @Result = @Result + ')' + @EOL
	SET @Result = @Result + 'AS' + @EOL
	SET @Result = @Result + 'BEGIN' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	DECLARE @SystemUserGUID UNIQUEIDENTIFIER' + @EOL
	SET @Result = @Result + '	SELECT @SystemUserGUID = GUID FROM SystemUser WHERE Token = @Token AND TokenIsValid = 1' + @EOL
	SET @Result = @Result + '	IF @SystemUserGUID IS NULL' + @EOL
	SET @Result = @Result + '	BEGIN' + @EOL
	SET @Result = @Result + '		THROW 51000, ''Invalid token'', 1;  '+ @EOL
	SET @Result = @Result + '	END' + @EOL
	SET @Result = @Result + '	DECLARE @GuidsSplit TABLE' + @EOL
	SET @Result = @Result + '	(' + @EOL
	SET @Result = @Result + '		GUID UNIQUEIDENTIFIER' + @EOL
	SET @Result = @Result + '	)' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	INSERT INTO @GuidsSplit' + @EOL
	SET @Result = @Result + '	(' + @EOL
	SET @Result = @Result + '		GUID' + @EOL
	SET @Result = @Result + '	)' + @EOL
	SET @Result = @Result + '	SELECT SplitData' + @EOL
	SET @Result = @Result + '	FROM dbo.fnSplitString(@GUIDS, ''|'')' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	SELECT XML = (SELECT ' + @TableName + '.* ' + @EOL
	SET @Result = @Result + '	FROM dbo.' + @TableName + ' ' + @TableName + '' + @EOL
	SET @Result = @Result + '	INNER JOIN @GuidsSplit G ON ' + @TableName + '.GUID = g.GUID' + @EOL
	SET @Result = @Result + '	FOR XML AUTO)' + @EOL
	SET @Result = @Result + 'END' + @EOL
	SET @Result = @Result + 'GO' + @EOL


	DECLARE @DateTimeProcName VARCHAR(MAX)
	SET @DateTimeProcName  = 'sp' + @TableName + 'ToXMLByDateTime'

	SET @Result = @Result + 'IF EXISTS(SELECT 1 FROM   INFORMATION_SCHEMA.ROUTINES WHERE  ROUTINE_NAME = ''' + @DateTimeProcName + ''' AND SPECIFIC_SCHEMA = ''dbo'')' + @EOL
	SET @Result = @Result + 'BEGIN' + @EOL
    SET @Result = @Result + '	DROP PROCEDURE ' + @DateTimeProcName  + @EOL
	SET @Result = @Result + 'END' + @EOL
	SET @Result = @Result + 'GO' + @EOL

	
	SET @Result = @Result + 'CREATE PROCEDURE [dbo].[' + @DateTimeProcName + ']' + @EOL
	SET @Result = @Result + '(' + @EOL
	SET @Result = @Result + '	@FromDateTime DATETIME' + @EOL
	SET @Result = @Result + '	,@ToDateTime DATETIME' + @EOL
	SET @Result = @Result + '	,@Token VARBINARY(MAX)' + @EOL
	SET @Result = @Result + ')' + @EOL
	SET @Result = @Result + 'AS' + @EOL
	SET @Result = @Result + 'BEGIN' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	DECLARE @SystemUserGUID UNIQUEIDENTIFIER' + @EOL
	SET @Result = @Result + '	SELECT @SystemUserGUID = GUID FROM SystemUser WHERE Token = @Token AND TokenIsValid = 1' + @EOL
	SET @Result = @Result + '	IF @SystemUserGUID IS NULL' + @EOL
	SET @Result = @Result + '	BEGIN' + @EOL
	SET @Result = @Result + '		THROW 51000, ''Invalid token'', 1;  '+ @EOL
	SET @Result = @Result + '	END' + @EOL
	SET @Result = @Result + '	DECLARE @Guids TABLE ' + @EOL
	SET @Result = @Result + '	(' + @EOL
	SET @Result = @Result + '		GUID UNIQUEIDENTIFIER,' + @EOL
	SET @Result = @Result + '		ID INT IDENTITY(1,1) NOT NULL,' + @EOL
	SET @Result = @Result + '		Done BIT NOT NULL DEFAULT 0' + @EOL
	SET @Result = @Result + '	)' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	INSERT INTO @GUIDS (GUID)' + @EOL
	SET @Result = @Result + '	SELECT GUID' + @EOL
	SET @Result = @Result + '	FROM dbo.' + @TableName + '' + @EOL
	SET @Result = @Result + '	WHERE DateTimeCreated BETWEEN @FromDateTime AND @ToDateTime ' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	DECLARE @Val VARCHAR(MAX) = ''''' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	DECLARE @I INT ' + @EOL
	SET @Result = @Result + '	SELECT @I = MIN(ID) FROM @Guids WHERE Done = 0' + @EOL
	SET @Result = @Result + '	WHILE @I IS NOT NULL' + @EOL
	SET @Result = @Result + '	BEGIN' + @EOL
	SET @Result = @Result + '		SELECT @Val = @Val + CONVERT(VARCHAR(MAX),GUID) + ''|'' FROM @Guids WHERE ID = @I' + @EOL
	SET @Result = @Result + '		UPDATE @Guids SET DONE = 1 WHERE ID = @I' + @EOL
	SET @Result = @Result + '		SELECT @I = MIN(ID) FROM @Guids WHERE Done = 0' + @EOL
	SET @Result = @Result + '	END' + @EOL
	SET @Result = @Result + '' + @EOL
	SET @Result = @Result + '	EXEC ' + @ProcName + ' @Val, @Token' + @EOL
	SET @Result = @Result + 'END' + @EOL
	SET @Result = @Result + 'GO' + @EOL
	

	PRINT @Result
END


