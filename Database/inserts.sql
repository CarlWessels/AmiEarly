SET NOCOUNT ON
DECLARE @CreationDATE DATETIME
SELECT @CreationDATE = GETDATE()
DECLARE @SystemUserGUID UNIQUEIDENTIFIER
SELECT @SystemUserGUID = (SELECT GUID FROM SystemUser WHERE Username = 'SYSTEM')



DECLARE @Account TABLE
(
	GUID	uniqueidentifier
	,ID	int
	,DateTimeCreated	datetime
	,IsDeleted	bit
	,ActiveDateTime	datetime
	,TerminationDateTime	datetime
	,IsActiveForNow	bit
	,AccountName	varchar(MAX)
	,SystemUserGUID UNIQUEIDENTIFIER
)

DECLARE @Store TABLE
(
	GUID	UNIQUEIDENTIFIER
	,ID	INT
	,DateTimeCreated	DATETIME
	,IsDeleted	BIT
	,ActiveDateTime	DATETIME
	,TerminationDateTime	DATETIME
	,IsActiveForNow	BIT
	,StoreName	VARCHAR(MAX)
	,AccountGUID	UNIQUEIDENTIFIER
    ,SystemUserGUID UNIQUEIDENTIFIER
)

DECLARE @Customer TABLE
(
	GUID	uniqueidentifier
	,ID	int
	,DateTimeCreated	datetime
	,IsDeleted	bit
	,ActiveDateTime	datetime
	,TerminationDateTime	datetime
	,IsActiveForNow	bit
	,Firstname	varchar(max)
	,Surname	varchar(max)
	,EmailAddress	varchar(max)
	,IDNumber	varchar(max)
	,BirthDate	date
	,CellphoneNumber	varchar(max)
	,AccountGUID	uniqueidentifier
	,SystemUserGUID	uniqueidentifier
)

DECLARE @ServiceProvider TABLE
(
	GUID	uniqueidentifier
	,ID	int
	,DateTimeCreated	datetime
	,IsDeleted	bit
	,ActiveDateTime	datetime
	,TerminationDateTime	datetime
	,IsActiveForNow	bit
	,Firstname	VARCHAR(MAX)
	,Surname	VARCHAR(MAX)
	,AccountGUID	UNIQUEIDENTIFIER
    ,SystemUserGUID UNIQUEIDENTIFIER

)

DECLARE @Appointment TABLE
(
	GUID	uniqueidentifier
	,ID	int
	,DateTimeCreated	datetime
	,IsDeleted	bit
	,StartDateTime	datetime
	,EndDateTime	datetime
	,Duration	time
	,ActualStartDateTime	datetime
	,ActualEndDateTime	datetime
	,CustomerGUID	uniqueidentifier
	,StoreGUID	uniqueidentifier
	,ServiceProviderGUID	UNIQUEIDENTIFIER
    
	,GenID INT IDENTITY(1,1)
	,SystemUserGUID UNIQUEIDENTIFIER
)

DECLARE @ActivityType TABLE
(
	GUID	uniqueidentifier
	,ID	int
	,DateTimeCreated	datetime
	,IsDeleted	bit
	,ActivityType	VARCHAR(MAX)
	,AccountGUID	UNIQUEIDENTIFIER
	,SystemUserGUID UNIQUEIDENTIFIER
)

DECLARE @TokenTable TABLE
(
	Token VARBINARY(MAX),
	TokenExpires DATETIME
)
INSERT INTO @TokenTable (Token,TokenExpires)
EXEC spGenerateToken @SystemUserGUID, 1


DECLARE @Token  VARBINARY(MAX)

SELECT @Token = Token FROM @TokenTable


INSERT INTO @Account EXEC spAccountUpsert  NULL, 0, @CreationDATE, NULL, 'Account1', @Token, 1



DECLARE @AccountGUID UNIQUEIDENTIFIER 
SELECT @AccountGUID = GUID FROM Account WHERE AccountName = 'Account1'


INSERT INTO @Store EXEC spStoreUpsert NULL, 0, @CreationDATE, NULL, 'Store1', @AccountGUID, @Token, 1

DECLARE @StoreGUID UNIQUEIDENTIFIER
SELECT @StoreGUID = GUID FROM Store WHERE StoreName = 'Store1'


INSERT INTO @Customer EXEC spCustomerUpsert NULL, 0, @CreationDATE, NULL, 'John', 'Smith', 'test@email.com', '123', '2011-01-01', '08245784564', @AccountGUID, @Token,1

DECLARE @CustomerGUID UNIQUEIDENTIFIER
SELECT @CustomerGUID = GUID FROM dbo.Customer WHERE Firstname = 'John'


INSERT INTO @ServiceProvider EXEC spServiceProviderUpsert NULL, 0, @CreationDATE, NULL, 'Marius', 'Roos', @AccountGUID, @Token,1
INSERT INTO @ServiceProvider EXEC spServiceProviderUpsert NULL, 0, @CreationDATE, NULL, 'Koos', 'Van der Merwe', @AccountGUID,@Token,1

DECLARE @ServiceProviderA UNIQUEIDENTIFIER
DECLARE @ServiceProviderB UNIQUEIDENTIFIER

SELECT @ServiceProviderA = GUID FROM dbo.ServiceProvider WHERE Firstname = 'Marius'
SELECT @ServiceProviderB = GUID FROM dbo.ServiceProvider WHERE Firstname = 'Koos'


INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 9:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 10:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 12:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 12:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 13:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 13:30:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 14:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 14:30:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 15:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1

INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-02 8:00:00', '00:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderA, @Token,1
INSERT INTO @Appointment EXEC spAppointmentUpsert NULL, 0, '2017-01-01 12:00:00', '01:30:00', NULL, NULL, @CustomerGUID, @StoreGUID, @ServiceProviderB, @Token,1


INSERT INTO @ActivityType EXEC spLUActivityTypeUpsert NULL, 0, 'Consultations', @AccountGUID, @Token,0
INSERT INTO @ActivityType EXEC spLUActivityTypeUpsert NULL, 0, 'Rounds', @AccountGUID, @Token, 0



/*

	INSERT INTO ActivitySchedule (DoW, Starttime, EndTime, ActivityTypeGUID, ServiceProviderGUID) SELECT 1, '8:00:00', '11:59:59', (SELECT GUID FROM ActivityType WHERE ActivityType = 'Rounds'),  (SELECT GUID FROM ServiceProvider WHERE Firstname = 'ServiceProviderName')
	INSERT INTO ActivitySchedule (DoW, Starttime, EndTime, ActivityTypeGUID, ServiceProviderGUID) SELECT 1, '12:00:00', '17:00:00', (SELECT GUID FROM ActivityType WHERE ActivityType = 'Consultation'),  (SELECT GUID FROM ServiceProvider WHERE Firstname = 'ServiceProviderName')

	INSERT INTO ActivitySchedule (DoW, Starttime, EndTime, ActivityTypeGUID, ServiceProviderGUID) SELECT 2, '8:00:00', '17:00:00', (SELECT GUID FROM ActivityType WHERE ActivityType = 'Consultation'),  (SELECT GUID FROM ServiceProvider WHERE Firstname = 'ServiceProviderName2')
	INSERT INTO ActivitySchedule (DoW, Starttime, EndTime, ActivityTypeGUID, ServiceProviderGUID) SELECT 3, '8:00:00', '17:00:00', (SELECT GUID FROM ActivityType WHERE ActivityType = 'Consultation'),  (SELECT GUID FROM ServiceProvider WHERE Firstname = 'ServiceProviderName2')
	INSERT INTO ActivitySchedule (DoW, Starttime, EndTime, ActivityTypeGUID, ServiceProviderGUID) SELECT 4, '8:00:00', '17:00:00', (SELECT GUID FROM ActivityType WHERE ActivityType = 'Consultation'),  (SELECT GUID FROM ServiceProvider WHERE Firstname = 'ServiceProviderName2')
	INSERT INTO ActivitySchedule (DoW, Starttime, EndTime, ActivityTypeGUID, ServiceProviderGUID) SELECT 5, '8:00:00', '17:00:00', (SELECT GUID FROM ActivityType WHERE ActivityType = 'Consultation'),  (SELECT GUID FROM ServiceProvider WHERE Firstname = 'ServiceProviderName2')
	INSERT INTO ActivitySchedule (DoW, Starttime, EndTime, ActivityTypeGUID, ServiceProviderGUID) SELECT 6, '8:00:00', '17:00:00', (SELECT GUID FROM ActivityType WHERE ActivityType = 'Consultation'),  (SELECT GUID FROM ServiceProvider WHERE Firstname = 'ServiceProviderName2')
*/

DECLARE @AppointmentGUID UNIQUEIDENTIFIER
DECLARE @StartDateTime DATETIME
DECLARE @Duration TIME

SELECT @AppointmentGUID = GUID, @Duration = Duration, @StartDateTime = StartDateTime FROM @Appointment WHERE GenID = 1
EXEC dbo.spAppointmentUpsert	@AppointmentGUID,
								0, 
								@StartDateTime,
								@Duration,
								'2017-01-01 8:00:00',
								'2017-01-01 8:15:00',
								@CustomerGUID,
								@StoreGUID,
								@ServiceProviderA,
								@Token,
								0
SELECT @AppointmentGUID = GUID, @Duration = Duration, @StartDateTime = StartDateTime FROM @Appointment WHERE GenID = 2
EXEC dbo.spAppointmentUpsert	@AppointmentGUID,
								0, 
								@StartDateTime,
								@Duration,
								'2017-01-01 09:00:00.000',
								'2017-01-01 9:55:00',
								@CustomerGUID,
								@StoreGUID,
								@ServiceProviderA,
								@Token,
								0



SELECT @AppointmentGUID = GUID, @Duration = Duration, @StartDateTime = StartDateTime FROM @Appointment WHERE GenID = 3
EXEC dbo.spAppointmentUpsert	@AppointmentGUID,
								0, 
								@StartDateTime,
								@Duration,
								'2017-01-01 9:55:00',
								'2017-01-01 10:20:00',
								@CustomerGUID,
								@StoreGUID,
								@ServiceProviderA,
								@Token,
								0

SELECT @AppointmentGUID = GUID, @Duration = Duration, @StartDateTime = StartDateTime FROM @Appointment WHERE GenID = 4
EXEC dbo.spAppointmentUpsert	@AppointmentGUID,
								0, 
								@StartDateTime,
								@Duration,
								'2017-01-01 10:20:00',
								'2017-01-01 16:20:00',
								@CustomerGUID,
								@StoreGUID,
								@ServiceProviderA,
								@Token,
								0

/*
SELECT @AppointmentGUID = GUID, @Duration = Duration, @StartDateTime = StartDateTime FROM @Appointment WHERE GenID = 5
EXEC dbo.spAppointmentUpsert	@AppointmentGUID,
								0, 
								@StartDateTime,
								@Duration,
								'2017-01-01 16:25:00.000' ,
								NULL,
								@CustomerGUID,
								@StoreGUID,
								@ServiceProviderA,
								0*/








