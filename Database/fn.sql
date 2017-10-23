ALTER FUNCTION dbo.fnGetAppointmentDelayTime
(
	@AppointmentGUID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
    DECLARE @ServiceProviderGUID UNIQUEIDENTIFIER
	DECLARE @StoreGUID UNIQUEIDENTIFIER 
	DECLARE @ID INT 
	DECLARE @ActualEndDateTime DATETIME
	DECLARE @StartDate DATE

	SELECT @StoreGUID = StoreGUID, @ServiceProviderGUID = ServiceProviderGUID, @ID = ID, @ActualEndDateTime = ActualEndDateTime, @StartDate = CONVERT(DATE, StartDateTime)
	FROM dbo.Appointment
	WHERE GUID = @AppointmentGUID

	IF (@ActualEndDateTime IS NOT NULL)
	BEGIN
		RETURN NULL
	END

	DECLARE @PreviousAppointmentGUID UNIQUEIDENTIFIER 

	
	SELECT @PreviousAppointmentGUID = GUID 
	FROM dbo.Appointment
	WHERE ID = 
	(
		SELECT TOP 1 ID 
		FROM dbo.Appointment
		WHERE ServiceProviderGUID = @ServiceProviderGUID 
				AND StoreGUID = @StoreGUID
				AND ID < @ID
				AND CONVERT(DATE, StartDateTime) = @StartDate
		ORDER BY ID DESC
	)

	IF (@PreviousAppointmentGUID IS NULL)
	BEGIN
		RETURN NULL
	END

	DECLARE @PreviousDelay INT
	SELECT @PreviousDelay = CASE	WHEN ActualEndDateTime > DATEADD(SECOND, -1 ,StartDateTime + CONVERT(DATETIME,Duration))
										THEN DATEDIFF(MINUTE,DATEADD(SECOND, -1 ,StartDateTime + CONVERT(DATETIME,Duration)), ActualEndDateTime)
									WHEN ActualEndDateTime IS NULL
										THEN NULL
									ELSE 0
							END
	FROM dbo.Appointment
	WHERE GUID = @PreviousAppointmentGUID


	IF (@PreviousDelay	IS NULL)
	BEGIN
		SELECT @PreviousDelay  = dbo.fnGetAppointmentDelayTime(@PreviousAppointmentGUID)
	END

	RETURN @PreviousDelay

END
GO
