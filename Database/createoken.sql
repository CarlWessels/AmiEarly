ALTER PROCEDURE spGenerateToken
(
	@SystemUserGUID UNIQUEIDENTIFIER,
	@ReturnResults BIT = 0
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Token VARBINARY(MAX)
	
	SELECT	@Token = CONVERT(VARBINARY(MAX),NEWID())



	UPDATE dbo.SystemUser
	SET Token = @Token

	EXEC spRefreshToken @SystemUserGUID, 0

	IF (@ReturnResults = 1)
	BEGIN
		SELECT Token, TokenExpires
		FROM dbo.SystemUser 
		WHERE GUID = @SystemUserGUID
	END
    
END
GO
