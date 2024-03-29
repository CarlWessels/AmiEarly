USE [TestDb]
GO
/****** Object:  UserDefinedFunction [dbo].[fnHasPermission]    Script Date: 2017/10/31 4:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fnHasPermission] 
(
	@SystemUserGUID UNIQUEIDENTIFIER,
	@PermissionGUID UNIQUEIDENTIFIER
)
RETURNS BIT
AS
BEGIN
	DECLARE @Result BIT = 0

	SELECT @Result = CASE WHEN (SELECT COUNT(*) FROM dbo.SystemUserPermission WHERE PermissionGUID = @PermissionGUID AND ForSystemUserGUID = @SystemUserGUID) > 0 THEN 1 ELSE 0 END

	IF (@Result = 0)
	BEGIN
		SELECT @Result = CASE WHEN
		(
			SELECT COUNT(*)
			FROM dbo.SystemUserGroupLine gl 
			INNER JOIN SystemUserGroupPermission gp ON	gp.SystemUserGroupGUID = gp.SystemUserGroupGUID
														AND gp.PermissionGUID = @PermissionGUID
			WHERE gl.ForSystemUserGUID = @SystemUserGUID
		) = 0 THEN 0 ELSE 1 END
	END

	RETURN @Result

END
