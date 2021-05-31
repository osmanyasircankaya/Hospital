CREATE DATABASE Temp

USE Temp

CREATE TABLE Polyclinic
(
	Id		int PRIMARY KEY NOT NULL,	
	Name		varchar(255) NOT NULL,
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL			
)

CREATE TABLE Doctor
(
	Id			int PRIMARY KEY NOT NULL,
	FirstName	varchar(255) NOT NULL,
	LastName	varchar(255) NOT NULL,
	Mail		varchar(255) NOT NULL,
	PolId		int FOREIGN KEY REFERENCES Polyclinic(Id),
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL
)

CREATE TABLE Patient
(	
	Id		varchar(255) PRIMARY KEY NOT NULL,
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL
)

CREATE TABLE Appointment
(
	Id		int IDENTITY(0,1) PRIMARY KEY NOT NULL,
	AppointmentDate		datetime NOT NULL,
	IsEmpty 			BIT NOT NULL,
	DoctorId	int FOREIGN KEY REFERENCES Doctor(Id),
	PatientId		varchar(255) FOREIGN KEY REFERENCES Patient(Id),
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL
)

INSERT INTO Polyclinic
VALUES
(1, 'Kardiyoloji','2016-12-21 00:00:00.000', NULL),
(2, 'Ortopedi ve Travmatoloji','2016-12-21 00:00:00.000', NULL)

INSERT INTO Doctor
VALUES
(1, 'Utku', 'Kaya', 'gumus_utku@paint.com', 1, '2016-12-21 00:00:00.000', NULL),
(2, 'Fatma', 'Aslan', 'yesim_oztas@paint.com', 1, '2016-12-21 00:00:00.000', NULL),
(3, 'Kemal', 'Baycan', 'baycan_kemal@paint.com', 2, '2016-12-21 00:00:00.000', NULL),
(4, 'Yaren', 'Atan', 'atan_nazli@paint.com', 2, '2016-12-21 00:00:00.000', NULL),
(5, 'Fatih',  'Kuyu', 'kuyu_fatih@paint.com', 3, '2016-12-21 00:00:00.000', NULL),
(6, 'Hatice',  'Sayın', 'sayin_hatice@paint.com', 3, '2016-12-21 00:00:00.000', NULL)



INSERT INTO Patient	
VALUES
('52621459889', '2016-12-21 00:00:00.000', NULL),
('94367459231', '2016-12-21 00:00:00.000', NULL),
('49412459247', '2016-12-21 00:00:00.000', NULL),
('23621156863', '2016-12-21 00:00:00.000', NULL),
('34526732824', '2016-12-21 00:00:00.000', NULL)

CREATE PROC GetAllAppointment
AS 
Select count(*) from Appointment;

/*
CREATE PROCEDURE getLastMail
AS
SELECT TOP 1 Patient.Mail FROM Patient INNER JOIN Appointment ON Patient.Id = Appointment.PatientId ORDER BY Appointment.AddedOn DESC
GO

CREATE PROCEDURE getLastDate
AS
SELECT TOP 1 Appointment.AppointmentDate FROM Appointment ORDER BY Appointment.AddedOn DESC
GO
*/

CREATE TRIGGER SendAppointmentGet
ON Appointment
AFTER INSERT
AS 
BEGIN
SET NOCOUNT ON;
	DECLARE @LastMail varchar(255)
	Select TOP 1 @LastMail = Patient.Mail FROM Patient INNER JOIN Appointment ON Patient.Id = Appointment.PatientId ORDER BY Appointment.AddedOn DESC
	DECLARE @LastDate varchar(255)
	SELECT TOP 1 @LastDate = Appointment.AppointmentDate FROM Appointment ORDER BY Appointment.AddedOn DESC
	DECLARE @body varchar(255)= 'Your appointment saved. Appointment date is ' + @LastDate + '. Please be at the hospital 15 minutes before your appointment.'
       EXEC msdb.dbo.sp_send_dbmail
          @recipients = @LastMail, 
          @profile_name = 'HospitalMail',
          @subject = 'New Appointment', 
          @body =@body
END

CREATE TRIGGER SendAppointmentDoctor
ON Appointment
AFTER INSERT
AS 
BEGIN
SET NOCOUNT ON;
	DECLARE @DocMail varchar(255)
	Select TOP 1 @DocMail = Doctor.Mail FROM Doctor INNER JOIN Appointment ON Doctor.Id = Appointment.DoctorId ORDER BY Appointment.AddedOn DESC
	DECLARE @LastDate varchar(255)
	SELECT TOP 1 @LastDate=Appointment.AppointmentDate FROM Appointment ORDER BY Appointment.AddedOn DESC
	DECLARE @DocName varchar(255)
	SELECT TOP 1 @DocName = Doctor.FirstName + ' ' + Doctor.LastName FROM Doctor INNER JOIN Appointment ON Doctor.Id = Appointment.DoctorId ORDER BY Appointment.AddedOn DESC
	DECLARE @body varchar(255)= 'Doctor ' + @DocName + ',' + CHAR(13) + CHAR(10) +'You have a new appointment on ' + @LastDate + '. For your information.'
       EXEC msdb.dbo.sp_send_dbmail
          @recipients = @DocMail, 
          @profile_name = 'HospitalMail',
          @subject = 'New Appointment', 
          @body =@body
END

CREATE TRIGGER SendAppointmentDelete
ON Appointment
AFTER DELETE
AS 
BEGIN
SET NOCOUNT ON;
	DECLARE @LastMail varchar(255)
	Select TOP 1 @LastMail = Patient.Mail FROM Patient INNER JOIN deleted ON Patient.Id = deleted.PatientId ORDER BY deleted.AddedOn DESC
	DECLARE @LastDate varchar(255)
	SELECT TOP 1 @LastDate = deleted.AppointmentDate FROM deleted ORDER BY deleted.AddedOn DESC
	DECLARE @body varchar(255)= 'Your appointment on ' + @LastDate + ' has been cancelled.'
       EXEC msdb.dbo.sp_send_dbmail
          @recipients = @LastMail, 
          @profile_name = 'HospitalMail',
          @subject = 'Appointment Cancel', 
          @body =@body
END

CREATE TRIGGER SendAppointmentCancelDoctor
ON Appointment
AFTER DELETE
AS 
BEGIN
SET NOCOUNT ON;
	DECLARE @DocMail varchar(255)
	Select TOP 1 @DocMail = Doctor.Mail FROM Doctor INNER JOIN deleted ON Doctor.Id = deleted.DoctorId ORDER BY deleted.AddedOn DESC
	DECLARE @LastDate varchar(255)
	SELECT TOP 1 @LastDate = deleted.AppointmentDate FROM deleted ORDER BY deleted.AddedOn DESC
	DECLARE @DocName varchar(255)
	SELECT TOP 1 @DocName = Doctor.FirstName + ' ' + Doctor.LastName FROM Doctor INNER JOIN deleted ON Doctor.Id = deleted.DoctorId ORDER BY deleted.AddedOn DESC
	DECLARE @body varchar(255)= 'Doctor ' + @DocName + ',' + CHAR(13) + CHAR(10) +'Your appointment on ' + @LastDate + ' has been cancelled. For your information.'
       EXEC msdb.dbo.sp_send_dbmail
          @recipients = @DocMail, 
          @profile_name = 'HospitalMail',
          @subject = 'Appointment Cancel', 
          @body =@body
END

CREATE PROCEDURE IncSalaryDocsMail
AS
	DECLARE @Mail varchar(255)
	DECLARE @DocName varchar(255)
	DECLARE @Count int = 1
	DECLARE @Last int
	SELECT TOP 1 @Last = Doctor.Id FROM Doctor ORDER BY Id DESC
	WHILE @Count <= @Last
		BEGIN
			SELECT @Mail = Doctor.Mail, @DocName = Doctor.FirstName + ' ' + Doctor.LastName FROM Doctor WHERE Id = @Count 
			DECLARE @body varchar(255)= 'Doctor ' + @DocName + ',' + CHAR(13) + CHAR(10) + 'Your salary has been increased by $250. Stay healthy.'
				EXEC msdb.dbo.sp_send_dbmail
					@recipients = @Mail, 
					@profile_name = 'HospitalMail',
					@subject = 'Salary Update', 
					@body =@body
			SET @Count += 1
		END
		
		
--Çoktan aza randevu alınan poliklinikler
CREATE PROCEDURE FindAppointmentCountWithPolyclinicName
AS
SELECT Polyclinic.Name, Count(*) AS 'AppointmentCount' FROM Appointment
INNER JOIN Doctor ON Doctor.Id=Appointment.DoctorId
INNER JOIN Polyclinic ON Polyclinic.Id=Doctor.PolId group by Polyclinic.Name Order By 'AppointmentCount' DESC

-- Randevu sayısına göre günleri sıralama
CREATE PROCEDURE FindWeekDayWithAppointmentCount
AS
SELECT DATENAME(weekday,AppointmentDate) as 'GUN',Count(AppointmentDate) as 'AppointmentCount' 
From Appointment Group by DATENAME(weekday,AppointmentDate) Order By AppointmentCount DESC;

-- Belirtilen saat aralığındaki randevu sayılarını saate göre gruplar ve randevu sayılarına göre büyükten küçüğe sıralar
CREATE PROCEDURE FindAppointmentCountWithHour @StartTime int, @EndTime int
AS
SELECT DATEPART(HOUR,AppointmentDate) as HOURS, Count(*) as AppointmentCount from Appointment 
WHERE DATEPART(HOUR,AppointmentDate) BETWEEN @StartTime AND @EndTime Group By DATEPART(HOUR,AppointmentDate) 
Order By AppointmentCount DESC


