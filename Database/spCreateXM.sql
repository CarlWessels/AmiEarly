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
	SET @Result = @Result + '	@GUIDS VARCHAR(MAX)' + @EOL
	SET @Result = @Result + ')' + @EOL
	SET @Result = @Result + 'AS' + @EOL
	SET @Result = @Result + 'BEGIN' + @EOL
	SET @Result = @Result + '' + @EOL
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
	SET @Result = @Result + '	SELECT ' + @TableName + '.* ' + @EOL
	SET @Result = @Result + '	FROM dbo.' + @TableName + ' ' + @TableName + '' + @EOL
	SET @Result = @Result + '	INNER JOIN @GuidsSplit G ON ' + @TableName + '.GUID = g.GUID' + @EOL
	SET @Result = @Result + '	FOR XML AUTO' + @EOL
	SET @Result = @Result + 'END' + @EOL
	SET @Result = @Result + 'GO' + @EOL
	

	DECLARE @FromDateTime DATETIME = '2017-10-25 12:23:33.150'

	SELECT * FROM dbo.Appointment
	DECLARE @ToDateTime DATETIME = '2017-10-27 12:23:33.150'

	
	SELECT DISTINCT	GUIDS = SUBSTRING(
        (
            SELECT '|'+CONVERT(VARCHAR(MAX),GUID) 
            FROM dbo.Appointment 
			WHERE DateTimeCreated BETWEEN @FromDateTime AND @ToDateTime
            For XML PATH ('')
        ), 2, 1000) 
	FROM dbo.Appointment 
	EXEC dbo.AppointmentToXml

	PRINT @Result
END


