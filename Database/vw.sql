ALTER VIEW vwAppointment
AS

	SELECT ServiceProviderFirstname = sp.Firstname, ServiceProviderSurname = sp.Surname
			, CustomerFirstname = cust.Firstname, CustomerSurname = cust.Surname
			, a.StartDateTime,   a.EndDateTime, a.Duration
			, a.ActualStartDateTime, a.ActualEndDateTime, a.ActualDuration
			, a.DelayTime, a.ExpectedDelay, a.ExpectedStartDateTime
			, Colour =	CASE	WHEN a.ActualEndDateTime IS NOT NULL THEN 'Grey'
								WHEN ActualStartDateTime IS NOT NULL AND ActualEndDateTime IS NULL THEN 'Orange'
								WHEN ISNULL(a.ExpectedDelay,0) > 0 THEN 'Red'
								ELSE 'Green'
						END
	FROM
	(
		SELECT a.*
				, ExpectedStartDateTime = DATEADD(MINUTE, a.ExpectedDelay, a.StartDateTime)
		FROM 
		(
			SELECT	*,
					ExpectedDelay =  dbo.fnGetAppointmentDelayTime(a.GUID),
					ActualDuration =	CASE	WHEN ActualStartDateTime IS NULL 
													THEN NULL 
												WHEN ActualEndDateTime IS NULL 
													THEN NULL
												ELSE CONVERT(TIME,CONVERT(VARCHAR(MAX),DATEDIFF(SECOND, ActualStartDateTime, ActualEndDateTime)/3600)  
														+':' 
														+ RIGHT('00'+CONVERT(VARCHAR(MAX),(DATEDIFF(SECOND, ActualStartDateTime, ActualEndDateTime)%3600)/60),2) 
														+':' 
														+ RIGHT('00'+CONVERT(VARCHAR(MAX),DATEDIFF(SECOND, ActualStartDateTime, ActualEndDateTime)%60),2))
										END,
		
					DelayTime = CASE	WHEN ActualEndDateTime > DATEADD(SECOND, -1 ,StartDateTime + CONVERT(DATETIME,Duration))
								THEN DATEDIFF(MINUTE,DATEADD(SECOND, -1 ,StartDateTime + CONVERT(DATETIME,Duration)), ActualEndDateTime)
							WHEN ActualEndDateTime IS NULL
								THEN NULL
							ELSE 0
					END
			FROM dbo.Appointment a
		) a
	) a
	INNER JOIN dbo.Customer cust ON a.CustomerGUID = cust.GUID
	INNER JOIN dbo.Store st ON a.StoreGUID = st.GUID
	INNER JOIN dbo.ServiceProvider sp ON a.ServiceProviderGUID = sp.GUID



