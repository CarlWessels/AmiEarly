SELECT *
FROM	 dbo.vwAppointment

UPDATE Appointment 
SET ActualEndDateTime = '2017-01-01 8:15:00'
	,ActualStartDateTime = '2017-01-01 8:00:00'
WHERE ID = 1
SELECT *
FROM	 dbo.vwAppointment



UPDATE Appointment 
SET ActualEndDateTime = '2017-01-01 9:55:00'
	,ActualStartDateTime = '2017-01-01 09:00:00.000'
WHERE ID = 2
SELECT *
FROM	 dbo.vwAppointment


UPDATE Appointment 
SET ActualEndDateTime = '2017-01-01 10:20:00'
	,ActualStartDateTime = '2017-01-01 9:55:00' 
WHERE ID = 3
SELECT *
FROM	 dbo.vwAppointment

UPDATE Appointment 
SET ActualEndDateTime = '2017-01-01 16:20:00'
	,ActualStartDateTime = '2017-01-01 10:20:00' 
WHERE ID = 4
SELECT *
FROM	 dbo.vwAppointment


UPDATE Appointment 
SET ActualStartDateTime = '2017-01-01 16:25:00.000' 
WHERE ID = 5
SELECT *
FROM	 dbo.vwAppointment




UPDATE Appointment 
SET ActualEndDateTime = '2017-01-01 16:35:00.000' 
WHERE ID = 5
SELECT *
FROM	 dbo.vwAppointment

