
ALTER PROCEDURE [dbo].[spAuditLogUpsert]
(
	@GUID uniqueidentifier,
	--@Action VARCHAR(MAX) = NULL,
	@Source VARCHAR(MAX) = NULL,
	@TableName VARCHAR(MAX) = NULL,
	@BeforeSnapshot NVARCHAR(MAX) = NULL,
	@AfterSnapshot NVARCHAR(MAX) = NULL,
	@TableGUID uniqueidentifier = NULL,	
	--@ActionSystemUserGUID uniqueidentifier,
	@SystemUserGUID UNIQUEIDENTIFIER,
	@ReturnResults BIT = 0
)
AS
BEGIN
	DECLARE @BeforeXML NVARCHAR(MAX) = NULL
	DECLARE @AfterXML NVARCHAR(MAX) = NULL
	IF (@GUID IS NULL)
	BEGIN
		DECLARE @GUIDS TABLE (GUID UNIQUEIDENTIFIER)
		INSERT INTO AuditLog(Source,TableGUID,TableName,SystemUserGUID, BeforeSnapshot, AfterSnapshot)
		OUTPUT inserted.GUID INTO @GUIDS (GUID)
		SELECT 
			--Action = @Action,
			Source = @Source,
			TableGUID = @TableGUID,
			TableName = @TableName,
			SystemUserGUID = @SystemUserGUID,
			BeforeSnapshot = @BeforeSnapshot,
			AfterSnapshot = @AfterSnapshot

		SELECT @GUID = GUID FROM @GUIDS
		DECLARE @SystemSystemUserGUID UNIQUEIDENTIFIER
		SELECT @SystemSystemUserGUID  = (SELECT GUID FROM SystemUser WHERE Username = 'SYSTEM')
	END
	ELSE
	BEGIN
		/*DECLARE @sqlCommand nVARCHAR(MAX)
		SET @sqlCommand = 'SELECT @Res = (SELECT * from ' +@TableName + 'FOR XML AUTO)'
		EXECUTE sp_executesql @sqlCommand, N'@Res nvarchar(MAX) OUTPUT', @res = @BeforeXML OUTPUT*/
	

		UPDATE AuditLog
		SET
			--Action = @Action,
			Source = @Source,
			TableGUID = @TableGUID,
			TableName = @TableName,
			SystemUserGUID = @SystemUserGUID,
			BeforeSnapshot = @BeforeSnapshot,
			AfterSnapshot = @AfterSnapshot
		WHERE GUID = @GUID
	END
	IF (@ReturnResults = 1)
	BEGIN
		SELECT *
		FROM AuditLog
		WHERE GUID = @GUID
	END
END
