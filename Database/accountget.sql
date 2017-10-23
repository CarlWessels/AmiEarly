CREATE PROCEDURE spAccountGet
(
	@AccountGUID UNIQUEIDENTIFIER
)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE GUID = @AccountGUID
END