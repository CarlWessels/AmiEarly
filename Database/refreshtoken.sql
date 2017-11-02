ALTER PROC spRefreshToken
(
	@SystemUserGUID UNIQUEIDENTIFIER,
	@ReturnResults BIT = 0 
)
AS
BEGIN
	DECLARE @ExpiresInMinutes INT = 1
	DECLARE @ExpiresOn DATETIME

	SELECT @ExpiresOn = DATEADD(MINUTE, @ExpiresInMinutes, GETDATE())

	UPDATE dbo.SystemUser
	SET TokenExpires = @ExpiresOn

	IF (@ReturnResults = 1)
	BEGIN
		SELECT TokenExpires
		FROM dbo.SystemUser
		WHERE GUID = @SystemUserGUID

	END

ENd