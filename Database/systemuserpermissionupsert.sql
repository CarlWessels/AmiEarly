USE [TestDb]
GO
/****** Object:  StoredProcedure [dbo].[spSystemUserPermissionUpsert]    Script Date: 2017/10/31 4:31:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spSystemUserPermissionUpsert]
(
	@GUID uniqueidentifier,
	@IsDeleted bit,
	@ActiveDateTime datetime,
	@TerminationDateTime datetime = NULL,
	@ForSystemUserGUID uniqueidentifier,
	@PermissionGUID uniqueidentifier,
	@SystemUserGUID uniqueidentifier,
	@Token VARBINARY(MAX),
	@ReturnResults BIT = 0
)
AS
BEGIN
	IF (SELECT COUNT(*) FROM SystemUser WHERE GUID = @SystemUserGUID AND Token = @Token) = 0
	BEGIN
		UPDATE SystemUser 
		SET Token = NULL, TokenExpires = NULL 
		WHERE GUID = @SystemUserGUID;

		THROW 51000, 'Invalid token', 1
	END
	DECLARE @UpdatePermissionGUID UNIQUEIDENTIFIER = (SELECT GUID FROM Permission WHERE Permission = 'SystemUserPermissionUpdate')
	DECLARE @InsertPermissionGUID UNIQUEIDENTIFIER = (SELECT GUID FROM Permission WHERE Permission = 'SystemUserPermissionInsert')
	DECLARE @BeforeXML NVARCHAR(MAX)
	DECLARE @AfterXML NVARCHAR(MAX)

	IF (@GUID IS NULL)
	BEGIN
		SELECT @GUID = GUID FROM SystemUserPermission WHERE SystemUserGUID = @SystemUserGUID AND PermissionGUID = @PermissionGUID
	END

	IF (@GUID IS NULL)
	BEGIN
		IF (SELECT dbo.fnHasPermission(@SystemUserGUID, @InsertPermissionGUID)) = 0
		BEGIN
			THROW 51000, 'The user does not haver permission to INSERT', 1;  
		END
		ELSE
		BEGIN
			DECLARE @GUIDS TABLE (GUID UNIQUEIDENTIFIER)
			INSERT INTO SystemUserPermission(IsDeleted,ActiveDateTime,TerminationDateTime,ForSystemUserGUID,PermissionGUID,SystemUserGUID)
			OUTPUT inserted.GUID INTO @GUIDS (GUID)
			SELECT 
				IsDeleted = @IsDeleted,
				ActiveDateTime = @ActiveDateTime,
				TerminationDateTime = @TerminationDateTime,
				ForSystemUserGUID = @ForSystemUserGUID,
				PermissionGUID = @PermissionGUID,
				SystemUserGUID = @SystemUserGUID

			SELECT @GUID = GUID FROM @GUIDS
			SET @AfterXML = (SELECT * from SystemUserPermission WHERE GUID = @GUID FOR XML AUTO)
			EXEC spAuditLogUpsert NULL, 'spSystemUserPermissionUpsert', 'SystemUserPermission',@BeforeXML, @AfterXML, @GUID,  @SystemUserGUID,0
		END
	END
	ELSE
	BEGIN
		IF (SELECT dbo.fnHasPermission(@SystemUserGUID, @UpdatePermissionGUID)) = 0
		BEGIN
			THROW 51000, 'The user does not haver permission to UPDATE', 1;  
		END
		SET @BeforeXML = (SELECT * from SystemUserPermission WHERE GUID = @GUID FOR XML AUTO)
		UPDATE SystemUserPermission
		SET
			IsDeleted = @IsDeleted,
			ActiveDateTime = @ActiveDateTime,
			TerminationDateTime = @TerminationDateTime,
			ForSystemUserGUID = @ForSystemUserGUID,
			PermissionGUID = @PermissionGUID,
			SystemUserGUID = @SystemUserGUID
		WHERE GUID = @GUID
		SET @AfterXML = (SELECT * from SystemUserPermission WHERE GUID = @GUID FOR XML AUTO)
		EXEC spAuditLogUpsert NULL, 'spSystemUserPermissionUpsert', 'SystemUserPermission', @BeforeXML, @AfterXML, @GUID,  @SystemUserGUID, 0
	END
	IF (@ReturnResults = 1)
	BEGIN
		SELECT *
		FROM SystemUserPermission
		WHERE GUID = @GUID
	END
END
