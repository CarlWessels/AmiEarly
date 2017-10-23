


DECLARE @Calender TABLE
(
	StartDateTime DATETIME NOT NULL,
	EndDateTime DATETIME NOT NULL,
	ServiceProviderGUID UNIQUEIDENTIFIER NOT NULL,
	ActivityTypeGUID UNIQUEIDENTIFIER 
)

DECLARE @MinutesInveral INT = 30
DECLARE @FromDateTime DATETIME = '2017-01-01 8:00:00'
DECLARE @EndDateTime DATETIME  = '2017-01-14 17:00:00'
DECLARE @ActivityTypeGUID UNIQUEIDENTIFIER
DECLARE @ShowActiveOnly BIT = 0

--SELECT @ActivityTypeGUID = GUID FROM ActivityType WHERE ActivityType = 'Consultation'

DECLARE @CurrentDateTime DATETIME 
SELECT @CurrentDateTime = @FromDateTime


WHILE (@CurrentDateTime < @EndDatetime)
BEGIN
	INSERT INTO @Calender	(StartDateTime, EndDateTime, ServiceProviderGUID) 
	SELECT @CurrentDateTime, DATEADD(SECOND, -1 , DATEADD(MINUTE, @MinutesInveral,@CurrentDateTime )), p.GUID
	FROM ServiceProvider p 
	
	SET @CurrentDateTime = DATEADD(MINUTE, @MinutesInveral, @CurrentDateTime)

END

UPDATE c
SET c.ActivityTypeGUID = s.ActivityTypeGUID
--SELECT c.StartDateTime, c.EndDateTime, atype.ActivityType
FROM @Calender c
LEFT JOIN ActivitySchedule s ON s.ServiceProviderGUID = c.ServiceProviderGUID
LEFT JOIN ActivityType atype ON atype.GUID = s.ActivityTypeGUID
WHERE DATEPART(dw,c.StartDateTime) = s.DoW
		AND CONVERT(TIME,c.StartDateTime) BETWEEN s.StartTime AND s.EndTime


DECLARE @Appointments TABLE
(
	StarDateTime DATETIME,
	EndDateTime DATETIME,
	AppointmentGUID UNIQUEIDENTIFIER,
	CustomerGUID UNIQUEIDENTIFIER,
	ServiceProviderGUID UNIQUEIDENTIFIER,
	ActivityTypeGUID UNIQUEIDENTIFIER
)



IF (@ActivityTypeGUID IS NULL)
BEGIN
	
	INSERT INTO @Appointments
	(
	    StarDateTime,
	    EndDateTime,
		AppointmentGUID,
	    CustomerGUID,
	    ServiceProviderGUID,
	    ActivityTypeGUID
	)
	SELECT c.StartDateTime, c.EndDateTime, a.GUID, a.CustomerGUID, c.ServiceProviderGUID, c.ActivityTypeGUID
	FROM @Calender c 
	LEFT JOIN Appointment a ON c.StartDateTime BETWEEN a.StartDatetime AND a.EndDateTime
							AND a.ServiceProviderGUID = c.ServiceProviderGUID
	LEFT JOIN ActivityType atype ON atype.GUID = c.ActivityTypeGUID
END
ELSE
BEGIN

	INSERT INTO @Appointments
	(
	    StarDateTime,
	    EndDateTime,
		AppointmentGUID,
	    CustomerGUID,
	    ServiceProviderGUID,
	    ActivityTypeGUID
	)
	SELECT c.StartDateTime, c.EndDateTime, a.GUID, a.CustomerGUID, c.ServiceProviderGUID, c.ActivityTypeGUID
	FROM @Calender c 
	LEFT JOIN Appointment a ON c.StartDateTime BETWEEN a.StartDatetime AND a.EndDateTime
							AND a.ServiceProviderGUID = c.ServiceProviderGUID
	LEFT JOIN ActivityType atype ON atype.GUID = c.ActivityTypeGUID
	WHERE c.ActivityTypeGUID = @ActivityTypeGUID
END

/*
SELECT a.id, sp.Firstname,  atype.ActivityType, aa.*, a.ActualEndDateTime, a.DelayTime
FROM @Appointments aa
INNER JOIN ServiceProvider sp ON aa.ServiceProviderGUID = sp.guid
LEFT JOIN Appointment a ON aa.AppointmentGUID = a.GUID
LEFT JOIN ActivityType atype ON atype.GUID = aa.ActivityTypeGUID
ORDER BY sp.FirstName, aa.StarDateTime
*/

SELECT a.EndDateTime, a.ActualEndDateTime, DelayTime, DATEADD(MINUTE,(SELECT DelayTime FROM Appointment WHERE ID = (SELECT MAX(ID) FROM Appointment WHERE ID < a.ID AND DelayTime IS NOT NULL AND a.ServiceProviderGUID = ServiceProviderGUID)), EndDateTime)
FROM Appointment a




