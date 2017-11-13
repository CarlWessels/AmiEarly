declare @token uniqueIdentifier
exec dbo.spLogin @UserName = N'SYSTEM', -- nvarchar(254)
                 @Password = N'PASSWORD'  -- nvarchar(50)


select @token = Token from dbo.SystemUser where Username = 'SYSTEM'

/*
exec dbo.spSystemUserUpsert @GUID = null,                                 -- uniqueidentifier
                            @IsDeleted = 0,                            -- bit
                            @ActiveDateTime = '2017-11-13 08:08:11',      -- datetime
                            @TerminationDateTime =  null, -- datetime
                            @Username = 'TESTMERCHANT',                               -- varchar(max)
                            @Password = 'TESTMERCHANT',                               -- varchar(max)
							@Token = @token,
                            @ReturnResults = 0-- bit


exec dbo.spSystemUserUpsert @GUID = null,                                 -- uniqueidentifier
                            @IsDeleted = 0,                            -- bit
                            @ActiveDateTime = '2017-11-13 08:08:11',      -- datetime
                            @TerminationDateTime =  null, -- datetime
                            @Username = 'TESTCLIENT',                               -- varchar(max)
                            @Password = 'TESTCLIENT',                               -- varchar(max)
							@Token = @token,
                            @ReturnResults = 0-- bit
select * from dbo.SystemUser

*/

declare @MerchantServicePermissionGUID uniqueIdentifier = (select GUID from LUPermission where Permission = 'MerchantServiceAccess')
declare @TestMerchantGUID uniqueIdentifier = (select guid from dbo.SystemUser where Username = 'TESTMERCHANT')
declare @CurrentDate dateTime = (select getDate())


exec dbo.spSystemUserPermissionUpsert  @GUID = null, @IsDeleted = 0, @ActiveDateTime = @CurrentDate, @TerminationDateTime = null, @ForSystemUserGUID = @TestMerchantGUID, @PermissionGUID= @MerchantServicePermissionGUID, @Token = @token, @ReturnResults = 1


